import { autoinject } from "aurelia-framework";
import { INotificationType } from "domain/INotificationType";
import { NotificationTypeService } from "service/notificationTypeService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class NotificationTypesIndex {
    private _reservedNotificationTypes: INotificationType[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedNotificationTypeService: NotificationTypeService) {}

    attached() {
        this.getNotificationTypes();
    }

    private getNotificationTypes(): void {
        this.reservedNotificationTypeService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedNotificationTypes = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
