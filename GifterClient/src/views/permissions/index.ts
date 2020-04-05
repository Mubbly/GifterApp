import { autoinject } from 'aurelia-framework';
import { IPermission } from 'domain/IPermission';
import { PermissionService } from 'service/permissionService';

@autoinject
export class PermissionsIndex {
    private _permissions: IPermission[] = [];

    constructor(private permissionService: PermissionService) {

    }

    attached() {
        this.permissionService.getPermissions().then(
            data => this._permissions = data
        );
    }
}
