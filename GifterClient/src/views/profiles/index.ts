// import { Router } from "aurelia-router";
// import { autoinject } from "aurelia-framework";
// import { IProfile } from "domain/IProfile";
// import { ProfileService } from "service/profileService";
// import * as Utils from "utils/utilFunctions";
// import { Optional, GifterInterface } from "types/generalTypes";
// import { AppState } from "state/appState";
// import { AppUserService } from "service/base/appUserService";
// import { IGift } from "domain/IGift";
// import { GiftService } from "service/giftService";
// import { IAppUser } from "domain/IAppUser";
// import { WishlistService } from "service/wishlistService";
// import { IWishlist } from '../../domain/IWishlist';
// import { IFetchResponse } from "types/IFetchResponse";

// @autoinject
// export class ProfilesIndex {
//     //private _profiles: IProfile[] = [];
//     private _profile: Optional<IProfile> = null;
//     private _currentUser: Optional<IAppUser> = null;
//     private _targetUser: Optional<IAppUser> = null;
//     private _wishlist: Optional<IWishlist> = null;
//     private _gifts: IGift[] = [];
//     private _lastActiveDate: string = '';
//     private _errorMessage: Optional<string> = null;

//     constructor(
//         private profileService: ProfileService,
//         private appUserService: AppUserService,
//         private wishlistService: WishlistService,
//         private giftService: GiftService,
//         private router: Router,
//         private appState: AppState
//     ) {
//     }

//     attached() {}

//     activate(params: any) {
//         if(!this.appState.jwt) {
//             this.router.navigateToRoute(Utils.LOGIN_ROUTE);
//         } else {
//             this.getCurrentAppUser();
//             this.getTargetAppUser(params.userId);
//             this.getProfile(params.userId);
//             this.getWishlist();
//             this.getGifts();
//         }
//     }

//     private getCurrentAppUser(): void {
//         this.appUserService
//             .getCurrentUser()
//             .then((response) => {
//                 if(Utils.isSuccessful(response) && response.data) {
//                     this._currentUser = response.data;
//                 } else {
//                     this.handleErrors(response);
//                 }
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     }

//     private getTargetAppUser(): Promise<void> {
//         return this.appUserService
//             .getCurrentUser()
//             .then((response) => {
//                 if(Utils.isSuccessful(response) && response.data) {
//                     this._currentUser = response.data;
//                 } else {
//                     this.handleErrors(response);
//                 }
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     }

//     /**
//      * Get user's profile. Default initial one if not edited yet.
//      */
//     private getProfile(profileId: string): Promise<void> {
//         return this.profileService
//             .get(profileId)
//             .then((response) => {
//                 if (!Utils.isSuccessful(response)) {
//                     this.handleErrors(response);
//                 } else {                    
//                     this._profile = response.data!;
//                     this._lastActiveDate = Utils.formatAsHtml5Date(this._profile.appUser.lastActive);
//                     console.log(this._profile.id);
//                 }
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     }

//     private getWishlist(wishlistId: string): Promise<void> {
//         return this.wishlistService
//             .get(wishlistId)
//             .then((response) => {
//                 if (!Utils.isSuccessful(response)) {
//                     this.handleErrors(response);
//                 } else {                    
//                     this._wishlist = response.data!;
//                 }
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     }

//     private getGifts(): Promise<void> {
//         return this.giftService
//             .getAll()
//             .then((response) => {
//                 if (!Utils.isSuccessful(response)) {
//                     this.handleErrors(response);
//                 } else {                    
//                     this._gifts = response.data!;
//                 }
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     }

//     /**
//      * Set error message or route to login/home page
//      */
//     private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
//         switch(response.status) {
//             case Utils.STATUS_CODE_UNAUTHORIZED:
//                 this.router.navigateToRoute(Utils.LOGIN_ROUTE);
//                 break;
//             default:
//                 this._errorMessage = Utils.getErrorMessage(response);
//         }
//     }
// }
