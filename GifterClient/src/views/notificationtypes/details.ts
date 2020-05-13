import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { INotificationType } from 'domain/INotificationType';
import { NotificationTypeService } from 'service/notificationTypeService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class NotificationTypeDetails {
    private _notificationType: Optional<INotificationType> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private notificationTypeService: NotificationTypeService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getNotificationType(params.id);
    }

    private getNotificationType(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.notificationTypeService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._notificationType = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
