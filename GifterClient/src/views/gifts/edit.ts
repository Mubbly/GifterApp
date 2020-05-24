import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { GiftService } from 'service/giftService';
import { IGiftEdit } from 'domain/IGift';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class GiftsEdit {
    private _gift?: IGiftEdit;
    private _errorMessage: Optional<string> = null;

    constructor(private giftService: GiftService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getGift(params.id);
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
            .update(this._gift!)
            .then(
                (response: IFetchResponse<IGiftEdit>) => {
                    if (UtilFunctions.isSuccessful(response)) {
                        this.router.navigateToRoute('profilesPersonal', {});
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);
                    }
                }
            );

        event.preventDefault();
    }

    private getGift(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.giftService.get(id).then(
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
}
