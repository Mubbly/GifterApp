import { autoinject } from 'aurelia-framework';
import { IUserProfile } from 'domain/IUserProfile';
import { UserProfileService } from 'service/userProfileService';

@autoinject
export class UserProfilesIndex {
    private _userProfiles: IUserProfile[] = [];

    constructor(private userProfileService: UserProfileService) {

    }

    attached() {
        this.userProfileService.getUserProfiles().then(
            data => this._userProfiles = data
        );
    }
}
