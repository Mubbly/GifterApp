import { Optional } from "types/generalTypes";
import { IQuiz } from "./IQuiz";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IQuizTypeCreate {
    name: string;
    // Quizzes that have this type
    quizzes?: Optional<IQuiz[]>;
}

export interface IQuizTypeEdit extends IBaseEntity, IQuizTypeCreate {

}
export interface IQuizType extends IQuizTypeEdit {

}
