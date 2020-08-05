import { autoinject } from 'aurelia-framework';
import { IExample } from 'domain/IExample';
import { Optional, IFetchResponse } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { ExampleService } from 'service/exampleService';
import * as Utils from 'utils/utilFunctions';
import { Router } from 'aurelia-router';
import { ExamInterface } from '../../types/generalTypes';

@autoinject
export class ExamplesIndex {
  public readonly NO_EXAMPLES_MESSAGE = 'No examples found';

  private _examples: IExample[] = [];
  private _noExamplesMessage: Optional<string> = null;
  private _errorMessage: Optional<string> = null;

  constructor(private appState: AppState,
        private router: Router,
        private exampleService: ExampleService) {
  }

  attached() {}

  activate() {
      if(!this.appState.jwt) {
          this.router.navigateToRoute(Utils.LOGIN_ROUTE);
      } else {
          this.getAllExamples();
      }
  }

  private getAllExamples(): void {
      this.exampleService
      .getAllPersonal()
      .then((response) => {
          if(!Utils.isSuccessful(response)) {
              this.handleErrors(response);
          } else {
              if(!response.data || response.data.length <= 0) {
                  this._noExamplesMessage = this.NO_EXAMPLES_MESSAGE;
                  return;
              }
              this._noExamplesMessage = null;
              this._examples = response.data;
              console.log('Examples: ', this._examples);
          }
      })
      .catch((error) => {
          console.log(error);
      });
  }
  
  /**
   * Set error message or route to login/home page
   */
   private handleErrors(response: IFetchResponse<ExamInterface | ExamInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            case Utils.STATUS_CODE_NOT_FOUND:
                this._noExamplesMessage = this.NO_EXAMPLES_MESSAGE;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
