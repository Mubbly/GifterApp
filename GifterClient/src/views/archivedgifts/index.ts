import { autoinject } from "aurelia-framework";
import { IArchivedGift } from "domain/IArchivedGift";
import { ArchivedGiftService } from "service/archivedGiftService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class ArchivedGiftsIndex {
    private _reservedArchivedGifts: IArchivedGift[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedArchivedGiftService: ArchivedGiftService) {}

    attached() {
        this.getArchivedGifts();
    }

    private getArchivedGifts(): void {
        this.reservedArchivedGiftService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedArchivedGifts = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
