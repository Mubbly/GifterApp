import * as DevEnvironment from '../../config/environment.json';

// Api endpoint base URL
export const API_BASE_URL = DevEnvironment.backendUrl; // Utils.getEnvironmentProperties().backendUrl;
// Account
export const ACCOUNT_LOGIN = 'account/login';
export const ACCOUNT_REGISTER = 'account/register';
// Data
export const APP_USERS = 'AppUsers';
export const EXAMPLES = 'Examples';
// User specific
export const PERSONAL = 'Personal';
export const USER = 'User';
