import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { QuestionService } from "service/questionService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";
import * as Utils from 'utils/utilFunctions';
import { AppState } from "state/appState";
import { IQuestionCreate } from "domain/IQuestion";
import { IAnswerCreate } from '../../../domain/IAnswer';
import { AnswerService } from '../../../service/answerService';

@autoinject
export class QuestionsCreate {
    private _name = "";
    private _quizId = "";
    private _questionId = "";
    private _questionName = "";
    private _errorMessage: Optional<string> = null;

    constructor(
        private answerService: AnswerService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this._questionId = params.id;
        this._questionName = params.name;
    }

    // On submit create new Question
    onSubmit(event: Event) {
        event.preventDefault();

        if(this._questionId) {
            let newAnswer: IAnswerCreate = {
                name: this._name,
                questionId: this._questionId
            };
            this.createAnswer(newAnswer);
        }
    }

    private createAnswer(newAnswer: IAnswerCreate): void {
        this.answerService
            .create(newAnswer)
            .then((response) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(Utils.ADMIN_ROUTE); // TODO: navigate to corresponding quiz
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
