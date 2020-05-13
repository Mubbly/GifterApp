import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IGift } from 'domain/IGift';
import { GiftService } from 'service/giftService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class GiftDetails {
    private _gift: Optional<IGift> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private giftService: GiftService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getGift(params.id);
    }

    private getGift(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.giftService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._gift = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
