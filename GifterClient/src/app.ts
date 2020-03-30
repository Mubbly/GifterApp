import { EventAggregator, Subscription } from 'aurelia-event-aggregator';
import { autoinject } from 'aurelia-framework';
import { IGifterApp } from "domain/IGifterApp";
import { EventTypes } from 'types/EventTypes';

@autoinject
export class App {
  private _subscriptions: Subscription[] = [];
  private _todos: IGifterApp[] = [];

  private _placeholder = "Gift description";
  private _appTitle = "Add Gifts";
  private _submitButtonTitle = "Add";
  private _input = ""; // binded in html

  constructor(private eventAggregator: EventAggregator) {
    this._subscriptions.push(
      this.eventAggregator.subscribe(
        EventTypes.NewGiftAddition,
        (desc: string) => this.eventNewGiftAddition(desc)
      )
    );
  }

  detach() {
    this._subscriptions.forEach(subscription => subscription.dispose());
    this._subscriptions = [];
  }

  eventNewGiftAddition(description: string) {
    this._todos.push({ description: description, done: false });
  }

  removeTodo(index: number) {
    this._todos.splice(index, 1);
  }
}

