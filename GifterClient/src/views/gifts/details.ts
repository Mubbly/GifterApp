import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IGift } from 'domain/IGift';
import { GiftService } from 'service/giftService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class GiftDetails {
    private _gift: Optional<IGift> = null;

    constructor(private giftService: GiftService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const giftId = params.id;
        if(UtilFunctions.existsAndIsString(giftId)) {
            this.giftService.getGift(giftId).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._gift = response.data!;
                    } else {
                        UtilFunctions.alertErrorMessage(response);
                    }
                }
            )
        }
    }
}
