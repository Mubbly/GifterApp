import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IArchivedGift } from 'domain/IArchivedGift';
import { ArchivedGiftService } from 'service/archivedGiftService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class ArchivedGiftDetails {
    private _archivedGift: Optional<IArchivedGift> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private archivedGiftService: ArchivedGiftService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getArchivedGift(params.id);
    }

    private getArchivedGift(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.archivedGiftService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._archivedGift = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}
