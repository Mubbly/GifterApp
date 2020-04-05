import { autoinject } from 'aurelia-framework';
import { IPrivateMessage } from 'domain/IPrivateMessage';
import { PrivateMessageService } from 'service/privateMessageService';

@autoinject
export class PrivateMessagesIndex {
    private _privateMessages: IPrivateMessage[] = [];

    constructor(private privateMessageService: PrivateMessageService) {

    }

    attached() {
        this.privateMessageService.getPrivateMessages().then(
            data => this._privateMessages = data
        );
    }
}
