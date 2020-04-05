import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IUserCampaign } from 'domain/IUserCampaign';

@autoinject
export class UserCampaignService {
    private readonly _baseUrl = 'https://localhost:5001/api/UserCampaigns';

    constructor(private httpClient: HttpClient) {

    }

    getUserCampaigns(): Promise<IUserCampaign[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IUserCampaign[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getUserCampaign(id: string): Promise<Optional<IUserCampaign>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IUserCampaign) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
