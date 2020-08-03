import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { IProfile } from "domain/IProfile";
import { ProfileService } from "service/profileService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { AppState } from "state/appState";
import { AppUserService } from "service/base/appUserService";
import { IGift, IGiftEdit, IGiftCreate } from "domain/IGift";
import { GiftService } from "service/giftService";
import { IAppUser } from "domain/IAppUser";
import { WishlistService } from "service/wishlistService";
import { IFetchResponse } from "types/IFetchResponse";
import { IWishlist } from "domain/IWishlist";
import { FriendshipService } from '../../service/friendshipService';
import { ReservedGiftService } from "service/reservedGiftService";
import { IReservedGiftCreate } from "domain/IReservedGift";
import { ACTION_TYPES, ARCHIVED_GIFTS } from '../../utils/apiEndpointUrls';
import { profile } from "console";
import { Statuses } from "domain/PredefinedData";

@autoinject
export class ProfilesIndex {
    private readonly EMPTY_WISHLIST_MESSAGE = "This user's wishlist seems to be empty! :(";
    private readonly MESSAGE_FRIEND_REQUEST_SENT = 'Friend request sent';
    private readonly MESSAGE_FRIENDSHIP_DELETED = "Friendship deleted";
    private readonly ERROR_PROFILE_NOT_FOUND = "404 Not Found";
    private readonly ACTIVE_STATUS = Statuses.ACTIVE;
    private readonly RESERVED_STATUS = Statuses.RESERVED;
    private readonly ARCHIVED_STATUS = Statuses.ARCHIVED;

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

    private _showLoader: boolean = false;

    constructor(
        private profileService: ProfileService,
        private appUserService: AppUserService,
        private wishlistService: WishlistService,
        private giftService: GiftService,
        private reservedGiftService: ReservedGiftService,
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

    onReserveGift(event: Event, gift: IGift) {
        event.preventDefault();
        this.reserve(gift);
    }

    onCancelReservation(event: Event, gift: IGift) {
        event.preventDefault();
        this.cancelReservation(gift);
    }

    onMarkAsGifted(event: Event, gift: IGift) {
        event.preventDefault();
        this.archive(gift);
    }

     /** Get user's full profile including wishlist and gifts if there are any. Default initial one if not edited yet. */
    private getFullProfile(userId: string): Promise<void> {
        this._showLoader = true;
        return this.profileService
            .getFullForUser(userId)
            .then((response) => {
                this._showLoader = false;
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
                            this._gifts!.forEach(gift => {
                                if(gift.reservedFrom) {
                                    gift.reservedFrom = Utils.formatAsHtml5Date(gift.reservedFrom);
                                }
                            });
                            this._emptyWishlistMessage = null;
                        }

                        this.getFriendshipStatus(userId);
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    /** Get the info on whether the requested profile belongs to a friend or not (and whether it's aconfirmed friendship) */
    private getFriendshipStatus(userId: string): Promise<void> {
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

    /** Create a new pending friendship */
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

    /** Sets gift status to reserved */
    private reserve(gift: IGift) {
        const giftOwnerId = this._profile?.appUserId;
        if(giftOwnerId) {
            this.giftService
            .updateToReservedStatus(gift, giftOwnerId)
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

    /** Sets gift status to archived */
    private archive(gift: IGift) {
        const giftOwnerId = this._profile?.appUserId;
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
        const giftOwnerId = this._profile?.appUserId;
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

    /** Set error message or route to login/home page */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._showLoader = false;
                this._errorMessage = Utils.getErrorMessage(response);
        }
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
}
