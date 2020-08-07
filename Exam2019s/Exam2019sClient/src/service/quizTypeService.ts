import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IQuizType } from 'domain/IQuizType';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IQuizTypeEdit, IQuizTypeCreate } from '../domain/IQuizType';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/generalTypes';

@autoinject
export class QuizTypeService extends BaseService<IQuizType, IQuizTypeCreate, IQuizTypeEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.QUIZ_TYPES, httpClient, appState);
    }
}
