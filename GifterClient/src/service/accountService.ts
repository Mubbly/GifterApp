import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import * as UtilFunctions from 'utils/utilFunctions';
import { ILoginResponse } from 'types/ILoginResponse';
import { AppState } from 'state/appState';

@autoinject
export class AccountService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
         this.httpClient.baseUrl = this.appState._baseUrl;
    }

    async login(email: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/login', 
                JSON.stringify({
                    email: email, 
                    password: password
                }), 
                {
                    cache: 'no-store'
                }
            );

            if(UtilFunctions.isSuccessful(response)) {
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

    
}
