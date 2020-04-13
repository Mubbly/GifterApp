import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { IWishlist } from "domain/IWishlist";
import { WishlistService } from "service/wishlistService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from '../../types/IFetchResponse';
import { AppState } from "state/appState";

@autoinject
export class WishlistsPersonal {
    private _wishlists: IWishlist[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(
        private wishlistService: WishlistService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getAllPersonalWishlists();
        }
    }
    
    /**
     * Get wishlists that the user has created
     */
    private getAllPersonalWishlists(): void {
        this.wishlistService
        .getPersonalWishlists()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._wishlists = response.data!;
            }
        })
        .catch((error) => {
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
