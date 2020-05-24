import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IFriendship, IFriendshipCreate, IFriendshipEdit } from 'domain/IFriendship';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class FriendshipService extends BaseService<IFriendship, IFriendshipCreate, IFriendshipEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.FRIENDSHIPS, httpClient, appState);
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
