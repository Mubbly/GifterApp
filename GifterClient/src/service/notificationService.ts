import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { INotification, INotificationCreate, INotificationEdit } from 'domain/INotification';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class NotificationService extends BaseService<INotification, INotificationCreate, INotificationEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.NOTIFICATIONS, httpClient, appState);
    }
}
