import { autoinject } from 'aurelia-framework';
import { IArchivedGift } from 'domain/IArchivedGift';
import { ArchivedGiftService } from 'service/archivedGiftService';

@autoinject
export class ArchivedGiftsIndex {
    private _archivedGifts: IArchivedGift[] = [];

    constructor(private archivedGiftService: ArchivedGiftService) {

    }

    attached() {
        this.archivedGiftService.getArchivedGifts().then(
            data => this._archivedGifts = data
        );
    }
}
