import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { GiftService } from "service/giftService";
import { AppUserService } from "service/base/appUserService";
import * as UtilFunctions from "utils/utilFunctions";
import { IAppUser } from "domain/IAppUser";
import { IStatus } from "domain/IStatus";
import { IActionType } from "domain/IActionType";
import { StatusService } from "service/statusService";
import { ActionTypeService } from "service/actionTypeService";
import { IGiftCreate } from '../../domain/IGift';
import { Optional } from "types/generalTypes";
import { WishlistService } from '../../service/wishlistService';
import { IFetchResponse } from "types/IFetchResponse";
import { ActionTypes, Statuses } from "domain/PredefinedData";
import { PermissionDetails } from '../permissions/details';
import * as Utils from 'utils/utilFunctions';
import { AppState } from "state/appState";

@autoinject
export class GiftsCreate {
    private _gift?: IGiftCreate;
    private _errorMessage: Optional<string> = null;

    private _name = "";
    private _description = null;
    private _image = null;
    private _url = null;
    private _partnerUrl = null;
    private _isPartnered = false;
    private _isPinned = false;
    private _actionTypeId = "";
    private _statusId = "";
    private _wishlistId = "";
    // related tables
    private _statuses: IStatus[] = [];
    private _actionTypes: IActionType[] = [];

    constructor(
        private giftService: GiftService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getRelatedData(params);
        }
    }

    // From other tables that are connected to this one via foreign keys
    private getRelatedData(params: any): void {
        this._wishlistId = params.wishlistId;
        this._statusId = Statuses.ACTIVE;
        this._actionTypeId = ActionTypes.RESERVE;
    }

    // On submit create new gift
    onSubmit(event: Event) {
        event.preventDefault();

        let newGift: IGiftCreate = {
            name: this._name,
            description: Utils.setNullIfEmpty(this._description),
            image: Utils.setNullIfEmpty(this._image),
            url: Utils.setNullIfEmpty(this._url),
            partnerUrl: Utils.setNullIfEmpty(this._partnerUrl),
            isPartnered: this._isPartnered,
            isPinned: this._isPinned,
            actionTypeId: this._actionTypeId,
            statusId: this._statusId,
            wishlistId: this._wishlistId
        };
        this.createGift(newGift);
    }

    private createGift(newGift: IGiftCreate): void {
        this.giftService
            .create(newGift)
            .then((response: IFetchResponse<IGiftCreate>) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute("profilesPersonal", {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
