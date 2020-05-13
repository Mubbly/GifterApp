import { autoinject } from "aurelia-framework";
import { IPermission } from "domain/IPermission";
import { PermissionService } from "service/permissionService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class PermissionsIndex {
    private _reservedPermissions: IPermission[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedPermissionService: PermissionService) {}

    attached() {
        this.getPermissions();
    }

    private getPermissions(): void {
        this.reservedPermissionService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedPermissions = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
