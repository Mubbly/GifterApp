// import { autoinject } from 'aurelia-framework';
// import { RouteConfig, NavigationInstruction } from 'aurelia-router';
// import { Optional } from 'types/generalTypes';
// import { ICampaignDonatee } from 'domain/ICampaignDonatee';
// import { CampaignDonateeService } from 'service/campaignDonateeService';

// @autoinject
// export class CampaignDonateeDetails {
//     private _campaignDonatees: ICampaignDonatee[] = [];
//     private _campaignDonatee: Optional<ICampaignDonatee> = null;

//     constructor(private campaignDonateeService: CampaignDonateeService) {

//     }

//     attached() {

//     }

//     activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
//         if(params.id && typeof(params.id) === 'string') {
//             this.campaignDonateeService.getCampaignDonatee(params.id).then(
//                 data => this._campaignDonatee = data
//             )
//         }
//     }
// }
