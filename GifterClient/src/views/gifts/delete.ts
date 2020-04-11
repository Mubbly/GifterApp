import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { GiftService } from 'service/giftService';
import * as UtilFunctions from 'utils/utilFunctions';
import { IGift } from 'domain/IGift';
import { Optional } from 'types/generalTypes';

@autoinject
export class GiftsDelete {
    private _gift?: IGift;
    private _errorMessage: Optional<string> = null;

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
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }

    onSubmit(event: Event) {
        this.giftService
        .deleteGift(this._gift!.id)
        .then(
            response => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('giftsIndex', {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);

                }
            }
        );
        event.preventDefault();
    }
}
