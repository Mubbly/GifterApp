import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IArchivedGift } from 'domain/IArchivedGift';
import { ArchivedGiftService } from 'service/archivedGiftService';

@autoinject
export class ArchivedGiftDetails {
    private _archivedGifts: IArchivedGift[] = [];
    private _archivedGift: Optional<IArchivedGift> = null;

    constructor(private archivedGiftService: ArchivedGiftService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.archivedGiftService.getArchivedGift(params.id).then(
                data => this._archivedGift = data
            )
        }
    }
}
