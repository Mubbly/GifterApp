import { autoinject } from 'aurelia-framework';
import { INotificationType } from 'domain/INotificationType';
import { NotificationTypeService } from 'service/notificationTypeService';

@autoinject
export class NotificationTypesIndex {
    private _notificationTypes: INotificationType[] = [];

    constructor(private notificationTypeService: NotificationTypeService) {

    }

    attached() {
        this.notificationTypeService.getNotificationTypes().then(
            data => this._notificationTypes = data
        );
    }
}
