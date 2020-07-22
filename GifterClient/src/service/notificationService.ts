import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { INotification, INotificationCreate, INotificationEdit, IUserNotificationEdit } from 'domain/INotification';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import { IUserNotification } from 'domain/IUserNotification';

@autoinject
export class NotificationService extends BaseService<INotification, INotificationCreate, INotificationEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.NOTIFICATIONS, httpClient, appState);
    }

    async getAllPersonalActive(): Promise<IFetchResponse<IUserNotification[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/${ApiEndpointUrls.PERSONAL}/${ApiEndpointUrls.ACTIVE}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IUserNotification[];
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

    /** Mark as seen/read/inactive */
    async markAsInactive(notification: IUserNotification): Promise<IFetchResponse<IUserNotificationEdit>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        
        const targetUserNotificationId = notification.id;
        const targetNotification: IUserNotificationEdit = {
            id: targetUserNotificationId,
            isActive: false,
            comment: notification.comment,
            notificationId: notification.notificationId,
            appUserId: notification.appUserId
        }
        try {
            const response = await this.httpClient.put(`${this.apiEndpointUrl}/${targetUserNotificationId}`, 
            JSON.stringify(targetNotification),
                { 
                    cache: "no-store",
                    headers: AUTH_HEADERS
                }
            );
        
            if(response.ok) {
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
