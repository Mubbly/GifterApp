import { IQuizResponse } from './IQuizResponse';
import { Optional } from 'types/generalTypes';
import { IBaseEntity } from './base/IBaseEntity';
import { IQuestion } from './IQuestion';

export interface IAnswerCreate {
    name: string;

    // One answer might be set as true in case it is a poll
    isCorrect?: Optional<boolean>;

    // Question that the answer belongs to
    questionId: string;
    question?: IQuestion;

    // Responses, showing who has selected this answer
    quizResponses?: Optional<IQuizResponse[]>;
}

export interface IAnswerEdit extends IBaseEntity, IAnswerCreate {

}

export interface IAnswer extends IAnswerEdit {

}
