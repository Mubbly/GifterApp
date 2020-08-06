import { Optional } from "types/generalTypes";
import { IQuiz } from "./IQuiz";

export interface IQuizType {
    name: string;
    // Quizzes that have this type
    quizzes?: Optional<IQuiz[]>;
}

// export interface IQuizResponseEdit extends IBaseEntity, IQuizResponseCreate {

// }
// export interface IQuizResponse extends IQuizResponseEdit {

// }
