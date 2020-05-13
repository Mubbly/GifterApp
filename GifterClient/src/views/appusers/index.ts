import { autoinject } from 'aurelia-framework';
import { IAppUser } from 'domain/IAppUser';
import { AppUserService } from 'service/base/appUserService';

@autoinject
export class AppUsersIndex {
    private _appUsers: IAppUser[] = [];

    constructor(private appUserService: AppUserService) {

    }

    attached() {
        this.appUserService.getAppUsers().then(
            data => this._appUsers = data
        );
    }
}
