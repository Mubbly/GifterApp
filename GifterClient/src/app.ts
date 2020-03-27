import { ITodo } from "domain/ITodo";

export class App {
  private _todos: ITodo[] = [];

  private _placeholder = "Gift description";
  private _appTitle = "Add Gifts";
  private _submitButtonTitle = "Add";
  private _input = ""; // binded in html

  submitForm(event: Event) {
    if(this._input.length > 0) {
      this._todos.push({description: this._input, done: false});
      this._input = "";
    }
    event.preventDefault;
  }

  removeTodo(index: number) {
    this._todos.splice(index, 1);
  }

}
