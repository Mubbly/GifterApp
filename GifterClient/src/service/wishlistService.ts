import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import * as UtilFunctions from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { IWishlist } from 'domain/IWishlist';
import { IWishlistCreate } from 'domain/IWishlistCreate'
import { IWishlistEdit } from 'domain/IWishlistEdit';
import { AppState } from 'state/appState';

@autoinject
export class WishlistService {
    private readonly _baseUrl = 'https://localhost:5001/api/Wishlists';
    private readonly _personalBaseUrl = 'https://localhost:5001/api/Wishlists/Personal';

    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState._baseUrl;
    }

    async getWishlists(): Promise<IFetchResponse<IWishlist[]>> {
        try {
            const response = await this.httpClient.fetch(this._baseUrl,
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
                );

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IWishlist[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch(reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getPersonalWishlists(): Promise<IFetchResponse<IWishlist[]>> {
        try {
            const response = await this.httpClient.fetch(this._personalBaseUrl,
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
                );

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IWishlist[];
                return {
                    status: response.status,
                    data: data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch(reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getWishlist(id: string): Promise<IFetchResponse<IWishlist>> {
        try {
            const response = await this.httpClient.fetch(`${this._baseUrl}/${id}`,
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
            );

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IWishlist;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch(reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createWishlist(wishlist: IWishlistCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(wishlist),
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
            );

            if(UtilFunctions.isSuccessful(response)) {
                console.log('response', response);
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async updateWishlist(wishlist: IWishlistEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${wishlist.id}`, JSON.stringify(wishlist),
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
            );
        
            if(UtilFunctions.isSuccessful(response)) {
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async deleteWishlist(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.delete(`${this._baseUrl}/${id}`, null, 
                { 
                    cache: "no-store",
                    headers: {
                        authorization: `Bearer ${this.appState.jwt}`
                    }
                }
            );

            if(UtilFunctions.isSuccessful(response)) {
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
}
