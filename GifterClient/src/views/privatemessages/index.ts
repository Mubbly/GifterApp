import { autoinject } from "aurelia-framework";
import { IPrivateMessage } from "domain/IPrivateMessage";
import { PrivateMessageService } from "service/privateMessageService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class PrivateMessagesIndex {
    private _reservedPrivateMessages: IPrivateMessage[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedPrivateMessageService: PrivateMessageService) {}

    attached() {
        this.getPrivateMessages();
    }

    private getPrivateMessages(): void {
        this.reservedPrivateMessageService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedPrivateMessages = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
