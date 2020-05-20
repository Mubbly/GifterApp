export interface IFetchResponse<TData> {
    status: number;
    errorMessage?: string; // can be undefined
    data?: TData
}
