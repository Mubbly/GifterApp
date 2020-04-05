import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IPrivateMessage } from 'domain/IPrivateMessage';
import { PrivateMessageService } from 'service/privateMessageService';

@autoinject
export class PrivateMessageDetails {
    private _privateMessages: IPrivateMessage[] = [];
    private _privateMessage: Optional<IPrivateMessage> = null;

    constructor(private privateMessageService: PrivateMessageService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.privateMessageService.getPrivateMessage(params.id).then(
                data => this._privateMessage = data
            )
        }
    }
}
