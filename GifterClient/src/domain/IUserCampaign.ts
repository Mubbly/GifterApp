import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { ICampaign } from './ICampaign';

export interface IUserCampaign {
  id: string;
  comment: Optional<string>;

  appUserId: string;
  appUser: IAppUser;

  campaignId: string;
  campaign: ICampaign;
}
