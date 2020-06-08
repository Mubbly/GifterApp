import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IFriendship, IFriendshipCreate, IFriendshipEdit } from 'domain/IFriendship';
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

    async createPending(friendId: string): Promise<IFetchResponse<IFriendshipCreate>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        let newPendingFriendship: IFriendshipCreate = {
            appUser2Id:  friendId,
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

    async getAllPendingFriendships(): Promise<IFetchResponse<IFriendship[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/pending`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IFriendship[];
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
