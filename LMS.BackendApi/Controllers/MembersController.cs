using LMS.BackendApi.Models.ViewModels;
using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.BackendApi.Data;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMember member;
        private readonly LmsDbContext _context;
        public MembersController(IMember member, LmsDbContext _context)
        {
            this.member = member;
            this._context = _context;
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

        [HttpGet]
        [Route("GetMemberById/{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var memberVM = await _context.Members.FindAsync(id);

            if (memberVM == null)
            {
                return NotFound();
            }

            return memberVM;
        }

        [HttpPut("EditMember/{id}")]
        public async Task<IActionResult> EditMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            if (!MemberVMExists(id))
            {
                return NotFound();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberVMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool MemberVMExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }

        [HttpDelete("DeleteMember/{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var memberVM = await _context.Members.FindAsync(id);
            if (memberVM == null)
            {
                return NotFound();
            }

            _context.Members.Remove(memberVM);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        [Route("Login")]
        public LoginResultVM Login(LoginVM loginModel)
        {
            return member.LoginCheck(loginModel);
        }



         

    }
}
