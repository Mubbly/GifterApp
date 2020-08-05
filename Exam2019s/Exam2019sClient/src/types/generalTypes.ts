import { IExample } from "domain/IExample";
import { IAppUser } from "domain/IAppUser";
import { ExampleService } from "service/exampleService";
import { AppUserService } from "service/base/appUserService";

// Types

export type Optional<TValue> = TValue | null;
export type ErrorMessage = string;
export type Id = string;
/** Expected format: YYYY-MM-DD */
export type HTML5DateString = string;

export enum ExamEntities {
    Example = "Example",
    AppUser = "AppUser"
}

export type ExamEntity =
    | ExamEntities.Example
    | ExamEntities.AppUser;

export type ExamInterface =
    | IExample
    | IAppUser;

export type GeneralInterface<ExamInterface> = { props: ExamInterface };

export type ExamService =
    | ExampleService
    | AppUserService;

export enum ViewTypes {
    Index = "Index",
    Details = "Details",
    Edit = "Edit",
    Delete = "Delete",
    Create = "Create"
}

export enum RequestTypes {
    CREATE = "POST",
    READ = "GET",
    UPDATE = "PUT",
    DELETE = "DELETE"
}

// Interfaces

export interface ILoginResponse {
    token: string;
    status: string; 
    id: string;
    firstName: string;
    lastName: string;
}

export interface IFetchResponse<TData> {
    status: number;
    errorMessage?: string; // can be undefined
    data?: TData
}
