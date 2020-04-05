import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { INotification } from 'domain/INotification';

@autoinject
export class NotificationService {
    private readonly _baseUrl = 'https://localhost:5001/api/Notifications';

    constructor(private httpClient: HttpClient) {

    }

    getNotifications(): Promise<INotification[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: INotification[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getNotification(id: string): Promise<Optional<INotification>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: INotification) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
