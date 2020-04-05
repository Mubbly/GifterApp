import { autoinject } from "aurelia-framework";
import { IDonatee } from "domain/IDonatee";
import { DonateeService } from "service/donateeService";
import * as UtilFunctions from "utils/utilFunctions";

@autoinject
export class DonateesIndex {
    private _donatees: IDonatee[] = [];

    constructor(private donateeService: DonateeService) {}

    attached() {
        this.getAllDonatees();
    }

    private getAllDonatees(): void {
        this.donateeService.getDonatees().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._donatees = response.data!;
            } else {
                UtilFunctions.alertErrorMessage(response);
            }
        });
    }
}
