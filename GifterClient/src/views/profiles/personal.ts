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
import { EventEmitter } from "events";

@autoinject
export class ProfilesPersonal {
    //private _profiles: IProfile[] = [];
    private readonly EMPTY_WISHLIST_MESSAGE = 'Your wishlist seems to be empty :( Add gifts to get started!';
    private _profile: Optional<IProfile> = null;
    private _appUser: Optional<IAppUser> = null;
    private _wishlist: Optional<IWishlist> = null;
    private _gifts: Optional<IGift[]> = null;
    private _lastActiveDate: string = '';
    private _emptyWishlistMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    private _showEmail: boolean = false;
    private _profileBannerUrl: Optional<string> = null;

    private _draggedElement = null;

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
            if(this.appState.showEmail) {
                this._showEmail = !!this.appState.showEmail;
            }
            if(this.appState.profileBannerUrl) {
                this._profileBannerUrl = this.appState.profileBannerUrl;
            }
            this.getPersonalFullProfile();
            // this.getCurrentAppUser();
            // this.getPersonalProfile();
            // this.getPersonalWishlist();
            // this.getPersonalGifts();
        }
    }

    /**
     * Get user's personal profile. Default initial one if not edited yet.
     */
    private getPersonalFullProfile(): Promise<void> {
        return this.profileService
            .getFullPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._profile = response.data!;
                    if(this._profile) {
                        this._wishlist = this._profile.wishlist;
                        this._appUser = this._profile.appUser;
                        this._gifts = this._profile.wishlist.gifts;
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    // private getCurrentAppUser(): void {
    //     this.appUserService
    //     .getCurrentUser()
    //     .then((response) => {
    //         if(Utils.isSuccessful(response) && response.data) {
    //             this._appUser = response.data;
    //             this._lastActiveDate = Utils.formatAsHtml5Date(this._appUser.lastActive);
    //         } else {
    //             this.handleErrors(response);
    //         }
    //     })
    //     .catch((error) => {
    //         console.log(error);
    //     });
    // }

    // /**
    //  * Get user's personal profile. Default initial one if not edited yet.
    //  */
    // private getPersonalProfile(): Promise<void> {
    //     return this.profileService
    //         .getPersonal()
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {                    
    //                 this._profile = response.data!;
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }

    // private getPersonalWishlist(): Promise<void> {
    //     return this.wishlistService
    //         .getPersonal()
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

    // private getPersonalGifts(): Promise<void> {
    //     return this.giftService
    //         .getAllPersonal()
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {                    
    //                 this._gifts = response.data!;
    //                 if(this._gifts.length <= 0) {
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


    // TODO: Drag and drop Gift panels to change order. Save to local storage?
    // https://stackoverflow.com/questions/10588607/tutorial-for-html5-dragdrop-sortable-list ; https://codepen.io/crouchingtigerhiddenadam/pen/qKXgap
    // https://stackoverflow.com/questions/43667677/using-html5-drag-and-drop-with-aurelia ; https://gist.run/?id=375dbed8d63cff44075e5f93403dd9dc
    // https://stackoverflow.com/questions/7110353/html5-dragleave-fired-when-hovering-a-child-element
    // https://stackoverflow.com/questions/54626442/drag-and-drop-events-for-image-file-upload-not-working-in-aurelia
    // https://davismj.me/blog/aurelia-drag-and-drop/
    
    // aurelia: draggable="true" dragstart.trigger="onDragStart($event)" dragover.trigger="onDragOver($event)" drop.trigger="onDragDrop($event)"

    // onDragStart(event: any) {
    //     // event.preventDefault();
    //     event.dataTransfer.effectAllowed = "move";
    //     event.dataTransfer.setData("text/plain", null);
    //     this._draggedElement = event.target.parentNode;
    //     $('.gift-panel').css('cursor', 'pointer');
    //     return true;
    // }

    // onDragOver(event: any) {
    //     event.preventDefault();
    //     if (this.isBefore(this._draggedElement, event.target)) {
    //         event.target.parentNode.insertBefore(this._draggedElement, event.target);
    //     } else {
    //         event.target.parentNode.insertBefore(this._draggedElement, event.target.nextSibling);
    //     }
    // }

    // onDragDrop(event: any) {
    //     event.preventDefault();
    // }

    // private isBefore(el1: any, el2: any) {
    //     const DOCUMENT_NODE = 9;
    //     if (el2.parentNode === el1.parentNode) {
    //         for (var current = el1.previousSibling; current && current.nodeType !== DOCUMENT_NODE; current = current.previousSibling) {
    //             if (current === el2) {
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    //   }
}

