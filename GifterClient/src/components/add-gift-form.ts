import { bindable, autoinject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { EventTypes } from 'types/eventTypes';

@autoinject
export class AddGiftForm {
    @bindable public placeholder = "Gift description";
    @bindable public appTitle = "Add Gifts";
    @bindable public submitButtonTitle = "Add";

    private _input = "";

    constructor(private eventAggregator: EventAggregator) {

    }

    submitForm(event: Event) {
        if (this._input.length > 0) {
            this.eventAggregator.publish(EventTypes.NewGiftAddition, this._input);
        }
        this._input = "";
        event.preventDefault;
    }
}
