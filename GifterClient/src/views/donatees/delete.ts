import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { DonateeService } from 'service/donateeService';
import * as UtilFunctions from 'utils/utilFunctions';
import { IDonatee } from 'domain/IDonatee';

@autoinject
export class DonateesDelete {
    private _donatee?: IDonatee;

    constructor(private donateeService: DonateeService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const donateeId = params.id;
        if(UtilFunctions.existsAndIsString(donateeId)) {
            this.donateeService.getDonatee(donateeId).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._donatee = response.data!;
                    } else {
                        UtilFunctions.alertErrorMessage(response);
                    }
                }
            )
        }
    }

    onSubmit(event: Event) {
        this.donateeService
        .deleteDonatee(this._donatee!.id)
        .then(
            response => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('donateesIndex', {});
                } else {
                    UtilFunctions.alertErrorMessage(response);
                }
            }
        );
        event.preventDefault();
    }
}
