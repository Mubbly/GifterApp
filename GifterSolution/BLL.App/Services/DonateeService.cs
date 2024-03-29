﻿using System;
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
    public class DonateeService : BaseEntityService<IAppUnitOfWork,
            IDonateeRepository, IDonateeServiceMapper, DALAppDTO.DonateeDAL, BLLAppDTO.DonateeBLL>,
        IDonateeService
    {
        public DonateeService(IAppUnitOfWork uow) : base(uow, uow.Donatees, new DonateeServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.DonateeBLL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId, bool noTracking = true)
        {
            var campaignDonatees = await UOW.Donatees.GetAllForCampaignAsync(campaignId, userId, noTracking);
            return campaignDonatees.Select(e => Mapper.Map(e));
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
                throw new InvalidOperationException($"User {userId} not allowed to add this donatee");
            }
            
            var dalDonatee = Mapper.Map(bllDonatee);
            var dalDonateeTracked = UOW.Donatees.Add(dalDonatee);
            
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