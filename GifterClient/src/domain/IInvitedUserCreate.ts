import { Optional } from "types/generalTypes";

export interface IInvitedUserCreate {
    email: string;
    phoneNumber: Optional<string>;
    message: Optional<string>;
}
