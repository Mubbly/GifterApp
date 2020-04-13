import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import * as UtilFunctions from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { IInvitedUser } from 'domain/IInvitedUser';
import { IInvitedUserCreate } from 'domain/IInvitedUserCreate'
import { IInvitedUserEdit } from 'domain/IInvitedUserEdit';
import { AppState } from 'state/appState';

@autoinject
export class InvitedUserService {
    private readonly _baseUrl = 'https://localhost:5001/api/InvitedUsers';

    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    async getInvitedUsers(): Promise<IFetchResponse<IInvitedUser[]>> {
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
                const data = (await response.json()) as IInvitedUser[];
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

    async getInvitedUser(id: string): Promise<IFetchResponse<IInvitedUser>> {
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
                const data = (await response.json()) as IInvitedUser;
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

    async createInvitedUser(invitedUser: IInvitedUserCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(invitedUser),
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

    async updateInvitedUser(invitedUser: IInvitedUserEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${invitedUser.id}`, JSON.stringify(invitedUser),
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

    async deleteInvitedUser(id: string): Promise<IFetchResponse<string>> {
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
