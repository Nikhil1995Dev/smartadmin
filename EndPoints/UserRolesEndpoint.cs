using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartAdmin.WebUI.Models;
using BITS.SmartAdmin.WebUI.Extensions.UIExtensions;

namespace BITS.SmartAdmin.WebUI.EndPoints
{
    [ApiController]
    [Route("api/userroles")]
    public class UserRolesEndpoint : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _manager;
        private readonly UserManager<ApplicationUser> _managerUser;
        private readonly SmartSettings _settings;

        public UserRolesEndpoint(RoleManager<ApplicationRole> manager, UserManager<ApplicationUser> managerUser, SmartSettings settings)
        {
            _manager = manager;
            _managerUser = managerUser;
            _settings = settings;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("lookuplist")]
        public ActionResult<IEnumerable<Claim>> GetAllRoles()
        {
            var roles = _manager.Roles.ToList();

            List<ClaimResponseResultChild> commResponse = new List<ClaimResponseResultChild>();

            foreach(ApplicationRole role in roles)
                commResponse.Add(new ClaimResponseResultChild() { Id = role.Name, Text = role.Name });

            return Ok(commResponse);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("lookuplist/{userid}")]
        public ActionResult<List<string>> GetRolesByUser(string userid)
        {
            List<string> commResponse = new List<string>();

            var currentUser = _managerUser.FindByIdAsync(userid).Result;

            var currentUserRoles = _managerUser.GetRolesAsync(currentUser).Result;

            var allRoles = _manager.Roles.ToList();

            foreach (var role in allRoles)
                if (currentUserRoles.Contains(role.Name))
                    commResponse.Add(role.Name);

            return Ok(commResponse);
        }

        [Route("adduserroles")]
        [HttpPost]
        public async Task<ActionResult> AddUserRole(string userid, string permissions)
        {
            var user = await _managerUser.FindByIdAsync(userid);

            if (user != null)
            {

                if (permissions == null)
                    return Ok(new ActionResponse() { ResponseCode = "error", ResponseMessage = "Please assign atleast one role to the user." });
                var userNewRolesFull = permissions.Split(",");

                var allroles = _manager.Roles.ToList();                

                var userOldRolesFull = _managerUser.GetRolesAsync(user).Result;

                foreach (var newRole in userNewRolesFull)
                {
                    if (!userOldRolesFull.Contains(newRole))
                    {
                        await _managerUser.AddToRoleAsync(user, newRole);
                    }
                }

                foreach (var oldClaim in userOldRolesFull)
                {
                    if (!userNewRolesFull.Contains(oldClaim))
                        await _managerUser.RemoveFromRoleAsync(user, oldClaim);
                }
                return Ok(new ActionResponse() { ResponseCode = "success", ResponseMessage = "Roles updated successful!!!!!!!" });
            }

            return Ok(new ActionResponse() { ResponseCode = "error", ResponseMessage = "Roles update NOT successful!!!!!!!" });
        }
    }
}
