import { autoinject } from 'aurelia-framework';
import { DonateeService } from '../../service/donatee-service';
import { IDonatee } from '../../domain/IDonatee';

@autoinject
export class DonateesIndex {
    private _donatees: IDonatee[] = [];

    constructor(private DonateeService: DonateeService) {

    }

    attached() {
        this.DonateeService.getDonatees().then(
            data => this._donatees = data
        );
    }
}
