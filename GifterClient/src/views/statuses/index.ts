import { autoinject } from 'aurelia-framework';
import { StatusService } from '../../service/status-service';
import { IStatus } from '../../domain/IStatus';

@autoinject
export class StatusesIndex {
    private _statuses: IStatus[] = [];

    constructor(private StatusService: StatusService) {

    }

    attached() {
        this.StatusService.getStatuses().then(
            data => this._statuses = data
        );
    }
}
