import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AppState } from "state/appState";
import { AppUserService } from 'service/base/appUserService';
import { NotificationService } from 'service/notificationService';
import { CampaignService } from 'service/campaignService';
import * as Utils from 'utils/utilFunctions';
import { ProfileService } from 'service/profileService';
import { Optional } from 'types/generalTypes';
import { IProfile } from 'domain/IProfile';
import { IAppUser } from 'domain/IAppUser';
import { IWishlist } from 'domain/IWishlist';
import { IGift } from 'domain/IGift';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class HomeIndex {
    private readonly DEFAULT_NAME = 'friend';
    private _userFullName: Optional<string> = '';
    private _profile: Optional<IProfile> = null;
    private _wishlist: Optional<IWishlist> = null;
    private _appUser: Optional<IAppUser> = null;

    constructor(private appState: AppState, 
        private router: Router,
        private appUserService: AppUserService,
        private profileService: ProfileService,
        private notificationService: NotificationService,
        private campaignService: CampaignService) {
    }

    activate(props: any) {
        if(this.appState.jwt) {
            this.getPersonalFullProfile();
            this._userFullName = this.appState.userFullName ? this.appState.userFullName : this.DEFAULT_NAME;
        }
    }

    /**
     * Get user's personal profile. Default initial one if not edited yet.
     */
    private getPersonalFullProfile(): void {
        this.profileService
            .getFullPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    return;
                } else {                    
                    this._profile = response.data!;
                    if(this._profile) {
                        this._wishlist = this._profile.wishlist;
                        this._appUser = this._profile.appUser;
                        console.log(this._wishlist.id);
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
