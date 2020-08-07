import { Optional } from "types/generalTypes";
import { IQuestion } from "./IQuestion";
import { IBaseEntity } from "./base/IBaseEntity";
import { IAppUser } from "./IAppUser";
import { IQuizType } from "./IQuizType";

export interface IQuizCreate {
    name: string;
    description: Optional<string>;
    quizTypeId: string;
    quizType?: IQuizType;

    // AppUser that created the quiz
    appUserId: string;
    appUser?: IAppUser;

    // Questions the quiz contains
    questions?: Optional<IQuestion[]>;
}

export interface IQuizEdit extends IBaseEntity, IQuizCreate {

}

export interface IQuiz extends IQuizEdit {

}
