import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { Optional, GifterInterface } from "types/generalTypes";
import { IWishlist } from "domain/IWishlist";
import { WishlistService } from "service/wishlistService";
import * as Utils from "utils/utilFunctions";
import { IFetchResponse } from "types/IFetchResponse";
import { AppState } from "state/appState";

@autoinject
export class WishlistDetails {
    private _wishlist: Optional<IWishlist> = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private wishlistService: WishlistService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getWishlist(params.id);
        }
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
        switch (response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
