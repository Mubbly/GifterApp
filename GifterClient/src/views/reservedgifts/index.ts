import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { GiftService } from '../../service/giftService';
import { IGift } from "domain/IGift";
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class ReservedGiftsIndex {
    private readonly ERROR_NO_RESERVED_GIFTS = "No currently reserved gifts. To see gifted ones go to Archive.";

    private _reservedGifts: IGift[] = [];
    private _errorMessage: Optional<string> = null;
    private _currentDate: string = Utils.formatAsHtml5Date(new Date().toUTCString());

    private _noReservationsMessage: Optional<string> = null;

    constructor(private giftService: GiftService,
        private router: Router, 
        private appState: AppState) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getPersonalReservedGifts();
    }

    onMarkAsGifted(event: Event, gift: IGift) {
        event.preventDefault();
        this.archive(gift);
    }

    onCancelReservation(event: Event, gift: IGift) {
        event.preventDefault();
        this.cancelReservation(gift);
    }

    private getPersonalReservedGifts(): Promise<void> {
        return this.giftService
        .getAllPersonalReserved()
        .then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(!response.data || response.data.length <= 0) {
                    this._noReservationsMessage = this.ERROR_NO_RESERVED_GIFTS;
                    return;
                }
                this._reservedGifts = response.data!;
                this._reservedGifts.forEach(gift => {
                    if(gift.reservedFrom) {
                        gift.reservedFrom = Utils.formatAsHtml5Date(gift.reservedFrom);
                    }
                });
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /** Sets gift status to archived */
    private archive(gift: IGift) {
        const giftOwnerId = gift.userReceiverId;
        if(giftOwnerId) {
            this.giftService
            .updateToGiftedStatus(gift, giftOwnerId)
            .then((response) => {
                if(!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    Utils.refreshPage(); // To getFullProfile again - with updated gifts
                }
            })
            .catch((error) => {
                console.log(error);
            });
        }
    }
 
    /** Sets gift status back to active */
    private cancelReservation(gift: IGift) {
        const giftOwnerId = gift.userReceiverId;
        if(giftOwnerId) {
            this.giftService
            .cancelReservation(gift, giftOwnerId)
            .then((response) => {
                if(!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    Utils.refreshPage(); // To getFullProfile again - with updated gifts
                }
            })
            .catch((error) => {
                console.log(error);
            });
        }
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
