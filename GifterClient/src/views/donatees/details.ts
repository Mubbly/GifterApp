import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IDonatee } from 'domain/IDonatee';
import { DonateeService } from 'service/donateeService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class DonateeDetails {
    private _donatee: Optional<IDonatee> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private donateeService: DonateeService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getDonatee(params.id);
    }

    private getDonatee(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.donateeService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._donatee = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);
                    }
                }
            )
        }
    }
}
