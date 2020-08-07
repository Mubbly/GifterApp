import * as DevEnvironment from '../../config/environment.json';

// Api endpoint base URL
export const API_BASE_URL = DevEnvironment.backendUrl; // Utils.getEnvironmentProperties().backendUrl;
// Account
export const ACCOUNT_LOGIN = 'account/login';
export const ACCOUNT_REGISTER = 'account/register';
// Data
export const APP_USERS = 'AppUsers';
export const EXAMPLES = 'Examples';
export const QUIZZES = 'Quizzes';
export const QUESTIONS = 'Questions';
export const ANSWERS = 'Answers';
export const QUIZ_RESPONSES = 'QuizResponses';
export const QUIZ_TYPES = 'QuizTypes'

// Specific keywords
export const PERSONAL = 'Personal';
export const USERS = 'Users';
export const REPORT = 'Report';
