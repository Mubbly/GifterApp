import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { ExampleService } from "service/exampleService";
import * as UtilFunctions from "utils/utilFunctions";
import { IExample } from "domain/IExample";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import * as Utils from "utils/utilFunctions";

@autoinject
export class ExamplesDelete {
    private _example?: IExample;
    private _errorMessage: Optional<string> = null;

    constructor(
        private exampleService: ExampleService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate(params: any) {
        if (!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getExample(params.id);
        }
    }

    onSubmit(event: Event) {
        event.preventDefault();
        if (this._example) {
            this.delete(this._example);
        }
    }

    private getExample(id: string): void {
        if (UtilFunctions.existsAndIsString(id)) {
            this.exampleService
            .getPersonal(id)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {                    
                    this._example = response.data!;
                }
            })
            .catch((error) => {
                console.log(error);
            });
        }
    }

    private delete(example: IExample) {
        this.exampleService.delete(example.id).then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this._errorMessage = UtilFunctions.getErrorMessage(response);
            } else {
                this.router.navigateToRoute(Utils.EXAMPLES_ROUTE);
            }
        });
    }
}
