import { autoinject } from 'aurelia-framework';
import { INotification } from 'domain/INotification';
import { NotificationService } from 'service/notificationService';

@autoinject
export class NotificationsIndex {
    private _notifications: INotification[] = [];

    constructor(private notificationService: NotificationService) {

    }

    attached() {
        this.notificationService.getNotifications().then(
            data => this._notifications = data
        );
    }
}
