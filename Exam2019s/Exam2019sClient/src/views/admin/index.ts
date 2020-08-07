import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { QuizService } from "service/quizService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";
import { IFetchResponse } from "types/generalTypes";
import * as Utils from 'utils/utilFunctions';
import { AppState } from "state/appState";
import { IQuizCreate, IQuiz } from "domain/IQuiz";
import { IQuizType } from "domain/IQuizType";
import { QuizTypeService } from "service/quizTypeService";

@autoinject
export class AdminIndex {
    private _personalQuizzes: IQuiz[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(
        private quizService: QuizService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getAllPersonal();
    }

    private getAllPersonal(): void {
        this.quizService
            .getAllPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    if(response.data) {
                        this._personalQuizzes = response.data;
                        this._errorMessage = null;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
