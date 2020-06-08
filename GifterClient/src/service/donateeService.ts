import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IDonatee, IDonateeCreate, IDonateeEdit } from 'domain/IDonatee';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class DonateeService extends BaseService<IDonatee, IDonateeCreate, IDonateeEdit> {

    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.DONATEES, httpClient, appState);
    }

    async getAllForCampaign(campaignId: string): Promise<IFetchResponse<IDonatee[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Campaign/${campaignId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IDonatee[];
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
    
    /** Do not use */
    async create(entity: IDonateeCreate): Promise<IFetchResponse<IDonateeCreate>> {
        return Utils.actionNotAllowed();
    }

    async createDonatee(entity: IDonateeCreate, campaignId: string): Promise<IFetchResponse<IDonateeCreate>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient.post(`${this.apiEndpointUrl}/Campaign/${campaignId}`, 
                JSON.stringify(entity),
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
}
