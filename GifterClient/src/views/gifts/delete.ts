import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { GiftService } from 'service/giftService';
import * as UtilFunctions from 'utils/utilFunctions';
import { IGift } from 'domain/IGift';
import { Optional } from 'types/generalTypes';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { PermissionDetails } from '../permissions/details';

@autoinject
export class GiftsDelete {
    private _gift?: IGift;
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
        this.giftService
        .delete(this._gift!.id)
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
