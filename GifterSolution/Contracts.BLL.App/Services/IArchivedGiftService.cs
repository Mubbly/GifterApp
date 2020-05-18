using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IArchivedGiftService : IBaseEntityService<BLLAppDTO.ArchivedGiftBLL>,
        IArchivedGiftRepositoryCustom<BLLAppDTO.ArchivedGiftBLL>
    {
        // public virtual async Task<IEnumerable<BLLAppDTO.ArchivedGiftBLL>> GetAllPersonalAsync(Guid userId = null, bool noTracking = true)
        // {
        //     var dalEntities = await Repository.GetAllAsync(userId, noTracking);
        //     var result = dalEntities.Select(e => Mapper.Map(e));
        //     return result;
        // }
        //
        // // public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null,
        // //     bool noTracking = true)
        // // {
        // //     var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
        // //     var result = Mapper.Map(dalEntity);
        // //     return result;
        // // }
        //
        // public virtual async Task<IEnumerable<GpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null,
        //     bool noTracking = true)
        // {
        //     return (await Repository.GetAllAsync(gpsSessionId, userId, noTracking)).Select(e => Mapper.Map(e));
        // }
        //
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