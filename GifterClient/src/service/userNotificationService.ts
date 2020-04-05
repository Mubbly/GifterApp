import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IUserNotification } from 'domain/IUserNotification';

@autoinject
export class UserNotificationService {
    private readonly _baseUrl = 'https://localhost:5001/api/UserNotifications';

    constructor(private httpClient: HttpClient) {

    }

    getUserNotifications(): Promise<IUserNotification[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IUserNotification[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getUserNotification(id: string): Promise<Optional<IUserNotification>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IUserNotification) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
