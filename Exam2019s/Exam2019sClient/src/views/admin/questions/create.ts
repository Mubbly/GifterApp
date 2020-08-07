import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { QuestionService } from "service/questionService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";
import * as Utils from 'utils/utilFunctions';
import { AppState } from "state/appState";
import { IQuestionCreate } from "domain/IQuestion";

@autoinject
export class QuestionsCreate {
    private _name = "";
    private _quizId = "";
    private _quizName = "";
    private _errorMessage: Optional<string> = null;

    constructor(
        private questionService: QuestionService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this._quizId = params.id;
        this._quizName = params.name;
    }

    // On submit create new Question
    onSubmit(event: Event) {
        event.preventDefault();

        if(this._quizId) {
            let newQuestion: IQuestionCreate = {
                name: this._name,
                quizId: this._quizId
            };
            this.createQuestion(newQuestion);
        }
    }

    private createQuestion(newQuestion: IQuestionCreate): void {
        this.questionService
            .create(newQuestion)
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
