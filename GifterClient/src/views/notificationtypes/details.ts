import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { INotificationType } from 'domain/INotificationType';
import { NotificationTypeService } from 'service/notificationTypeService';

@autoinject
export class NotificationTypeDetails {
    private _notificationTypes: INotificationType[] = [];
    private _notificationType: Optional<INotificationType> = null;

    constructor(private notificationTypeService: NotificationTypeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.notificationTypeService.getNotificationType(params.id).then(
                data => this._notificationType = data
            )
        }
    }
}
