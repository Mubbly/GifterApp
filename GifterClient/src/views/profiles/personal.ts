import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { IProfile } from "domain/IProfile";
import { ProfileService } from "service/profileService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { AppState } from "state/appState";
import { AppUserService } from "service/base/appUserService";
import { IGift } from "domain/IGift";
import { GiftService } from "service/giftService";
import { IAppUser } from "domain/IAppUser";
import { WishlistService } from "service/wishlistService";
import { IWishlist } from '../../domain/IWishlist';
import { IFetchResponse } from "types/IFetchResponse";

@autoinject
export class ProfilesPersonal {
    //private _profiles: IProfile[] = [];
    private readonly EMPTY_WISHLIST_MESSAGE = 'Your wishlist seems to be empty :( Add gifts to get started!';
    private _profile: Optional<IProfile> = null;
    private _appUser: Optional<IAppUser> = null;
    private _wishlist: Optional<IWishlist> = null;
    private _gifts: IGift[] = [];
    private _lastActiveDate: string = '';
    private _emptyWishlistMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    private _showEmail: boolean = false;
    private _profileBannerUrl: Optional<string> = null;

    constructor(
        private profileService: ProfileService,
        private appUserService: AppUserService,
        private wishlistService: WishlistService,
        private giftService: GiftService,
        private router: Router,
        private appState: AppState
    ) {
    }

    attached() {}

    activate(props: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            if(props.showEmail) {
                this._showEmail = props.showEmail;
            }
            if(props.profileBannerUrl) {
                this._profileBannerUrl = props.profileBannerUrl;
            }
            this.getCurrentAppUser();
            this.getPersonalProfile();
            this.getPersonalWishlist();
            this.getPersonalGifts();
        }
    }

    private getCurrentAppUser(): void {
        this.appUserService
        .getCurrentUser()
        .then((response) => {
            if(Utils.isSuccessful(response) && response.data) {
                this._appUser = response.data;
                this._lastActiveDate = Utils.formatAsHtml5Date(this._appUser.lastActive);
            } else {
                this.handleErrors(response);
            }
        })
        .catch((error) => {
            console.log(error);
        });
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
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private getPersonalWishlist(): Promise<void> {
        return this.wishlistService
            .getPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._wishlist = response.data!;
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
                    if(this._gifts.length <= 0) {
                        this._emptyWishlistMessage = this.EMPTY_WISHLIST_MESSAGE; 
                    } else {
                        this._emptyWishlistMessage = null; 
                    }
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

