import { autoinject } from "aurelia-framework";
import { IReservedGift } from "domain/IReservedGift";
import { ReservedGiftService } from "service/reservedGiftService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class ReservedGiftsIndex {
    private _reservedReservedGifts: IReservedGift[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedReservedGiftService: ReservedGiftService) {}

    attached() {
        this.getReservedGifts();
    }

    private getReservedGifts(): void {
        this.reservedReservedGiftService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedReservedGifts = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
