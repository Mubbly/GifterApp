import { QuestionService } from 'service/questionService';
import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IAnswer } from 'domain/IAnswer';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IAnswerEdit, IAnswerCreate } from '../domain/IAnswer';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';

@autoinject
export class AnswerService extends BaseService<IAnswer, IAnswerCreate, IAnswerEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.ANSWERS, httpClient, appState);
    }

    async getAllForQuestion(questionId: string): Promise<IFetchResponse<IAnswer[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.QUESTIONS}/${questionId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IAnswer[];
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
