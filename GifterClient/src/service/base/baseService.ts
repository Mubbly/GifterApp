import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import * as Environment from '../../../config/environment.json';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IFetchResponse } from 'types/IFetchResponse';
import { IBaseEntity, IBaseCreateEntity, IBaseEditEntity } from 'domain/base/IBaseEntity';
import { AppState } from 'state/appState';
import { Optional } from 'types/generalTypes';

@autoinject
export class BaseService<TEntity extends IBaseEntity, TEntityCreate extends IBaseCreateEntity, TEntityEdit extends IBaseEditEntity> {
    private AUTH_HEADERS = {'Authorization': 'Bearer '};
    // private _jwt: Optional<string> = null;

    constructor(protected apiEndpointUrl: string, protected httpClient: HttpClient, protected appState: AppState) {
        this.httpClient.configure(config => { // TODO: change environment to backendUrl
            config
                .withBaseUrl(Environment.backendUrlLocal)
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

    async getAll(): Promise<IFetchResponse<TEntity[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(this.apiEndpointUrl, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as TEntity[];
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

    async get(id: string): Promise<IFetchResponse<TEntity>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${id}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as TEntity;
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

    async getAllPersonal(): Promise<IFetchResponse<TEntity[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as TEntity[];
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

    async getPersonal(id?: string): Promise<IFetchResponse<TEntity>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        let personalId = id ? id : '';
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/${personalId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as TEntity;
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

    async create(entity: TEntityCreate): Promise<IFetchResponse<TEntityCreate>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient.post(this.apiEndpointUrl, 
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

    async update(entity: TEntityEdit): Promise<IFetchResponse<TEntityEdit>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient.put(`${this.apiEndpointUrl}/${entity.id}`, JSON.stringify(entity),
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

    async delete(id: string): Promise<IFetchResponse<string>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient.delete(`${this.apiEndpointUrl}/${id}`, null, 
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
}
