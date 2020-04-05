import { autoinject } from 'aurelia-framework';
import { IStatus } from 'domain/IStatus';
import { StatusService } from 'service/statusService';

@autoinject
export class StatusesIndex {
    private _statuses: IStatus[] = [];

    constructor(private statusService: StatusService) {

    }

    attached() {
        this.statusService.getStatuses().then(
            data => this._statuses = data
        );
    }
}
