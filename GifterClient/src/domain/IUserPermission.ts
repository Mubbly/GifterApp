import { IPermission } from './IPermission';
import { Optional } from 'types/generalTypes';
import { IAppUser } from './IAppUser';
export interface IUserPermission {
    id: string;
    from: string;
    to: string;
    comment: Optional<string>;

    appUserId: string;
    appUser: IAppUser;

    permissionId: string;
    permission: IPermission;
}
