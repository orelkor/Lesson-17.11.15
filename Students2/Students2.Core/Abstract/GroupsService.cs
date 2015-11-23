using Students2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students2.Core.Abstract
{
    public abstract class GroupsService
    {
        public abstract IEnumerable<Group> Groups();
        public abstract Group Save(Group group);
        public abstract void DeleteGroup(int groupId);
    }
}
