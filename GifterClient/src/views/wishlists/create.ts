import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { WishlistService } from "service/wishlistService";
import * as Utils from "utils/utilFunctions";
import { IWishlistCreate } from "domain/IWishlistCreate";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { App } from "app";

@autoinject
export class WishlistsCreate {
    private _wishlist?: IWishlistCreate;
    private _comment: Optional<string> = null;
    private _appUserId: string = "";
    private _errorMessage: Optional<string> = null;
    private _jwt: boolean = false;

    constructor(
        private wishlistService: WishlistService,
        private router: Router,
        private appState: AppState,
    ) {}

    attached() {}

    activate() {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
    }

    onSubmit(event: Event) {
        let newWishlist: IWishlistCreate = {
            comment: this._comment,
        };
        this.createWishlist(newWishlist);
        event.preventDefault();
    }

    private createWishlist(newWishlist: IWishlistCreate) {
        this.wishlistService
            .createWishlist(newWishlist)
            .then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute("wishlistsIndex", {});
                }
            });
    }
}
