import { autoinject } from "aurelia-framework";
import { IDonatee } from "domain/IDonatee";
import { DonateeService } from "service/donateeService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class DonateesIndex {
    private _donatees: IDonatee[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private donateeService: DonateeService) {}

    attached() {
        this.getDonatees();
    }

    private getDonatees(): void {
        this.donateeService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._donatees = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
