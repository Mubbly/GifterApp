import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { INotification } from 'domain/INotification';
import { NotificationService } from 'service/notificationService';

@autoinject
export class NotificationDetails {
    private _notifications: INotification[] = [];
    private _notification: Optional<INotification> = null;

    constructor(private notificationService: NotificationService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.notificationService.getNotification(params.id).then(
                data => this._notification = data
            )
        }
    }
}
