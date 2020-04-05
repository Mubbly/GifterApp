import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { IDonatee } from 'domain/IDonatee';
import { IDonateeEdit } from 'domain/IDonateeEdit';
import * as UtilFunctions from 'utils/utilFunctions';
import { IDonateeCreate } from 'domain/IDonateeCreate';

@autoinject
export class DonateeService {
    private readonly _baseUrl = 'https://localhost:5001/api/Donatees';

    constructor(private httpClient: HttpClient) {

    }

    async getDonatees(): Promise<IFetchResponse<IDonatee[]>> {
        try {
            const response = await this.httpClient.fetch(this._baseUrl);

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IDonatee[];
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

    async getDonatee(id: string): Promise<IFetchResponse<IDonatee>> {
        try {
            const response = await this.httpClient.fetch(`${this._baseUrl}/${id}`);

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IDonatee;
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

    async createDonatee(donatee: IDonateeCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(donatee));

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

    async updateDonatee(donatee: IDonateeEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${donatee.id}`, JSON.stringify(donatee));

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

    async deleteDonatee(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.delete(`${this._baseUrl}/${id}`, null);

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
