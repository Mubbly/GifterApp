import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { INotificationType } from 'domain/INotificationType';

@autoinject
export class NotificationTypeService {
    private readonly _baseUrl = 'https://localhost:5001/api/NotificationTypes';

    constructor(private httpClient: HttpClient) {

    }

    getNotificationTypes(): Promise<INotificationType[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: INotificationType[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getNotificationType(id: string): Promise<Optional<INotificationType>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: INotificationType) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
