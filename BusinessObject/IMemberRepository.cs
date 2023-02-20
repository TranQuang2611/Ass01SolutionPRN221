using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public interface IMemberRepository
    {
        List<Member> GetListMembers();
        bool LoginMember(string email, string password);
    }
}
