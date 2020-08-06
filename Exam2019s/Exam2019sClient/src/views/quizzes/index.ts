import { autoinject } from 'aurelia-framework';
import { IQuiz } from 'domain/IQuiz';
import { Optional, IFetchResponse } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { QuizService } from 'service/quizService';
import * as Utils from 'utils/utilFunctions';
import { Router } from 'aurelia-router';
import { ExamInterface } from '../../types/generalTypes';

@autoinject
export class QuizzesIndex {
  public readonly NO_QUIZZES_MESSAGE = 'No quizzes found';

  private _quizzes: IQuiz[] = [];
  private _noQuizzesMessage: Optional<string> = null;
  private _errorMessage: Optional<string> = null;

  constructor(private appState: AppState,
        private router: Router,
        private quizService: QuizService) {
  }

  attached() {}

  activate() {
      if(!this.appState.jwt) {
          this.router.navigateToRoute(Utils.LOGIN_ROUTE);
      } else {
          this.getAllQuizzes();
      }
  }

  private getAllQuizzes(): void {
      this.quizService
      .getAll()
      .then((response) => {
          if(!Utils.isSuccessful(response)) {
              this.handleErrors(response);
          } else {
              if(!response.data || response.data.length <= 0) {
                  this._noQuizzesMessage = this.NO_QUIZZES_MESSAGE;
                  return;
              }
              this._noQuizzesMessage = null;
              this._quizzes = response.data;
              console.log('Quizzes: ', this._quizzes);
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
                this._noQuizzesMessage = this.NO_QUIZZES_MESSAGE;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
