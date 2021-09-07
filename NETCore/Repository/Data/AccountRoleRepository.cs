using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRoleRepository, int>
    {
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
