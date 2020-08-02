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
        
        public new async Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllAsync(object? userId, bool noTracking = true)
        {
            // UserId is mandatory for adding Campaign
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var campaigns = (await UOW.Campaigns.GetAllAsync(new Guid(userId.ToString()), noTracking))
                .Select(e => Mapper.Map(e)).ToList();
            campaigns.ForEach(c => c.CampaignDonateesCount = c.CampaignDonatees?.Count ?? 0);
            
            return campaigns;
        }

        public async Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            var personalCampaigns = (await UOW.Campaigns.GetAllPersonalAsync(userId, noTracking))
                .Select(e => Mapper.Map(e)).ToList();
            personalCampaigns.ForEach(c => c.CampaignDonateesCount = c.CampaignDonatees?.Count ?? 0);
            
            return personalCampaigns;
        }
        
        public async Task<BLLAppDTO.CampaignBLL> GetPersonalAsync(Guid campaignId, Guid userId, bool noTracking = true)
        {
            var allPersonalCampaigns = await GetAllPersonalAsync(userId, noTracking);
            var personalCampaign = allPersonalCampaigns.SingleOrDefault(e => e.Id == campaignId);
            personalCampaign.CampaignDonateesCount = personalCampaign.CampaignDonatees?.Count ?? 0;
        
            return personalCampaign;
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
            var dalCampaignTracked = UOW.Campaigns.Add(dalCampaign);

            UOW.AddToEntityTracker(dalCampaignTracked, bllCampaign);
            var bllNewCampaign = Mapper.Map(dalCampaignTracked);

            // Add new UserCampaign. TODO: Should be done via EF probably somehow
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