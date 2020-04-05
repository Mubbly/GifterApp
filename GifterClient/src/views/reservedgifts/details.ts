import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IReservedGift } from 'domain/IReservedGift';
import { ReservedGiftService } from 'service/reservedGiftService';

@autoinject
export class ReservedGiftDetails {
    private _reservedGifts: IReservedGift[] = [];
    private _reservedGift: Optional<IReservedGift> = null;

    constructor(private reservedGiftService: ReservedGiftService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.reservedGiftService.getReservedGift(params.id).then(
                data => this._reservedGift = data
            )
        }
    }
}
