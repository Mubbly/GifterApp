using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class CampaignService : BaseEntityService<IAppUnitOfWork,
            ICampaignRepository, ICampaignServiceMapper, DALAppDTO.CampaignDAL, BLLAppDTO.CampaignBLL>,
        ICampaignService
    {
        public CampaignService(IAppUnitOfWork uow) : base(uow, uow.Campaigns, new CampaignServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            var personalCampaigns = await Repository.GetAllPersonalAsync(userId, noTracking);
            return personalCampaigns.Select(e => Mapper.Map(e));
            
            // var personalCampaigns = new List<BLLAppDTO.CampaignBLL>();
            //
            // var userCampaigns = await UOW.UserCampaigns.GetAllAsync(userId);
            // if (userCampaigns == null)
            // {
            //     return personalCampaigns;
            // }
            //
            // foreach (var userCampaign in userCampaigns)
            // {
            //     var campaign = (await Repository.FirstOrDefaultAsync(userCampaign.CampaignId));
            //     if (campaign != null)
            //     {
            //         personalCampaigns.Add(Mapper.Map(campaign));
            //     }
            // }
            // return personalCampaigns;
        }
        
        public virtual async Task<BLLAppDTO.CampaignBLL> GetPersonalAsync(Guid campaignId, Guid userId, bool noTracking = true)
        {
            var allPersonalCampaigns = await GetAllPersonalAsync(userId, noTracking);
            var personalCampaign = allPersonalCampaigns.Where(e => e.Id == campaignId);
        
            return personalCampaign.FirstOrDefault();
        }

        public new BLLAppDTO.CampaignBLL Add(BLLAppDTO.CampaignBLL bllCampaign, object? userId = null)
        {
            // UserId is mandatory for adding Campaign
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            
            // Add new Campaign
            var dalCampaign = Mapper.Map(bllCampaign);
            var dalCampaignTracked = Repository.Add(dalCampaign);

            UOW.AddToEntityTracker(dalCampaignTracked, bllCampaign);
            var bllNewCampaign = Mapper.Map(dalCampaignTracked);

            // Add new UserCampaign
            var userIdGuid = new Guid(userId.ToString());
            var bllUserCampaign = new BLLAppDTO.UserCampaignBLL()
            {
                AppUserId = userIdGuid,
                CampaignId = bllNewCampaign.Id
            };
            var dalUserCampaign = Mapper.MapUserCampaignToDAL(bllUserCampaign);
            var dalUserCampaignTracked = UOW.UserCampaigns.Add(dalUserCampaign);

            UOW.AddToEntityTracker(dalUserCampaignTracked, bllUserCampaign);
            Mapper.MapUserCampaignToBLL(dalUserCampaignTracked);

            return bllNewCampaign;
        }
    }
}