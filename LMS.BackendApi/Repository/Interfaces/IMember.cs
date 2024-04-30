using LMS.BackendApi.Models;
using LMS.BackendApi.Models.ViewModels;

namespace LMS.BackendApi.Repository.Interfaces
{
    public interface IMember
    {
        List<Member> GetMembersAll();
        int InsertMember(Member member);
        LoginResultVM LoginCheck(LoginVM loginModel);
    }
}
