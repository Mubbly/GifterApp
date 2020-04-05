import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IProfile } from 'domain/IProfile';
import { ProfileService } from 'service/profileService';

@autoinject
export class ProfileDetails {
    private _profiles: IProfile[] = [];
    private _profile: Optional<IProfile> = null;

    constructor(private profileService: ProfileService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.profileService.getProfile(params.id).then(
                data => this._profile = data
            )
        }
    }
}
