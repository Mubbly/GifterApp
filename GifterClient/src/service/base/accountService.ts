import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import * as Environment from '../../../config/environment.json';
import { ILoginResponse } from 'types/ILoginResponse';
import { AppState } from 'state/appState';

@autoinject
export class AccountService {
    private readonly API_ENDPOINT_URL = Environment.backendUrlLocal;

    constructor(private appState: AppState, private httpClient: HttpClient) {
    }

    async login(email: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post(this.API_ENDPOINT_URL + ApiEndpointUrls.ACCOUNT_LOGIN, 
                JSON.stringify({
                    email: email, 
                    password: password
                }), 
                {
                    cache: 'no-store'
                }
            );

            if(response.ok) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    status: response.status,
                    data: data
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

    async register(email: string, firstName: string, lastName: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post(this.API_ENDPOINT_URL + ApiEndpointUrls.ACCOUNT_REGISTER, 
                JSON.stringify({
                    email: email, 
                    firstname: firstName,
                    lastname: lastName,
                    password: password
                }), 
                {
                    cache: 'no-store',
                }
            );

            if(response.ok) {
                const data = (await response.json()) as ILoginResponse;
                this.login(email, password);
                return {
                    status: response.status,
                    data: data
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
