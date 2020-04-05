import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IWishlist } from 'domain/IWishlist';
import { WishlistService } from 'service/wishlistService';

@autoinject
export class WishlistDetails {
    private _wishlists: IWishlist[] = [];
    private _wishlist: Optional<IWishlist> = null;

    constructor(private wishlistService: WishlistService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.wishlistService.getWishlist(params.id).then(
                data => this._wishlist = data
            )
        }
    }
}
