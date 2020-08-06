import { IBaseEntity } from "./base/IBaseEntity";
import { IQuiz } from "./IQuiz";
import { Optional } from "types/generalTypes";
import { IQuizResponse } from "./IQuizResponse";

export interface ICurrentUser extends IBaseEntity {
    firstName: string,
    lastName: string
}

export interface IAppUser extends IBaseEntity {
    email: string;
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    dateJoined: string;

    // Quizzes that this user has created
    userQuizzes: Optional<IQuiz[]>;

    // Responses that this user has given to quizzes/polls
    userQuizResponses: Optional<IQuizResponse[]>;
}

export interface IAppUserEdit extends IBaseEntity {
}
