import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IQuestion } from 'domain/IQuestion';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IQuestionEdit, IQuestionCreate } from '../domain/IQuestion';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';

@autoinject
export class QuestionService extends BaseService<IQuestion, IQuestionCreate, IQuestionEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.QUESTIONS, httpClient, appState);
    }

    async getAllForQuiz(quizId: string): Promise<IFetchResponse<IQuestion[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.QUIZZES}/${quizId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IQuestion[];
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
}
