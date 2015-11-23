using Students2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students2.Core.Abstract
{
    public interface IGroupsRepository
    {
        IEnumerable<Group> GetGroups();
        int Add(Group group);
        void Edit(Group group);
        void Delete(int id);
    }
}
