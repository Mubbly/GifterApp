import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { INotification } from 'domain/INotification';
import { NotificationService } from 'service/notificationService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class NotificationDetails {
    private _notification: Optional<INotification> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private notificationService: NotificationService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getNotification(params.id);
    }

    private getNotification(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.notificationService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._notification = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
