import { autoinject } from "aurelia-framework";
import { IQuestion } from "domain/IQuestion";
import { Optional, IFetchResponse } from "types/generalTypes";
import { AppState } from "state/appState";
import { QuestionService } from "service/questionService";
import * as Utils from "utils/utilFunctions";
import { Router } from "aurelia-router";
import { ExamInterface } from "../../types/generalTypes";
import { IAnswer } from "../../domain/IAnswer";
import { AnswerService } from "../../service/answerService";
import { IQuizResponse, IQuizResponseCreate } from "domain/IQuizResponse";
import { forEach } from "lodash";
import { QuizResponseService } from "service/quizResponseService";
import { QuizService } from '../../service/quizService';

@autoinject
export class QuizzesDetails {
    public readonly NO_QUESTIONS_MESSAGE = "No questions found";
    public readonly ERROR_NOT_ALL_QUESTIONS_ANSWERED = "Not all questions are answered";
    public readonly QUIZ_RESPONSE_SAVED = "Thank you for answering!";

    private _questions: IQuestion[] = [];
    private _questionAnswers: IAnswer[] = [];
    private _noQuestionsMessage: Optional<string> = null;
    private _successMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    private _quizId: string = "";
    private _isCreator: boolean = false;
    // private _isAnswerInputVisible: boolean = false;

    private _mapOfQuestionAnswers: Record<string, string> = {};

    constructor(
        private appState: AppState,
        private router: Router,
        private quizService: QuizService,
        private questionService: QuestionService,
        private answerService: AnswerService,
        private quizResponseService: QuizResponseService
    ) {}

    attached() {}

    activate(params: any) {
        if (!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getAllQuestions(params.id);
            this._quizId = params.id;
            this.getIsUserQuizCreator(this._quizId);
        }
    }

    // addAnswer(event: Event) {
    //     event.preventDefault();
    //     this._isAnswerInputVisible = true;
    // }

    onSelect(event: Event, questionId: string, answerId: string) {
        event.preventDefault();
        this.rememberAnswer(questionId, answerId);
    }

    onSubmit(event: Event) {
        event.preventDefault();
        let answeredQuestionsIds = Object.keys(this._mapOfQuestionAnswers);

        for (let question of this._questions) {
            // Don't allow unanswered questions
            if(!answeredQuestionsIds.includes(question.id)) {
                this._errorMessage = this.ERROR_NOT_ALL_QUESTIONS_ANSWERED;
                return;
            }
        }
        // Send all answers as a list via one request
        let quizResponses: IQuizResponseCreate[] = Object.keys(this._mapOfQuestionAnswers).map((questionId) => {
            let quizResponse = {
                answerId: this._mapOfQuestionAnswers[questionId],
                questionId: questionId,
                quizId: this._quizId,
                appUserId: this.appState!.userId!
            }
            return quizResponse;
        });
        this.saveAnswers(quizResponses);
    }

    /** Add new map entry when question wasn't answered yet, modify existing one when it was already present */
    private rememberAnswer(questionId: string, answerId: string) {
        this._mapOfQuestionAnswers[questionId] = answerId;
    }

    private saveAnswers(quizResponses: IQuizResponseCreate[]) {
        this.quizResponseService
        .createMultiple(quizResponses)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                this._errorMessage = null;
                this._successMessage = this.QUIZ_RESPONSE_SAVED;
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }
 
    private getAllQuestions(quizId: string): void {
        this.questionService
            .getAllForQuiz(quizId)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    if (!response.data || response.data.length <= 0) {
                        this._noQuestionsMessage = this.NO_QUESTIONS_MESSAGE;
                        return;
                    }
                    this._noQuestionsMessage = null;
                    this._questions = response.data;

                    // Get answer choices for each question
                    if (this._questions.length) {
                        this._questions.forEach(async (question) => {
                            const answers = await this.getAnswersForQuestion(question.id);
                            question.answers = answers;
                        });
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private async getAnswersForQuestion(questionId: string): Promise<IAnswer[]> {
        return this.answerService
            .getAllForQuestion(questionId)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    console.warn(Utils.getErrorMessage(response));
                    return [];
                } else {
                    return response.data ? response.data : [];
                }
            })
            .catch((error) => {
                console.log(error);
                return [];
            });
    }

    private getIsUserQuizCreator(quizId: string) {
        this.quizService
        .get(quizId)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(response.data) {
                    this._isCreator = response.data.appUserId === this.appState.userId;
                }
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(
        response: IFetchResponse<ExamInterface | ExamInterface[]>
    ) {
        switch (response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            case Utils.STATUS_CODE_NOT_FOUND:
                this._noQuestionsMessage = this.NO_QUESTIONS_MESSAGE;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
