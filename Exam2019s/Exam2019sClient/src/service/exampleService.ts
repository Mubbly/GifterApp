import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IExample } from 'domain/IExample';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IExampleEdit, IExampleCreate } from '../domain/IExample';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';

@autoinject
export class ExampleService extends BaseService<IExample, IExampleCreate, IExampleEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.EXAMPLES, httpClient, appState);
    }

    async getAllForUser(userId: string): Promise<IFetchResponse<IExample[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.USER}/${userId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IExample[];
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
