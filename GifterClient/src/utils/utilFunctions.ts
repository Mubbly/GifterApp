import { IFetchResponse } from 'types/IFetchResponse';
import { Optional } from 'types/generalTypes';

export function isSuccessful(response: Response | IFetchResponse<any>): boolean {
    const OK_STATUS = 200;
    const LAST_SUCCESS_STATUS = 300;
    return response.status >= OK_STATUS && response.status < LAST_SUCCESS_STATUS;
}

export function isEmpty(inputValue: Optional<string>): boolean {
    return inputValue === null || inputValue.length === 0;
}

export function existsAndIsString(value: any): value is string {
    return value && typeof value === 'string';
}

export function alertErrorMessage(response: IFetchResponse<any>) {
    return alert(`${response.status.toString()} - ${response.errorMessage}`);
}
