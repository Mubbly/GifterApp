import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import * as UtilFunctions from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { ICampaign } from 'domain/ICampaign';
import { ICampaignCreate } from 'domain/ICampaignCreate'
import { ICampaignEdit } from 'domain/ICampaignEdit';
import { AppState } from 'state/appState';

@autoinject
export class CampaignService {
    private readonly _baseUrl = 'https://localhost:5001/api/Campaigns';

    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState._baseUrl;
    }

    async getCampaigns(): Promise<IFetchResponse<ICampaign[]>> {
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
                const data = (await response.json()) as ICampaign[];
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

    async getCampaign(id: string): Promise<IFetchResponse<ICampaign>> {
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
                const data = (await response.json()) as ICampaign;
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

    async createCampaign(campaign: ICampaignCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(campaign),
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

    async updateCampaign(campaign: ICampaignEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${campaign.id}`, JSON.stringify(campaign),
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

    async deleteCampaign(id: string): Promise<IFetchResponse<string>> {
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
