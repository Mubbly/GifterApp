import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { INotificationType, INotificationTypeCreate, INotificationTypeEdit } from 'domain/INotificationType';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class NotificationTypeService extends BaseService<INotificationType, INotificationTypeCreate, INotificationTypeEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.NOTIFICATION_TYPES, httpClient, appState);
    }
}
