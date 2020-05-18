import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { IProfile } from "domain/IProfile";
import { ProfileService } from "service/profileService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from '../../types/IFetchResponse';
import { AppState } from "state/appState";
import { AppUserService } from "service/base/appUserService";
import { IGift } from "domain/IGift";
import { GiftService } from "service/giftService";

@autoinject
export class ProfilesPersonal {
    //private _profiles: IProfile[] = [];
    private _profile: Optional<IProfile> = null;
    private _gifts: IGift[] = [];
    private _lastActiveDate: string = '';
    private _errorMessage: Optional<string> = null;

    constructor(
        private profileService: ProfileService,
        private appUserService: AppUserService,
        private giftService: GiftService,
        private router: Router,
        private appState: AppState
    ) {
    }

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getPersonalProfile();
            this.getPersonalGifts();
        }
    }

    /**
     * Get user's personal profile. Default initial one if not edited yet.
     */
    private getPersonalProfile(): Promise<void> {
        return this.profileService
            .getPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._profile = response.data!;
                    this._lastActiveDate = Utils.formatAsHtml5Date(this._profile.appUser.lastActive);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private getPersonalGifts(): Promise<void> {
        return this.giftService
            .getAllPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._gifts = response.data!;
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

