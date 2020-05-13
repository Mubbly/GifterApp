import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IWishlist, IWishlistCreate, IWishlistEdit } from 'domain/IWishlist';
import { BaseService } from './base/baseService';
import { AppState } from 'state/appState';

@autoinject
export class WishlistService extends BaseService<IWishlist, IWishlistCreate, IWishlistEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.WISHLISTS, httpClient, appState);
    }
}
