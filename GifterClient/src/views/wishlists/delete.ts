import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { WishlistService } from 'service/wishlistService';
import * as Utils from 'utils/utilFunctions';
import { IWishlist } from 'domain/IWishlist';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class WishlistsDelete {
    private readonly WISHLISTS_ROUTE = 'wishlistsIndex';

    private _wishlist?: IWishlist;
    private _errorMessage: Optional<string> = null;

    constructor(private wishlistService: WishlistService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getWishlist(params.id);
        }
    }

    onSubmit(event: Event) {
        this.deleteWishlist(this._wishlist!.id);
        event.preventDefault();
    }

    private getWishlist(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.wishlistService.getWishlist(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this._wishlist = response.data!;
                }
            });
        }
    }

    private deleteWishlist(id: string) {
        this.wishlistService
        .deleteWishlist(id)
        .then(
            response => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(this.WISHLISTS_ROUTE, {});
                }
            }
        );
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
