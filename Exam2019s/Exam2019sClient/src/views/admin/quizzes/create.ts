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
import { AdminIndex } from '../index';

@autoinject
export class QuizzesCreate {
    private _name = "";
    private _description = null;
    private _quizTypeId = "";
    private _quizTypes: IQuizType[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(
        private quizTypeService: QuizTypeService,
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
        this.getQuizTypes();
    }

    // On submit create new Quiz
    onSubmit(event: Event) {
        event.preventDefault();

        let newQuiz: IQuizCreate = {
            name: this._name,
            description: Utils.setNullIfEmpty(this._description),
            appUserId: this.appState.userId!,
            quizTypeId: this._quizTypeId,

        };
        this.createQuiz(newQuiz);
    }

    private getQuizTypes(): void {
        this.quizTypeService
            .getAll()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    if(response.data) {
                        this._quizTypes = response.data;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private createQuiz(newQuiz: IQuizCreate): void {
        this.quizService
            .create(newQuiz)
            .then((response) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(Utils.ADMIN_ROUTE);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
