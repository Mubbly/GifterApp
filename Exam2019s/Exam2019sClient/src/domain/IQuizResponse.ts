import { IBaseEntity } from './base/IBaseEntity';
import { IAnswer } from './IAnswer';
import { IAppUser } from './IAppUser';
import { IQuestion } from './IQuestion';
import { IQuiz } from './IQuiz';

export interface IQuizResponseCreate {
    // Answer that was given
    answerId: string;
    answer?: IAnswer;

    // AppUser that gave the answer
    appUserId: string;
    appUser?: IAppUser;

    // Question that this answer belongs to
    questionId: string;
    question?: IQuestion;

    // Quiz that this question belongs to
    quizId: string;
    quiz?: IQuiz;
}

export interface IQuizResponseEdit extends IBaseEntity, IQuizResponseCreate {

}

export interface IQuizResponse extends IQuizResponseEdit {

}
