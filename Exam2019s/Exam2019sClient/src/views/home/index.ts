import { autoinject } from "aurelia-framework";
import { AppState } from "state/appState";
import { Optional } from "types/generalTypes";
import { QuizResponseService } from "../../service/quizResponseService";
import * as Utils from "utils/utilFunctions";
import { QuizService } from "../../service/quizService";
import { IQuizReport } from "domain/IQuizReport";
import { IQuiz } from "domain/IQuiz";

@autoinject
export class HomeIndex {
    private readonly DEFAULT_NAME = "friend";
    private readonly NO_QUIZZES_MESSAGE = "There are currently no quizzes";

    private _userFullName: Optional<string> = this.DEFAULT_NAME;
    private _quizzes: Optional<IQuiz[]> = [];
    private _quizReports: Record<string, IQuizReport> = {};
    private _noQuizzesMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;


    constructor(
        private appState: AppState,
        private quizService: QuizService,
        private quizResponseService: QuizResponseService
    ) {}

    activate(props: any) {
        if (this.appState.jwt && this.appState.userFullName) {
            this._userFullName = this.appState.userFullName;
            this.getAllQuizzes();
        }
    }

    onQuizClick(event: Event, quizId: string) {
        event.preventDefault();
        this.getQuizReport(quizId);
    }

    private getAllQuizzes() {
        this.quizService
            .getAll()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    if(response.data && response.data.length) {
                        this._quizzes = response.data;
                        this._noQuizzesMessage = null;
                    } else {
                        this._noQuizzesMessage = this.NO_QUIZZES_MESSAGE;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private getQuizReport(quizId: string): void {
        this.quizResponseService
            .getQuizReport(quizId)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    if(response.data) {
                        this._quizReports[quizId] = response.data;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
