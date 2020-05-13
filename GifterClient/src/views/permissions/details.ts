import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IPermission } from 'domain/IPermission';
import { PermissionService } from 'service/permissionService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class PermissionDetails {
    private _permission: Optional<IPermission> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private permissionService: PermissionService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getPermission(params.id);
    }

    private getPermission(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.permissionService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._permission = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
