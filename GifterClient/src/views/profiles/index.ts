import { autoinject } from 'aurelia-framework';
import { IProfile } from 'domain/IProfile';
import { ProfileService } from 'service/profileService';

@autoinject
export class ProfilesIndex {
    private _profiles: IProfile[] = [];

    constructor(private profileService: ProfileService) {

    }

    attached() {
        this.profileService.getProfiles().then(
            data => this._profiles = data
        );
    }
}
