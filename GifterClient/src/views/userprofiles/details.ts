// import { autoinject } from 'aurelia-framework';
// import { RouteConfig, NavigationInstruction } from 'aurelia-router';
// import { Optional } from 'types/generalTypes';
// import { IUserProfile } from 'domain/IUserProfile';
// import { UserProfileService } from 'service/userProfileService';

// @autoinject
// export class UserProfileDetails {
//     private _userProfiles: IUserProfile[] = [];
//     private _userProfile: Optional<IUserProfile> = null;

//     constructor(private userProfileService: UserProfileService) {

//     }

//     attached() {

//     }

//     activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
//         if(params.id && typeof(params.id) === 'string') {
//             this.userProfileService.getUserProfile(params.id).then(
//                 data => this._userProfile = data
//             )
//         }
//     }
// }
