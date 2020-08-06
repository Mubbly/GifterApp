import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IQuiz } from 'domain/IQuiz';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IQuizEdit, IQuizCreate } from '../domain/IQuiz';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';

@autoinject
export class QuizService extends BaseService<IQuiz, IQuizCreate, IQuizEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.QUIZZES, httpClient, appState);
    }
}
