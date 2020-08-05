import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { ExampleService } from "service/exampleService";
import { IExampleEdit } from "domain/IExample";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";
import { IFetchResponse } from "types/generalTypes";
import { AppState } from "state/appState";
import * as Utils from "utils/utilFunctions";

@autoinject
export class ExamplesEdit {
    private _example?: IExampleEdit;
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

        const editedExample = this._example;
        if (!editedExample) {
            return;
        }
        // Name input is required
        let nameInput = <string>editedExample.name;
        if (nameInput == null || nameInput.length === 0) {
            alert("Name missing!");
            return;
        }
        // Desc input is optional
        let descInput = <Optional<string>>editedExample.description;
        let desc: Optional<string> =
            descInput === null || descInput.length <= 0 ? null : descInput;
        editedExample.description = desc;

        this.updateExample(editedExample);
    }

    private getExample(id: string): void {
        if (UtilFunctions.existsAndIsString(id)) {
            this.exampleService.getPersonal(id).then((response) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(
                        response
                    );
                } else {
                    if (response.data) {
                        // Leave appUser data out
                        this._example = {
                            name: response.data!.name,
                            description: response.data!.description,
                            id: response.data!.id,
                        };
                    }
                }
            });
        }
    }

    private updateExample(example: IExampleEdit): void {
        this.exampleService
            .update(example)
            .then((response: IFetchResponse<IExampleEdit>) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(
                        response
                    );
                } else {
                    this.router.navigateToRoute(Utils.EXAMPLES_ROUTE, {});
                }
            });
    }
}
