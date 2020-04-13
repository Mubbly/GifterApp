import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IActionType } from 'domain/IActionType';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import * as UtilFunctions from 'utils/utilFunctions';
import { IActionTypeCreate } from 'domain/IActionTypeCreate';
import { IActionTypeEdit } from 'domain/IActionTypeEdit';

@autoinject
export class ActionTypeService {
    private readonly _baseUrl = 'https://localhost:5001/api/admin/ActionTypes';

    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    async getActionTypes(): Promise<IFetchResponse<IActionType[]>> {
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
                const data = (await response.json()) as IActionType[];
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

    async getActionType(id: string): Promise<IFetchResponse<IActionType>> {
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
                const data = (await response.json()) as IActionType;
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

    async createActionType(actionType: IActionTypeCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(actionType),
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

    async updateActionType(actionType: IActionTypeEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${actionType.id}`, JSON.stringify(actionType),
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

    async deleteActionType(id: string): Promise<IFetchResponse<string>> {
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
