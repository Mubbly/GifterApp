import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IAppUser } from 'domain/IAppUser';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import * as Environment from '../../../config/environment.json';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class AppUserService {
    private readonly appUserEndpointUrl = ApiEndpointUrls.APP_USERS;

    constructor(protected httpClient: HttpClient, protected appState: AppState) {
        this.httpClient.configure(config => {
            config
                .withBaseUrl(ApiEndpointUrls.API_BASE_URL)
                .withDefaults({
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json',
                        'X-Requested-With': 'Fetch'
                    }
                })
                .withInterceptor({
                    request(request) {
                        console.log(`Requesting ${request.method} ${request.url}`);
                        return request;
                    },
                    response(response) {
                        console.log(`Received ${response.status} ${response.url}`);
                        return response;
                    }
                });
        });
    }

    
    async getAllUsers(): Promise<IFetchResponse<IAppUser[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(this.appUserEndpointUrl, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IAppUser[];
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

    async getUsersByName(name: string): Promise<IFetchResponse<IAppUser[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.appUserEndpointUrl}/name/${name}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IAppUser[];
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

    async getUser(id: string): Promise<IFetchResponse<IAppUser>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.appUserEndpointUrl}/${id}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IAppUser;
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

    async getCurrentUser(): Promise<IFetchResponse<IAppUser>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.appUserEndpointUrl}/${ApiEndpointUrls.PERSONAL}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IAppUser;
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

    async update(entity: IAppUser): Promise<IFetchResponse<IAppUser>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .put(`${this.appUserEndpointUrl}/${entity.id}`, JSON.stringify(entity), { 
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
}
