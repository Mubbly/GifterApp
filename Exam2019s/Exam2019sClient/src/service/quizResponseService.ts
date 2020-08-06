import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IQuizResponse } from 'domain/IQuizResponse';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IQuizResponseEdit, IQuizResponseCreate } from '../domain/IQuizResponse';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';
import { IQuizReport } from 'domain/IQuizReport';

@autoinject
export class QuizResponseService extends BaseService<IQuizResponse, IQuizResponseCreate, IQuizResponseEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.QUIZ_RESPONSES, httpClient, appState);
    }
    
    async getQuizReport(quizId: string): Promise<IFetchResponse<IQuizReport>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.QUIZZES}/${quizId}/report`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IQuizReport;
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

    async createMultiple(entities: IQuizResponseCreate[]): Promise<IFetchResponse<IQuizResponseCreate[]>> {
        console.log(entities);
        const responses = {
            quizResponses: entities
        };
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient.post(this.apiEndpointUrl, 
                JSON.stringify(responses),
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
