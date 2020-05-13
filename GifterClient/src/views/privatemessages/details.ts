import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IPrivateMessage } from 'domain/IPrivateMessage';
import { PrivateMessageService } from 'service/privateMessageService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class PrivateMessageDetails {
    private _privateMessage: Optional<IPrivateMessage> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private privateMessageService: PrivateMessageService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getPrivateMessage(params.id);
    }

    private getPrivateMessage(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.privateMessageService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._privateMessage = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
