import { autoinject } from 'aurelia-framework';
import { IDonatee } from 'domain/IDonatee';
import { DonateeService } from 'service/donateeService';

@autoinject
export class DonateesIndex {
    private _donatees: IDonatee[] = [];

    constructor(private donateeService: DonateeService) {

    }

    attached() {
        this.donateeService.getDonatees().then(
            data => this._donatees = data
        );
    }
}
