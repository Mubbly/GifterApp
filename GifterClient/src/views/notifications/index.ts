import { autoinject } from "aurelia-framework";
import { INotification } from "domain/INotification";
import { NotificationService } from "service/notificationService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class NotificationsIndex {
    private _reservedNotifications: INotification[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedNotificationService: NotificationService) {}

    attached() {
        this.getNotifications();
    }

    private getNotifications(): void {
        this.reservedNotificationService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedNotifications = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
