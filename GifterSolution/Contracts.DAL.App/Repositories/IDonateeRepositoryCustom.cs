using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDonateeRepositoryCustom : IDonateeRepositoryCustom<DALAppDTO.DonateeDAL>
    {
    }

    public interface IDonateeRepositoryCustom<TDonatee>
    {
    }
}