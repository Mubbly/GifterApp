import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IStatus } from 'domain/IStatus';
import { StatusService } from 'service/statusService';

@autoinject
export class StatusDetails {
    private _statuses: IStatus[] = [];
    private _status: Optional<IStatus> = null;

    constructor(private statusService: StatusService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.statusService.getStatus(params.id).then(
                data => this._status = data
            )
        }
    }
}
