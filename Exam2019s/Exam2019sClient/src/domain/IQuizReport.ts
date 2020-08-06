import { Optional } from "types/generalTypes";

export interface IQuizReport {
    name: string;
    totalResponseCount: number;
    reportQuestions: IQuizQuestion[];
}

interface IQuizQuestion {
    name: string;
    reportAnswers: IQuizAnswer[];
}

interface IQuizAnswer {
    name: string;
    responseCount: number;
}
