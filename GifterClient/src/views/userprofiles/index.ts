// import { Router } from "aurelia-router";
// import { autoinject } from "aurelia-framework";
// import { IUserProfile } from "domain/IUserProfile";
// import { UserProfileService } from "service/userProfileService";
// import * as Utils from "utils/utilFunctions";
// import { Optional, GifterInterface } from "types/generalTypes";
// import { IFetchResponse } from '../../types/IFetchResponse';
// import { AppState } from "state/appState";

// @autoinject
// export class UserProfilesIndex {
//     private _userProfiles: IUserProfile[] = [];
//     private _errorMessage: Optional<string> = null;

//     constructor(
//         private userProfileService: UserProfileService,
//         private router: Router,
//         private appState: AppState
//     ) {}

//     attached() {
//         if(!this.appState.jwt) {
//             this.router.navigateToRoute(Utils.LOGIN_ROUTE);
//         } else {
//             this.getAllUserProfiles();
//         }
//     }
    
//     private getAllUserProfiles(): void {
//         this.userProfileService
//         .getAll()
//         .then((response) => {
//             if (!Utils.isSuccessful(response)) {
//                 this.handleErrors(response);
//             } else {
//                 this._userProfiles = response.data!;
//             }
//         })
//         .catch((error) => {
//             console.log(error);
//         });
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
