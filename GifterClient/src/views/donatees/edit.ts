import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { DonateeService } from 'service/donateeService';
import { IDonateeEdit } from 'domain/IDonateeEdit';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';

@autoinject
export class DonateesEdit {
    private _donatee?: IDonateeEdit;
    private _errorMessage: Optional<string> = null;

    constructor(private donateeService: DonateeService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const donateeId = params.id;
        if(UtilFunctions.existsAndIsString(donateeId)) {
            this.donateeService.getDonatee(donateeId).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._donatee = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }

    onSubmit(event: Event) {
        let firstNameInput = <string>this._donatee!.firstName;
        let giftNameInput = <string>this._donatee!.giftName;
        let activeFromInput = <string>this._donatee!.activeFrom;
        let activeToInput = <string>this._donatee!.activeTo;

        if(UtilFunctions.isEmpty(firstNameInput) 
            || UtilFunctions.isEmpty(giftNameInput)
            || UtilFunctions.isEmpty(activeFromInput)
            || UtilFunctions.isEmpty(activeToInput)) {
            alert("Name missing!")
            return;
        }
        let lastNameInput = <Optional<string>>this._donatee!.lastName;
        let genderInput = <Optional<string>>this._donatee!.gender;
        let ageInput = <Optional<string>>this._donatee!.age;
        let bioInput = <Optional<string>>this._donatee!.bio;
        let giftDescriptionInput = <Optional<string>>this._donatee!.giftDescription;
        let giftImageInput = <Optional<string>>this._donatee!.giftImage;
        let giftUrlInput = <Optional<string>>this._donatee!.giftUrl;

        this._donatee!.lastName = UtilFunctions.isEmpty(lastNameInput) ? null : lastNameInput;
        this._donatee!.gender = UtilFunctions.isEmpty(genderInput) ? null : genderInput;
        this._donatee!.age = UtilFunctions.isEmpty(ageInput) ? null : Number(ageInput);
        this._donatee!.bio = UtilFunctions.isEmpty(bioInput) ? null : bioInput;
        this._donatee!.giftDescription = UtilFunctions.isEmpty(giftDescriptionInput) ? null : giftDescriptionInput;
        this._donatee!.giftImage = UtilFunctions.isEmpty(giftImageInput) ? null : giftImageInput;
        this._donatee!.giftUrl = UtilFunctions.isEmpty(giftUrlInput) ? null : giftUrlInput;

        console.log(this._donatee);

        this.donateeService
            .updateDonatee(this._donatee!)
            .then(
                response => {
                    if (UtilFunctions.isSuccessful(response)) {
                        this.router.navigateToRoute('donateesIndex', {});
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            );

        event.preventDefault();
    }
}
