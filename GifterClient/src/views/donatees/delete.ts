import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { DonateeService } from 'service/donateeService';
import * as UtilFunctions from 'utils/utilFunctions';
import { IDonatee } from 'domain/IDonatee';
import { Optional } from 'types/generalTypes';

@autoinject
export class DonateesDelete {
    private _donatee?: IDonatee;
    private _errorMessage: Optional<string> = null;

    constructor(private donateeService: DonateeService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const donateeId = params.id;
        if(UtilFunctions.existsAndIsString(donateeId)) {
            this.donateeService.get(donateeId).then(
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

    onSubmit(event: Event) {
        this.donateeService
        .delete(this._donatee!.id)
        .then(
            response => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('donateesIndex', {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);

                }
            }
        );
        event.preventDefault();
    }
}
