import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IProfile } from 'domain/IProfile';
import { ProfileService } from 'service/profileService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class ProfileDetails {
    private _profile: Optional<IProfile> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private profileService: ProfileService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getProfile(params.id);
    }

    private getProfile(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.profileService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._profile = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
