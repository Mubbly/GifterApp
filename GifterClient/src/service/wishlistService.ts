import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IWishlist } from 'domain/IWishlist';

@autoinject
export class WishlistService {
    private readonly _baseUrl = 'https://localhost:5001/api/Wishlists';

    constructor(private httpClient: HttpClient) {

    }

    getWishlists(): Promise<IWishlist[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IWishlist[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getWishlist(id: string): Promise<Optional<IWishlist>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IWishlist) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
