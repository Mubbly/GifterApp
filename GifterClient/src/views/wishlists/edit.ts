import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IWishlistEdit } from 'domain/IWishlist';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { WishlistService } from 'service/wishlistService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class WishlistsEdit {
    private readonly WISHLIST_ROUTE = 'wishlistsIndex';

    private _wishlist?: IWishlistEdit;
    private _errorMessage: Optional<string> = null;

    constructor(private wishlistService: WishlistService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getWishlist(params.id);
        }
    }

    onSubmit(event: Event) {
        this.getNewValuesFromInputs();
        this.updateWishlist();
        
        event.preventDefault();
    }

    /** Reassigns _wishlist props */
    private getNewValuesFromInputs() {
        let commentInput = <Optional<string>>this._wishlist!.comment;
        if(Utils.isEmpty(commentInput)) {
            return;
        }
        this._wishlist!.comment = commentInput;
    }

    private updateWishlist(): void {
        this.wishlistService
            .update(this._wishlist!)
            .then(
                (response: IFetchResponse<IWishlistEdit>) => {
                    if (Utils.isSuccessful(response)) {
                        this.router.navigateToRoute(this.WISHLIST_ROUTE, {});
                    } else {
                        this._errorMessage = Utils.getErrorMessage(response);
                    }
                }
            );
    }

    private getWishlist(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.wishlistService.get(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this._wishlist = response.data!;
                }
            });
        }
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
