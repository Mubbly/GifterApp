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
    private _appUserId = "";
    private _wishlistId = "";
    // related tables
    private _appUsers: IAppUser[] = [];
    private _statuses: IStatus[] = [];
    private _actionTypes: IActionType[] = [];

    constructor(
        private giftService: GiftService,
        private appUserService: AppUserService,
        private statusService: StatusService,
        private actionTypeService: ActionTypeService,
        private WishlistService: WishlistService,
        private router: Router
    ) {}

    attached() {
        this.getRelatedData();
    }

    onSubmit(event: Event) {
        let newGift: IGiftCreate = {
            name: this._name,
            description: this._description,
            image: this._image,
            url: this._url,
            partnerUrl: this._partnerUrl,
            isPartnered: this._isPartnered,
            isPinned: this._isPinned,
            actionTypeId: this._actionTypeId,
            statusId: this._statusId,
            appUserId: this._appUserId,
            wishlistId: this._wishlistId
        };
        console.log(newGift);
        this.createGift(newGift);

        event.preventDefault();
    }

    // From other tables that are connected to this one via foreign keys
    private getRelatedData(): void {

    }

    private createGift(newGift: IGiftCreate): void {
        this.giftService
            .create(newGift)
            .then((response: IFetchResponse<IGiftCreate>) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute("giftsIndex", {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);

                }
            });
    }
}
