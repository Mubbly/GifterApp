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
import { IFetchResponse } from "types/IFetchResponse";
import { IWishlist } from "domain/IWishlist";
import { FriendshipService } from '../../service/friendshipService';

@autoinject
export class ProfilesIndex {
    private readonly EMPTY_WISHLIST_MESSAGE = "This user's wishlist seems to be empty! :(";
    private readonly MESSAGE_FRIEND_REQUEST_SENT = 'Friend request sent';
    private readonly MESSAGE_FRIENDSHIP_DELETED = "Friendship deleted";
    private readonly ERROR_PROFILE_NOT_FOUND = "404 Not Found";

    private _profile: Optional<IProfile> = null;
    private _currentUser: Optional<IAppUser> = null;
    private _profileOwner: Optional<IAppUser> = null;
    private _wishlist: Optional<IWishlist> = null;
    private _gifts: Optional<IGift[]> = null;
    private _successMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    /** Either confirmed or pending relationship is present */ 
    private _isFriend: boolean = false;
    private _isConfirmedFriend: boolean = false;

    private _lastActiveDate: string = '';
    private _emptyWishlistMessage: Optional<string> = null;

    // TODO: These need to go to the db!
    private _showEmail: boolean = false;
    private _profileBannerUrl: Optional<string> = null;

    constructor(
        private profileService: ProfileService,
        private appUserService: AppUserService,
        private wishlistService: WishlistService,
        private giftService: GiftService,
        private friendshipService: FriendshipService,
        private router: Router,
        private appState: AppState
    ) {
    }

    attached() {}

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            if(params.userId) {
                if(params.userId === this.appState.userId) {
                    this.router.navigateToRoute(Utils.PERSONAL_PROFILE_ROUTE);
                } else {
                    // this.getFullRequestedProfile(params.userId);
                    this.getFullProfile(params.userId);
                }
            } else {
                this._errorMessage = this.ERROR_PROFILE_NOT_FOUND;
            }
        }
    }

    onAddFriend(event: Event, friendId: string) {
        event.preventDefault();
        this.sendFriendRequest(friendId);
    }

    onUnfriend(event: Event, friendId: string) {
        event.preventDefault();
        this.deleteFriendship(friendId);
    }

    private sendFriendRequest(friendId: string) {
        this.friendshipService
        .createPending(friendId)
        .then((response) => {
            if(Utils.isSuccessful(response)) {
                this._isFriend = true;
                this._successMessage = this.MESSAGE_FRIEND_REQUEST_SENT;
            } else {
                this._errorMessage = Utils.getErrorMessage(response);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /** Delete existing confirmed friendship or pending request */
    private deleteFriendship(friendId: string): void {
        this.friendshipService
        .delete(friendId)
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._isFriend = false;
                this._successMessage = this.MESSAGE_FRIENDSHIP_DELETED;
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Get user's profile. Default initial one if not edited yet.
     */
    private getFullProfile(userId: string): Promise<void> {
        return this.profileService
            .getFullForUser(userId)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._profile = response.data!;
                    // if(this._profile) {
                    //     this._lastActiveDate = Utils.formatAsHtml5Date(this._profileOwner!.lastActive);

                    //     // this.getWishlist(this._profile.wishlistId);
                    //     // this.getGifts(userId);
                    // }
                    if(this._profile) {
                        this._wishlist = this._profile.wishlist;
                        this._profileOwner = this._profile.appUser;
                        this._gifts = this._profile.wishlist.gifts;

                        let noGiftsInWishlist: boolean = !this._gifts || (this._gifts && this._gifts?.length <= 0);
                        if(noGiftsInWishlist) {
                            this._emptyWishlistMessage = this.EMPTY_WISHLIST_MESSAGE;
                        } else {
                            this._emptyWishlistMessage = null;
                        }

                        this.getFriendship(userId);
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private getFriendship(userId: string): Promise<void> {
        return this.friendshipService
            .getPersonal(userId)
            .then((response) => {
                // Either friend or not
                this._isFriend = Utils.isSuccessful(response);

                // Either confirmed or pending friendship
                if(this._isFriend) {
                    this._isConfirmedFriend = response.data !== undefined && response.data.isConfirmed;
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    // private getFullRequestedProfile(profileOwnerId: string): void {
    //     this.getRequestingUser();
    //     this.getProfileOwner(profileOwnerId);
    // }

    // private getRequestingUser(): void {
    //     this.appUserService
    //         .getCurrentUser()
    //         .then((response) => {
    //             if(Utils.isSuccessful(response) && response.data) {
    //                 this._currentUser = response.data;
    //             } else {
    //                 this.handleErrors(response);
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

    // private getProfileOwner(userId: string): Promise<void> {
    //     return this.appUserService
    //         .getUser(userId)
    //         .then((response) => {
    //             if(Utils.isSuccessful(response) && response.data) {
    //                 this._profileOwner = response.data;
    //                 // If user is found, get requested profile and related items
    //                 if(this._profileOwner && this._profileOwner.id === userId) {
    //                     this.getProfile(userId);
    //                 }
    //             } else {
    //                 this.handleErrors(response);
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

    // /**
    //  * Get user's profile. Default initial one if not edited yet.
    //  */
    // private getProfile(userId: string): Promise<void> {
    //     return this.profileService
    //         .getUserProfile(userId)
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {                    
    //                 this._profile = response.data!;
    //                 if(this._profile) {
    //                     this._lastActiveDate = Utils.formatAsHtml5Date(this._profileOwner!.lastActive);

    //                     this.getWishlist(this._profile.wishlistId);
    //                     this.getGifts(userId);
    //                 }
    //                 console.log(this._profile.id);
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

    // private getWishlist(wishlistId: string): Promise<void> {
    //     return this.wishlistService
    //         .get(wishlistId)
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {                    
    //                 this._wishlist = response.data!;
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

    // private getGifts(userId: string): Promise<void> {
    //     return this.giftService
    //         .getAllForUser(userId)
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {                    
    //                 this._gifts = response.data!;
    //                 if(!this._gifts || this._gifts.length <= 0) {
    //                     this._emptyWishlistMessage = this.EMPTY_WISHLIST_MESSAGE;
    //                 } else {
    //                     this._emptyWishlistMessage = null;
    //                 }
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

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
