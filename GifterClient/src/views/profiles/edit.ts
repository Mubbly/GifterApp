import { autoinject } from 'aurelia-framework';
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IProfileEdit, IProfile } from "domain/IProfile";
import { IAppUser } from "domain/IAppUser";
import { Optional } from "types/generalTypes";
import { ProfileService } from "service/profileService";
import { AppUserService } from "service/base/appUserService";
import * as Utils from 'utils/utilFunctions';

@autoinject
export class ProfilesEdit {
    private _profile?: IProfileEdit;
    private _appUsers: IAppUser[] = [];
    private _appUser: Optional<IAppUser> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private profileService: ProfileService,
        private appUserService: AppUserService,
        private router: Router) {
    }

    attached() {
    }

    activate(params: any) {
        console.log(this.profileService);
        this.profileService.getPersonal(params.id).then(
            response => {
                if(Utils.isSuccessful(response)) {
                    this._profile = response.data!
                } else {
                    this._errorMessage = Utils.getErrorMessage(response);
                }
            }
        );
        // const profileId = params.id;
        // this._campaignId = params.campaignId;
        // this._campaignName = params.campaignName;

        // if(UtilFunctions.existsAndIsString(profileId)) {
        //     this.getRelatedData();

        //     this.profileService.get(profileId).then(
        //         response => {
        //             if(UtilFunctions.isSuccessful(response)) {
        //                 this._profile = response.data!;
        //             } else {
        //                 this._errorMessage = UtilFunctions.getErrorMessage(response);

        //             }
        //         }
        //     )
        // }
    }

    // From other tables that are connected to this one via foreign keys
    private getRelatedData() {

    }

    onSubmit(event: Event) {
        event.preventDefault();

        // let firstNameInput = <string>this._profile!.firstName;
        // let giftNameInput = <string>this._profile!.giftName;
        // let activeFromInput = <string>this._profile!.activeFrom;
        // let activeToInput = <string>this._profile!.activeTo;

        // if(UtilFunctions.isEmpty(firstNameInput) 
        //     || UtilFunctions.isEmpty(giftNameInput)
        //     || UtilFunctions.isEmpty(activeFromInput)
        //     || UtilFunctions.isEmpty(activeToInput)) {
        //     alert("Name missing!")
        //     return;
        // }
        // let lastNameInput = <Optional<string>>this._profile!.lastName;
        // let genderInput = <Optional<string>>this._profile!.gender;
        // let ageInput = <Optional<string>>this._profile!.age;
        // let bioInput = <Optional<string>>this._profile!.bio;
        // let giftDescriptionInput = <Optional<string>>this._profile!.giftDescription;
        // let giftImageInput = <Optional<string>>this._profile!.giftImage;
        // let giftUrlInput = <Optional<string>>this._profile!.giftUrl;

        // this._profile!.lastName = UtilFunctions.isEmpty(lastNameInput) ? null : lastNameInput;
        // this._profile!.gender = UtilFunctions.isEmpty(genderInput) ? null : genderInput;
        // this._profile!.age = UtilFunctions.isEmpty(ageInput) ? null : Number(ageInput);
        // this._profile!.bio = UtilFunctions.isEmpty(bioInput) ? null : bioInput;
        // this._profile!.giftDescription = UtilFunctions.isEmpty(giftDescriptionInput) ? null : giftDescriptionInput;
        // this._profile!.giftImage = UtilFunctions.isEmpty(giftImageInput) ? null : giftImageInput;
        // this._profile!.giftUrl = UtilFunctions.isEmpty(giftUrlInput) ? null : giftUrlInput;

        // console.log(this._profile);

        // this.profileService
        //     .update(this._profile!)
        //     .then(
        //         (response: IFetchResponse<IProfileEdit>) => {
        //             if (UtilFunctions.isSuccessful(response)) {
        //                 let isCampaignSpecified = this._campaignId !== '' && this._campaignName !== '';
        //                 let route = isCampaignSpecified ? 'profilesIndex' : 'campaignsIndex';
        //                 let params = isCampaignSpecified ? { campaignId: this._campaignId, campaignName: this._campaignName } : {};
        //                 this.router.navigateToRoute(route, params);
        //             } else {
        //                 this._errorMessage = UtilFunctions.getErrorMessage(response);

        //             }
        //         }
        //     );
    }
}
