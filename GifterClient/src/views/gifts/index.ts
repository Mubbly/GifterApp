import { autoinject } from "aurelia-framework";
import { IGift } from "domain/IGift";
import { GiftService } from "service/giftService";
import * as UtilFunctions from "utils/utilFunctions";

@autoinject
export class GiftsIndex {
    private _gifts: IGift[] = [];

    constructor(private giftService: GiftService) {}

    attached() {
        this.getAllGifts();
    }

    private getAllGifts(): void {
        this.giftService.getGifts().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._gifts = response.data!;
            } else {
                UtilFunctions.alertErrorMessage(response);
            }
        });
    }
}
