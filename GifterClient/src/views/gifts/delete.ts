import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { GiftService } from 'service/giftService';
import { IGiftEdit } from 'domain/IGiftEdit';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';

@autoinject
export class GiftsEdit {
    private _gift?: IGiftEdit;

    constructor(private giftService: GiftService, private router: Router) {
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

    onSubmit(event: Event) {
        // TODO: Confirmation asking
        this.giftService
        .deleteGift(this._gift!.id)
        .then(
            response => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('giftsIndex', {});
                } else {
                    UtilFunctions.alertErrorMessage(response);
                }
            }
        );
        event.preventDefault();
    }

}
