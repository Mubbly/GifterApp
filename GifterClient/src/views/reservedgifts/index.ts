import { autoinject } from 'aurelia-framework';
import { IReservedGift } from 'domain/IReservedGift';
import { ReservedGiftService } from 'service/reservedGiftService';

@autoinject
export class ReservedGiftsIndex {
    private _reservedGifts: IReservedGift[] = [];

    constructor(private reservedGiftService: ReservedGiftService) {

    }

    attached() {
        this.reservedGiftService.getReservedGifts().then(
            data => this._reservedGifts = data
        );
    }
}
