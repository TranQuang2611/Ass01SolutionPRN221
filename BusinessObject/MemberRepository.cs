
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class MemberRepository : IMemberRepository
    {

        public List<Member> GetListMembers()
        {
            using(var context = new SaleDbContext())
            {
                return context.Members.ToList();
            }
        }

        public Member GetMember(string email, string password)
        {
            using (var context = new SaleDbContext())
            {
                return context.Members.FirstOrDefault(x => x.Email== email && x.Password == password);
            }
        }

        public bool LoginMember(string email, string password)
        {
            using (var context = new SaleDbContext())
            {
                Member member= context.Members.FirstOrDefault(x => x.Email== email && x.Password == password);
                if(member!=null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
