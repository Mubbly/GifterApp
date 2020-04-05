import { IFetchResponse } from 'types/IFetchResponse';

export function isSuccessful(response: Response | IFetchResponse<any>): boolean {
    return response.status >= 200 && response.status < 300;
}

export function existsAndIsString(value: any): value is string {
    return value && typeof value === 'string';
}

export function alertErrorMessage(response: IFetchResponse<any>) {
    return alert(`${response.status.toString()} - ${response.errorMessage}`);
}
