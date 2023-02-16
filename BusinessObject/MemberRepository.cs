
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
    }
}
