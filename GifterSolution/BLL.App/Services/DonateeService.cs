using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class DonateeService : BaseEntityService<IAppUnitOfWork,
            IDonateeRepository, IDonateeServiceMapper, DALAppDTO.DonateeDAL, BLLAppDTO.DonateeBLL>,
        IDonateeService
    {
        public DonateeService(IAppUnitOfWork uow) : base(uow, uow.Donatees, new DonateeServiceMapper())
        {
        }

        // TODO: Move to repo
        public async Task<IEnumerable<BLLAppDTO.DonateeBLL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId, bool noTracking = true)
        {
            var allCampaignDonatees = await UOW.CampaignDonatees.GetAllAsync();
            var donatees = await UOW.Donatees.GetAllAsync();

            return 
                from campaignDonatee in allCampaignDonatees
                join donatee in donatees
                    on campaignDonatee.DonateeId equals donatee.Id
                where campaignId == campaignDonatee.CampaignId
                select Mapper.Map(donatee);
        }

        public BLLAppDTO.DonateeBLL Add(BLLAppDTO.DonateeBLL bllDonatee, Guid campaignId, Guid userId)
        {
            if (campaignId == null)
            {
                throw new ArgumentNullException(nameof(campaignId));
            }
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Check if Campaign exists and current user is the owner of it
            var isUserCurrentCampaignOwner = UOW.UserCampaigns.FirstOrDefaultAsync(campaignId, userId);
            
            // Add new Donatee
            if (isUserCurrentCampaignOwner.Result != null)
            {
                throw new InvalidOperationException(userId + " user not allowed to add new Donatee to this Campaign: " + campaignId);
            }
            
            var dalDonatee = Mapper.Map(bllDonatee);
            var dalDonateeTracked = Repository.Add(dalDonatee);
            
            UOW.AddToEntityTracker(dalDonateeTracked, bllDonatee);
            var bllNewDonatee = Mapper.Map(dalDonateeTracked);
            
            // Add new CampaignDonatee
            var bllCampaignDonatee = new BLLAppDTO.CampaignDonateeBLL()
            {
                CampaignId = campaignId,
                DonateeId = bllNewDonatee.Id
            };
            var dalCampaignDonatee = Mapper.MapCampaignDonateeToDAL(bllCampaignDonatee);
            var dalCampaignDonateeTracked = UOW.CampaignDonatees.Add(dalCampaignDonatee);
            
            UOW.AddToEntityTracker(dalCampaignDonateeTracked, bllCampaignDonatee);
            Mapper.MapCampaignDonateeToBLL(dalCampaignDonateeTracked);
            
            return bllNewDonatee;
        }

    }
}