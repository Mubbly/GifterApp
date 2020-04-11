import { autoinject } from "aurelia-framework";
import { IGift } from "domain/IGift";
import { GiftService } from "service/giftService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class GiftsIndex {
    private _gifts: IGift[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private giftService: GiftService) {}

    attached() {
        this.getAllGifts();
    }

    private getAllGifts(): void {
        this.giftService.getGifts().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._gifts = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
