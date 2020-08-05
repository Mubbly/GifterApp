using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;
using V1DTOIdentity = PublicApi.DTO.v1.Identity;

namespace WebApp.ApiControllers._1._0.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUserBLL> _userManager;
        private readonly AppUserMapper _mapper = new AppUserMapper();

        public AppUsersController(UserManager<AppUserBLL> userManager, IAppBLL bll)
        {
            _userManager = userManager;
            _bll = bll;
        }
        
        // TODO:fix, https://stackoverflow.com/questions/38751616/asp-net-core-identity-get-current-user

        // GET: api/AppUsers
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTOIdentity.AppUserDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTOIdentity.AppUserDTO>>> GetAppUsers()
        {
            var allDomainUsers = await _userManager.Users.ToListAsync();
            var allUsers = allDomainUsers.Select(u => _mapper.Map(u));
            return Ok(allUsers);
        }
        
        // GET: api/AppUsers/name/Alexandra
        [HttpGet("name/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTOIdentity.AppUserDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTOIdentity.AppUserDTO>>> GetAppUsersByName(string name)
        {
            // TODO: Move to BLL
            var lowercaseName = name.ToLower();
            var allDomainUsers = await _userManager.Users.ToListAsync();
            var domainUsersByName = allDomainUsers
                .Where(u => u.FirstName.ToLower().Contains(lowercaseName) 
                            || u.LastName.ToLower().Contains(lowercaseName) 
                            || u.FullName.ToLower().Contains(lowercaseName))
                .ToList();
            if (!domainUsersByName.Any())
            { 
                return NotFound(new V1DTO.MessageDTO($"Could not find users with name {name}"));
            }
            var usersByName = domainUsersByName.Select(u => _mapper.Map(u));
            return Ok(usersByName);
        }

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTOIdentity.AppUserDTO>))]
        public async Task<ActionResult<V1DTOIdentity.AppUserDTO>> GetAppUser(Guid id)
        {
            var domainUser = await _userManager.FindByIdAsync(id.ToString());
            if (domainUser == null)
            {
                return NotFound(new V1DTO.MessageDTO($"User with id {id.ToString()} not found"));
            }
            var user = _mapper.Map(domainUser);
            return Ok(user);
        }
        
        // GET: api/AppUsers/Personal
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTOIdentity.AppUserDTO>))]
        public async Task<ActionResult<V1DTOIdentity.AppUserDTO>> GetCurrentAppUser()
        {
            var domainUser = await _userManager.FindByIdAsync(User.UserGuidId().ToString());
            if (domainUser == null)
            {
                return NotFound(new V1DTO.MessageDTO("User not found"));
            }

            var userDTO = _mapper.Map(domainUser);
            return Ok(userDTO);   
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IEnumerable<V1DTOIdentity.AppUserDTO>))]
        public async Task<IActionResult> PutAppUser(Guid id, V1DTOIdentity.AppUserDTO appUser)
        {
            // Don't allow wrong data
            if (id != appUser.Id)
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot update the user with id {id.ToString()}"));
            }
            var domainUser = await _userManager.FindByIdAsync(id.ToString());
            if (domainUser == null)
            {
                return NotFound(new V1DTO.MessageDTO($"User with id {id.ToString()} not found"));
            }
            if (domainUser.Id != User.UserGuidId())
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot update the user with id {id.ToString()}"));
            }
            
            //await _userManager.UpdateSecurityStampAsync(_mapper.Map(appUser));
            domainUser.UserName = appUser.UserName;
            domainUser.Email = appUser.Email;
            domainUser.FirstName = appUser.FirstName;
            domainUser.LastName = appUser.LastName;
            
            // Update existing user
            await _userManager.UpdateAsync(domainUser);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // // DELETE: api/AppUsers/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<DomainIdentity.AppUser>> DeleteAppUser(Guid id)
        // {
        //     var appUser = await _context.Users.FindAsync(id);
        //     if (appUser == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Users.Remove(appUser);
        //     await _context.SaveChangesAsync();
        //
        //     return appUser;
        // }
    }
}

