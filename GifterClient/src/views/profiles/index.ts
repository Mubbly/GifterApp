import { autoinject } from "aurelia-framework";
import { IProfile } from "domain/IProfile";
import { ProfileService } from "service/profileService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class ProfilesIndex {
    private _reservedProfiles: IProfile[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private reservedProfileService: ProfileService) {}

    attached() {
        this.getProfiles();
    }

    private getProfiles(): void {
        this.reservedProfileService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedProfiles = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
