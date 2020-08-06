import { Optional } from "types/generalTypes";
import { IAnswer } from './IAnswer';
import { IQuiz } from "./IQuiz";
import { IBaseEntity } from './base/IBaseEntity';

export interface IQuestionCreate {
    name: string;

    // Quiz that the question belongs to
    quizId: string;
    quiz?: IQuiz;

    // Answer choices that the question has
    answers?: Optional<IAnswer[]>;
}

export interface IQuestionEdit extends IBaseEntity, IQuestionCreate {

}

export interface IQuestion extends IQuestionEdit {

}
