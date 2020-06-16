import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IFriendship, IFriendshipCreate, IFriendshipEdit, IFriendshipResponse } from 'domain/IFriendship';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class FriendshipService extends BaseService<IFriendship, IFriendshipCreate, IFriendshipEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.FRIENDSHIPS, httpClient, appState);
    }

    /** Do not use */
    async create(entity: IFriendshipCreate): Promise<IFetchResponse<IFriendshipCreate>> {
        return Utils.actionNotAllowed();
    }

    /** Do not use */
    async update(entity: IFriendshipEdit): Promise<IFetchResponse<IFriendshipEdit>> {
        return Utils.actionNotAllowed();
    }

    /** Edit friendship */
    async updateToConfirmedStatus(friendship: IFriendshipEdit): Promise<IFetchResponse<IFriendshipEdit>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}

        if (friendship.isConfirmed === false) {
            let editedFriendship: IFriendshipEdit = {
                id: friendship.id,
                appUser2Id:  friendship.appUser2Id,
                isConfirmed: true
            }
            try {
                const response = await this.httpClient.put(`${this.apiEndpointUrl}/personal/pending/${editedFriendship.id}`, 
                JSON.stringify(editedFriendship),
                    { 
                        cache: "no-store",
                        headers: AUTH_HEADERS
                    }
                );
            
                if(response.ok) {
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
        return {
            status: 404,
            errorMessage: "This friendship is already confirmed"
        }
    }

    async createPending(friendId: string): Promise<IFetchResponse<IFriendshipCreate>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        let newPendingFriendship: IFriendshipCreate = {
            appUser2Id: friendId,
            comment: null
        }
        try {
            const response = await this.httpClient.post(`${this.apiEndpointUrl}/personal/pending`, 
                JSON.stringify(newPendingFriendship),
                { 
                    cache: "no-store",
                    headers: AUTH_HEADERS
                }
            );

            if(response.ok) {
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

    async getAllSentPendingFriendships(): Promise<IFetchResponse<IFriendshipResponse[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/personal/pending/sent`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendshipResponse[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
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

    async getAllReceivedPendingFriendships(): Promise<IFetchResponse<IFriendshipResponse[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/pending/received`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendshipResponse[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
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

    async getPending(friendId: string): Promise<IFetchResponse<IFriendshipResponse>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/pending/${friendId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendshipResponse;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
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

    
    async getAllConfirmed(): Promise<IFetchResponse<IFriendshipResponse[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/confirmed`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendshipResponse[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
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

    async getConfirmed(friendId: string): Promise<IFetchResponse<IFriendshipResponse>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/confirmed/${friendId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendshipResponse;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
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
