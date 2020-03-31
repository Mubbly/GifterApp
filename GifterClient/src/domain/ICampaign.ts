export interface ICampaign {
    id: string;
    name: string;
    description: string | null;
    adImage: string | null;
    institution: string | null;
    activeFromDate: string;
    activeToDate: string;
    isActive: boolean;
    
    userCampaignsCount: number;
    campaignDonateesCount: number;
}
