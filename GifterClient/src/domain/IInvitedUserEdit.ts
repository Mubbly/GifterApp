import { Optional } from "types/generalTypes";

export interface IInvitedUserEdit {
    id: string;
    email: string;
    phoneNumber: Optional<string>;
    message: Optional<string>;
}
