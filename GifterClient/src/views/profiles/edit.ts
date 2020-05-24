import { autoinject } from 'aurelia-framework';
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IProfileEdit, IProfile } from "domain/IProfile";
import { IAppUser } from "domain/IAppUser";
import { Optional, GifterInterface, ProfileRouteProps } from "types/generalTypes";
import { ProfileService } from "service/profileService";
import { AppUserService } from "service/base/appUserService";
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppState } from 'state/appState';
import * as UtilFunctions from 'utils/utilFunctions';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';

@autoinject
export class ProfilesEdit {
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _profile: Optional<IProfileEdit> = null;
    private _appUser: Optional<IAppUser> = null;
    private _errorMessage: Optional<string> = null;

    private _showEmail: boolean = false;
    private _profileBannerUrl: Optional<string> = null;
    private _routeProps: ProfileRouteProps = {};

    private _genders = [
        { name: 'Male' },
        { name: 'Female' },
        { name: 'Non-binary'}
    ]
    private _selectedGender: Optional<string> = null;

    constructor(private profileService: ProfileService,
        private appUserService: AppUserService,
        private router: Router,
        private appState: AppState) {
    }

    attached() {
    }

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCurrentAppUser();
            this.getPersonalProfile();
        }
    }

    private getCurrentAppUser(): void {
        this.appUserService
        .getCurrentUser()
        .then((response) => {
            if(Utils.isSuccessful(response) && response.data) {
                this._appUser = response.data;
            } else {
                this.handleErrors(response);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Get user's personal profile. Default initial one if not edited yet.
     */
    private getPersonalProfile(): void {
        this.profileService
            .getPersonal()
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {                    
                    this._profile = response.data!;
                    this._selectedGender = this._profile.gender;
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    onSubmit(event: Event) {
        event.preventDefault();

        // Required fields
        let userNameInput = <string>this._appUser!.userName;
        let firstNameInput = <string>this._appUser!.firstName;
        let lastNameInput = <string>this._appUser!.lastName;
        let emailInput = <string>this._appUser!.email;
        
        if(Utils.isNullOrEmpty(userNameInput) || Utils.isNullOrEmpty(firstNameInput) 
                || Utils.isNullOrEmpty(lastNameInput) || Utils.isNullOrEmpty(emailInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        this._appUser!.userName = userNameInput;
        this._appUser!.firstName = firstNameInput;
        this._appUser!.lastName = lastNameInput;
        this._appUser!.email = emailInput;

        // Optional fields
        let genderInput = <Optional<string>>this._selectedGender;
        this._profile!.gender = Utils.isNullOrEmpty(genderInput) ? null : genderInput;

        let ageInput = <Optional<number>>this._profile!.age;
        this._profile!.age = (ageInput == 0 || ageInput == null || ageInput == undefined) ? null : Number(ageInput);

        let bioInput = <Optional<string>>this._profile!.bio;
        this._profile!.bio = Utils.isNullOrEmpty(bioInput) ? null : bioInput;

        let profilePictureUrlInput = <Optional<string>>this._profile!.profilePicture;
        this._profile!.profilePicture = Utils.isNullOrEmpty(profilePictureUrlInput) ? null : profilePictureUrlInput;

        let makePrivateInput = <boolean>this._profile!.isPrivate;
        this._profile!.isPrivate = makePrivateInput;

        // Non-db fields
        this._profileBannerUrl = <Optional<string>>this._profileBannerUrl;
        this._routeProps.profileBannerUrl = this._profileBannerUrl;

        let showEmailInput = <boolean>this._showEmail;
        this._showEmail = showEmailInput;
        this._routeProps.showEmail = this._showEmail;

        // Update data
        if(this._appUser && this._profile) {
            this.updateProfile();
        }
    }

    private updateProfile() {
        console.log(this._profile);
        this.profileService
            .update(this._profile!)
            .then((response) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.updateAppUser();
                    console.log(response);
                } else {
                    this._errorMessage = Utils.getErrorMessage(response);
                }
            }).catch((error) => {
                console.log(error);
            });
    }

    private updateAppUser(): void {
        this.appUserService
            .update(this._appUser!)
            .then((response) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute(ApiEndpointUrls.PROFILES_PERSONAL, this._routeProps);
                } else {
                    this._errorMessage = Utils.getErrorMessage(response);
                }
            }).catch((error) => {
                console.log(error);
            });
    }    

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
