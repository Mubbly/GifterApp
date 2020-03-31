import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ICampaign } from '../domain/ICampaign';

@autoinject
export class CampaignService {
    private readonly _baseUrl = 'https://localhost:5001/api/Campaigns';

    constructor(private httpClient: HttpClient) {

    }

    getCampaigns(): Promise<ICampaign[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: ICampaign[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return []; 
            })
    }

    getCampaign(id: string) {

    }

    // TODO: .. update, delete etc
}
