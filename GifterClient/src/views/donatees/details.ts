import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IDonatee } from 'domain/IDonatee';
import { DonateeService } from 'service/donateeService';

@autoinject
export class DonateeDetails {
    private _donatees: IDonatee[] = [];
    private _donatee: Optional<IDonatee> = null;

    constructor(private donateeService: DonateeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.donateeService.getDonatee(params.id).then(
                data => this._donatee = data
            )
        }
    }
}
