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

@autoinject
export class QuizzesCreate {
    private _name = "";
    private _description = null;
    private _quizTypeId = "";
    private _quizTypes: Optional<IQuizType> = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private QuizService: QuizService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        // this._quizTypes = getQuizTypes(); // TODO
    }

    // On submit create new Quiz
    onSubmit(event: Event) {
        event.preventDefault();

        console.warn("Not implemented");

        let newQuiz: IQuizCreate = {
            name: this._name,
            description: Utils.setNullIfEmpty(this._description),
            appUserId: this.appState.userId!,
            quizTypeId: this._quizTypeId,

        };
        // this.createQuiz(newQuiz);
    }

    // private createQuiz(newQuiz: IQuiz): void {
    //     this.QuizService
    //         .create(newQuiz)
    //         .then((response) => {
    //             if (!UtilFunctions.isSuccessful(response)) {
    //                 this._errorMessage = UtilFunctions.getErrorMessage(response);
    //             } else {
    //                 var createdQuizId = response.data?.id;
    //                 this.router.navigateToRoute(Utils.CREATE_QUESTIONS_ROUTE, { id: createdQuizId});
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }
}
