import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { GiftService } from 'service/giftService';
import { IGiftEdit } from 'domain/IGiftEdit';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';

@autoinject
export class GiftsEdit {
    private _gift?: IGiftEdit;
    private _errorMessage: Optional<string> = null;

    constructor(private giftService: GiftService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const giftId = params.id;
        if(UtilFunctions.existsAndIsString(giftId)) {
            this.giftService.getGift(giftId).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._gift = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }

    onSubmit(event: Event) {
        let nameInput = <string>this._gift!.name;
        if(nameInput == null || nameInput.length === 0) {
            alert("Name missing!")
            return;
        }
        let descInput = <Optional<string>>this._gift!.description;
        let imageInput = <Optional<string>>this._gift!.image;
        let urlInput = <Optional<string>>this._gift!.url;
        let partnerUrlInput = <Optional<string>>this._gift!.partnerUrl;

        this._gift!.description = (descInput === null || descInput.length === 0) ? null : descInput;
        this._gift!.image = (imageInput === null || imageInput.length === 0) ? null : imageInput;
        this._gift!.url = (urlInput === null || urlInput.length === 0) ? null : urlInput;
        this._gift!.partnerUrl = (partnerUrlInput === null || partnerUrlInput.length === 0) ? null : partnerUrlInput;
        
        console.log(this._gift);

        this.giftService
            .updateGift(this._gift!)
            .then(
                response => {
                    if (UtilFunctions.isSuccessful(response)) {
                        this.router.navigateToRoute('giftsIndex', {});
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            );

        event.preventDefault();
    }
}
