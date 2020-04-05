import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IUserNotification } from 'domain/IUserNotification';
import { UserNotificationService } from 'service/userNotificationService';

@autoinject
export class UserNotificationDetails {
    private _userNotifications: IUserNotification[] = [];
    private _userNotification: Optional<IUserNotification> = null;

    constructor(private userNotificationService: UserNotificationService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.userNotificationService.getUserNotification(params.id).then(
                data => this._userNotification = data
            )
        }
    }
}
