using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SetepassosPRJ.Models;

namespace SetepassosPRJ.Controllers
{
    [Route("api/IdentificarEquipaProjeto")]
    public class TeamMemberApiController : Controller
    {


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TeamMember tm = RepositorioTeamMember.GetMembro(id);
            if (tm != null)
            {
                return Ok(tm);
            }
            else
                return NotFound();
        }

        [HttpGet]
        public List<TeamMember> Get()
        {
            return RepositorioTeamMember.ListaMembros;
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
    }
}