using System;
using System.Collections.Generic;
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
    public class CampaignService : BaseEntityService<IAppUnitOfWork,
            ICampaignRepository, ICampaignServiceMapper, DALAppDTO.CampaignDAL, BLLAppDTO.CampaignBLL>,
        ICampaignService
    {
        public CampaignService(IAppUnitOfWork uow) : base(uow, uow.Campaigns, new CampaignServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            var personalCampaigns = new List<BLLAppDTO.CampaignBLL>();
            
            var userCampaigns = await UOW.UserCampaigns.GetAllAsync(userId);
            if (userCampaigns == null)
            {
                return personalCampaigns;
            }
            
            foreach (var userCampaign in userCampaigns)
            {
                var campaign = (await Repository.FirstOrDefaultAsync(userCampaign.CampaignId));
                if (campaign != null)
                {
                    personalCampaigns.Add(Mapper.Map(campaign));
                }
            }
            return personalCampaigns;
        }

        public new BLLAppDTO.CampaignBLL Add(BLLAppDTO.CampaignBLL bllCampaign, object? userId = null)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(userId as string);
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
        
        //
        // public virtual async Task<GpsLocation> AddAndUpdateSessionAsync(GpsLocation gpsLocation)
        // {
        //     // get the last location from uow
        //     // calculate updated metrics for session
        //     // update session
        //     // add location
        //
        //     var gpsSession = await UOW.GpsSessions.FirstOrDefaultAsync(gpsLocation.GpsSessionId, gpsLocation.AppUserId);
        //     var lastLocation = await UOW.GpsLocations.LastInSessionAsync(gpsSession.Id);
        //     if (lastLocation != null)
        //     {
        //         // calculate the metrics
        //         var distance = getDistance(gpsLocation, lastLocation);
        //         var vertical = getVerticalDistance(gpsLocation, lastLocation);
        //         gpsSession.Distance += distance;
        //         if (vertical < 0)
        //         {
        //             gpsSession.Descent += Math.Abs(vertical);
        //         } else if (vertical > 0)
        //         {
        //             gpsSession.Descent += vertical;
        //         }
        //
        //         gpsSession.Duration = (gpsSession.RecordedAt - lastLocation.RecordedAt).TotalSeconds; 
        //         
        //         await UOW.GpsSessions.UpdateAsync(gpsSession);
        //     }
        //     
        //     return base.Add(gpsLocation);
        // }
        //
        // private double getDistance(GpsLocation gpsLocation, DAL.App.DTO.GpsLocation lastLocation)
        // {
        //     var d1 = gpsLocation.Latitude * (Math.PI / 180.0);
        //     var num1 = gpsLocation.Longitude * (Math.PI / 180.0);
        //     var d2 = lastLocation.Latitude * (Math.PI / 180.0);
        //     var num2 = lastLocation.Longitude * (Math.PI / 180.0) - num1;
        //     var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        //
        //     return Math.Abs(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))));
        // }
        //
        // private double getVerticalDistance(GpsLocation gpsLocation, DAL.App.DTO.GpsLocation lastLocation)
        // {
        //     return gpsLocation.Altitude - lastLocation.Altitude;
        // }
    }
}