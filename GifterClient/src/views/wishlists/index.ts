import { autoinject } from 'aurelia-framework';
import { IWishlist } from 'domain/IWishlist';
import { WishlistService } from 'service/wishlistService';

@autoinject
export class WishlistsIndex {
    private _wishlists: IWishlist[] = [];

    constructor(private wishlistService: WishlistService) {

    }

    attached() {
        this.wishlistService.getWishlists().then(
            data => this._wishlists = data
        );
    }
}
