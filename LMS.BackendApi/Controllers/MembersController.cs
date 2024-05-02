using LMS.BackendApi.Models.ViewModels;
using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMember member;
        public MembersController(IMember member)
        {
            this.member = member;
        }


        [HttpGet]
        [Route("GetMembers")]
        public List<Member> Get()
        {
            return member.GetMembersAll();
        }

        [HttpPost]
        [Route("InsertMembers")]
        public int Insert(Member members)
        {
            return member.InsertMember(members);
        }

        [HttpPost]
        [Route("Login")]
        public LoginResultVM Login(LoginVM loginModel)
        {
            return member.LoginCheck(loginModel);
        }

    }
}
