import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { ExampleService } from "service/exampleService";
import * as UtilFunctions from "utils/utilFunctions";
import { IExampleCreate } from '../../domain/IExample';
import { Optional } from "types/generalTypes";
import { IFetchResponse } from "types/generalTypes";
import * as Utils from 'utils/utilFunctions';
import { AppState } from "state/appState";

@autoinject
export class ExamplesCreate {
    private _name = "";
    private _description = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private ExampleService: ExampleService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
    }

    // On submit create new Example
    onSubmit(event: Event) {
        event.preventDefault();

        let newExample: IExampleCreate = {
            name: this._name,
            description: Utils.setNullIfEmpty(this._description)
        };
        this.createExample(newExample);
    }

    private createExample(newExample: IExampleCreate): void {
        this.ExampleService
            .create(newExample)
            .then((response: IFetchResponse<IExampleCreate>) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(Utils.EXAMPLES_ROUTE, {});
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
