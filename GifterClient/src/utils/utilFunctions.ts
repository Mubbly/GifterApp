import { IFetchResponse } from 'types/IFetchResponse';
import { Optional } from 'types/generalTypes';

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
