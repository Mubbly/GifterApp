import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IGift, IGiftCreate, IGiftEdit } from 'domain/IGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class GiftService extends BaseService<IGift, IGiftCreate, IGiftEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.GIFTS, httpClient, appState);
    }

    async getAllForUser(userId: string): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/User/${userId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
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
