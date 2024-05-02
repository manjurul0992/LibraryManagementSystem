using LMS.BackendApi.Data;
using LMS.BackendApi.Models;
using LMS.BackendApi.Models.ViewModels;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.BackendApi.Repository.Implementation
{
    public class MemberRepo : IMember
    {
        private LmsDbContext _context;
        public MemberRepo(LmsDbContext _context)
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        public List<Member> GetMembersAll()
        {
            try
            {
                List<Member> members = _context.Members.ToList();
                return members;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsertMember(Member member)
        {
            try
            {
                _context.Members.Add(member);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public LoginResultVM LoginCheck(LoginVM loginModel)
        {
            LoginResultVM result = new LoginResultVM();
            result.LoginStatus = false;
            result.UserId = 0;
            try
            {
                var loginCheck = _context.Members.Where(x => x.Email == loginModel.UserName && x.Password == loginModel.Password).FirstOrDefault();
                if (loginCheck != null)
                {
                    result.LoginStatus = true;
                    result.UserId = loginCheck.MemberId;
                    return result;
                }
                else return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }
}
