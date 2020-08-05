import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { Optional, IFetchResponse, ExamInterface } from "types/generalTypes";
import { IExample } from "domain/IExample";
import { ExampleService } from "service/exampleService";
import * as UtilFunctions from "utils/utilFunctions";
import { AppState } from "state/appState";
import * as Utils from "utils/utilFunctions";

@autoinject
export class ExampleDetails {
    private _example: Optional<IExample> = null;
    private _exampleCreatorName: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private appState: AppState,
        private router: Router,
        private exampleService: ExampleService
    ) {}

    attached() {}

    activate(params: any) {
        if (!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getExample(params.id);
        }
    }

    private getExample(id: string): void {
        if (UtilFunctions.existsAndIsString(id)) {
            this.exampleService
            .get(id)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {                    
                    this._example = response.data!;
                    // Get creator user name
                    if(response.data!.appUser) {
                        this._exampleCreatorName = response.data!.appUser!.fullName;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
        }
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<ExamInterface | ExamInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
