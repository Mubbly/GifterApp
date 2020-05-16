import { IFetchResponse } from 'types/IFetchResponse';
import { Optional, HTML5DateString } from 'types/generalTypes';

export const STATUS_CODE_OK = 200;
export const STATUS_CODE_LAST_SUCCESS = 300;
export const STATUS_CODE_REQUEST = 400;
export const STATUS_CODE_UNAUTHORIZED = 401;
export const STATUS_CODE_FORBIDDEN = 403;
export const STATUS_CODE_NOT_FOUND = 404;
export const STATUS_CODE_SERVER_ERROR = 500;

export const HOME_ROUTE = "homeIndex";
export const LOGIN_ROUTE = "accountLogin";

export function isSuccessful(response: Response | IFetchResponse<any>): boolean {
    return response.status >= STATUS_CODE_OK && response.status < STATUS_CODE_LAST_SUCCESS;
}

export function isEmpty(inputValue: Optional<string>): boolean {
    return inputValue === null || inputValue.length === 0;
}

export function existsAndIsString(value: any): value is string {
    return value && typeof value === 'string';
}

export function getErrorMessage(response: IFetchResponse<any>, comment?: string) {
    return `${response.status.toString()} ${response.errorMessage} ${comment ? ' - ' + comment : ''}`;
}

export function actionNotAllowed(): IFetchResponse<any> {
    return {
        status: 405,
        errorMessage: 'Action not allowed'
    }
}

/**
* Returns date as YYYY-MM-DD (or the same unedited date if parsing fails)
* so that html5 date input value could be filled in
* TODO: GET RID OF THIS STUPID TEMPORARY WORKAROUND AND USE SOME LIBRARY!
*/
export function formatAsHtml5Date(date: string): HTML5DateString | string {
   if(existsAndIsString(date)) {
       let newDate: Date = new Date();

       try {
          newDate = new Date(date);
          if(newDate) {
               let year = newDate.getFullYear();
               let month: number | string = newDate.getMonth() + 1; // +1 because january is 0
               month = month < 10 ? `0${month}` : month; // add 0 before one digit numbers
               let day: number | string = newDate.getDate();
               day = day < 10 ? `0${day}` : day; // add 0 before one digit numbers
       
               let html5Date = `${year}-${month}-${day}`;
               return html5Date;
          }
       } catch(error) {
           console.warn(`Could not parse given date (${date}) into html5 format: ${error}`);
           return date;
       }
   }
   console.warn(`Could not parse given date (${date}) into html5 format`);
   return date;
}
