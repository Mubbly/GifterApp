import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ICampaignDonatee } from '../domain/ICampaignDonatee';

@autoinject
export class CampaignDonateeService {
    private readonly _baseUrl = 'https://localhost:5001/api/CampaignDonatees';

    constructor(private httpClient: HttpClient) {

    }

    getCampaignDonatees(): Promise<ICampaignDonatee[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: ICampaignDonatee[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return []; 
            })
    }

    getCampaignDonatee(id: string) {

    }

    // TODO: .. update, delete etc
}
