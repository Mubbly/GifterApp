// import { autoinject } from 'aurelia-framework';
// import { RouteConfig, NavigationInstruction } from 'aurelia-router';
// import { Optional } from 'types/generalTypes';
// import { IUserPermission } from 'domain/IUserPermission';
// import { UserPermissionService } from 'service/userPermissionService';

// @autoinject
// export class UserPermissionDetails {
//     private _userPermissions: IUserPermission[] = [];
//     private _userPermission: Optional<IUserPermission> = null;

//     constructor(private userPermissionService: UserPermissionService) {

//     }

//     attached() {

//     }

//     activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
//         if(params.id && typeof(params.id) === 'string') {
//             this.userPermissionService.getUserPermission(params.id).then(
//                 data => this._userPermission = data
//             )
//         }
//     }
// }
