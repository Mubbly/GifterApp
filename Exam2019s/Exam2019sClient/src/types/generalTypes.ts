import { IExample } from "domain/IExample";
import { IAppUser } from "domain/IAppUser";
import { ExampleService } from "service/exampleService";
import { AppUserService } from "service/base/appUserService";
import { IQuiz } from '../domain/IQuiz';
import { IQuestion } from '../domain/IQuestion';
import { IAnswer } from '../domain/IAnswer';
import { IQuizResponse } from '../domain/IQuizResponse';
import { QuizService } from '../service/quizService';
import { QuestionService } from '../service/questionService';
import { AnswerService } from '../service/answerService';
import { QuizResponseService } from '../service/quizResponseService';

// Types

export type Optional<TValue> = TValue | null;
export type ErrorMessage = string;
export type Id = string;
/** Expected format: YYYY-MM-DD */
export type HTML5DateString = string;

export enum ExamEntities {
    Example = "Example",
    AppUser = "AppUser",
    Quiz = "Quiz",
    Question = "Question",
    Answer = "Answer",
    QuizResponse = "QuizResponse"
}

export type ExamEntity =
    | ExamEntities.Example
    | ExamEntities.AppUser
    | ExamEntities.Quiz
    | ExamEntities.Question
    | ExamEntities.Answer
    | ExamEntities.QuizResponse;

export type ExamInterface =
    | IExample
    | IAppUser
    | IQuiz
    | IQuestion
    | IAnswer
    | IQuizResponse;

export type GeneralInterface<ExamInterface> = { props: ExamInterface };

export type ExamService =
    | ExampleService
    | AppUserService
    | QuizService
    | QuestionService
    | AnswerService
    | QuizResponseService;

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
