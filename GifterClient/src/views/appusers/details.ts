import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IAppUser } from 'domain/IAppUser';
import { AppUserService } from 'service/appUserService';

@autoinject
export class AppUserDetails {
    private _appUsers: IAppUser[] = [];
    private _appUser: Optional<IAppUser> = null;

    constructor(private appUserService: AppUserService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.appUserService.getAppUser(params.id).then(
                data => this._appUser = data
            )
        }
    }
}
