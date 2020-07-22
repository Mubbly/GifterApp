import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import { IArchivedGift } from "domain/IArchivedGift";
import { ArchivedGiftService } from "service/archivedGiftService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { GiftService } from "service/giftService";
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { IGift } from '../../domain/IGift';

@autoinject
export class ArchivedGiftsIndex {
    private _givenGifts: IGift[] = [];
    private _receivedGifts: IGift[] = [];
    private _pendingReceivedGifts: IGift[] = [];

    private _noGivenGiftsMessage: Optional<string> = null;
    private _noReceivedGiftsMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private reservedArchivedGiftService: ArchivedGiftService,
        private giftService: GiftService,
        private router: Router, 
        private appState: AppState) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getGivenGifts();
        this.getPendingReceivedGifts();
        this.getReceivedGifts();
    }

    onConfirmReceiving(event: Event, gift: IGift) {
        event.preventDefault();
        this.confirmReceiving(gift);
    }

    onDenyReceiving(event: Event, gift: IGift) {
        event.preventDefault();
        this.denyReceiving(gift);
    }

    onReactivate(event: Event, gift: IGift) {
        event.preventDefault();
        this.reactivate(gift);
    }

    onDelete(event: Event, gift: IGift) {
        event.preventDefault();
        this.delete(gift);
    }

    private getGivenGifts(): void {
        this.giftService
        .getAllGivenArchived()
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(!response.data || response.data.length <= 0) {
                    this._noGivenGiftsMessage = "No given gifts";
                    return;
                }
                this._givenGifts = response.data;
                this._givenGifts.forEach(gift => {
                    if(gift.archivedFrom) {
                        gift.archivedFrom = Utils.formatAsHtml5Date(gift.archivedFrom);
                    }
                });
                this._noGivenGiftsMessage = null;
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private getPendingReceivedGifts(): void {
        this.giftService
        .getAllPendingReceivedArchived()
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(!response.data || response.data.length <= 0) {
                    return;
                }
                this._pendingReceivedGifts = response.data;
                this._pendingReceivedGifts.forEach(gift => {
                    if(gift.archivedFrom) {
                        gift.archivedFrom = Utils.formatAsHtml5Date(gift.archivedFrom);
                    }
                });
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private getReceivedGifts(): void {
        this.giftService
        .getAllReceivedArchived()
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                if(!response.data || response.data.length <= 0) {
                    this._noReceivedGiftsMessage = "No received gifts";
                    return;
                }
                this._receivedGifts = response.data;
                this._receivedGifts.forEach(gift => {
                    if(gift.archivedFrom) {
                        gift.archivedFrom = Utils.formatAsHtml5Date(gift.archivedFrom);
                    }
                });
                this._noReceivedGiftsMessage = null;
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private confirmReceiving(gift: IGift): void {
        this.giftService
        .confirmArchival(gift)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                Utils.refreshPage(); // TODO: Some better approach to update data on the page?
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private denyReceiving(gift: IGift): void {
        this.giftService
        .denyArchival(gift)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                Utils.refreshPage(); // TODO: Some better approach to update data on the page?
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private reactivate(gift: IGift): void {
        this.giftService
        .reactivateArchived(gift)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this.router.navigateToRoute(Utils.PERSONAL_PROFILE_ROUTE);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private delete(gift: IGift): void {
        this.giftService
        .deleteArchived(gift)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                Utils.refreshPage(); // TODO: Some better approach to update data on the page?
            }
        })
        .catch((error) => {
            console.log(error);
        });
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
