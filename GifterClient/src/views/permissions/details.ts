import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IPermission } from 'domain/IPermission';
import { PermissionService } from 'service/permissionService';

@autoinject
export class PermissionDetails {
    private _permissions: IPermission[] = [];
    private _permission: Optional<IPermission> = null;

    constructor(private permissionService: PermissionService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.permissionService.getPermission(params.id).then(
                data => this._permission = data
            )
        }
    }
}
