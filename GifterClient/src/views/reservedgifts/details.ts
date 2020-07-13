// import { autoinject } from 'aurelia-framework';
// import { RouteConfig, NavigationInstruction } from 'aurelia-router';
// import { Optional } from 'types/generalTypes';
// import { IReservedGift } from 'domain/IReservedGift';
// import { ReservedGiftService } from 'service/reservedGiftService';
// import * as UtilFunctions from 'utils/utilFunctions';

// @autoinject
// export class ReservedGiftDetails {
//     private _reservedGift: Optional<IReservedGift> = null;
//     private _errorMessage: Optional<string> = null;

//     constructor(private reservedGiftService: ReservedGiftService) {}

//     attached() {}

//     activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
//         this.getReservedGift(params.id);
//     }

//     private getReservedGift(id: string): void {
//         if(UtilFunctions.existsAndIsString(id)) {
//             this.reservedGiftService.get(id).then(
//                 response => {
//                     if(UtilFunctions.isSuccessful(response)) {
//                         this._reservedGift = response.data!;
//                     } else {
//                         this._errorMessage = UtilFunctions.getErrorMessage(response);

//                     }
//                 }
//             )
//         }
//     }
// }
