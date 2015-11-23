using Students2.Core.Abstract;
using Students2.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students2.Core.Concrete
{
    public class DefaultGroupsServise : GroupsService
    {
        private readonly IGroupsRepository _groupsRepository;

        public DefaultGroupsServise(IGroupsRepository
            groupsRepository)
        {
            if (groupsRepository == null)
                throw new ArgumentNullException("groupsRepository");

            this._groupsRepository = groupsRepository;
        }

        public override IEnumerable<Entities.Group> Groups()
        {
            return _groupsRepository.GetGroups();
        }

        public override Entities.Group Save(Entities.Group group)
        {
            if (string.IsNullOrEmpty(group.Name))
                throw new ValidationException("не введено наименование группы");

            if (group.Id == 0) return AddGroup(group);
            else return EditGroup(group);
        }

        private Entities.Group EditGroup(Entities.Group group)
        {
            if (!_groupsRepository
                .GetGroups()
                .Any(x => x.Id == group.Id))
                throw new ArgumentException("группы с таким идентификатором не зарегистрирована");
            if (_groupsRepository
                .GetGroups()
                .Any(x => x.Name.CompareTo(group.Name) == 0
                && x.Id != group.Id))
                throw new ValidationException("группа с таким наименованием уже существует");

            try
            {
                _groupsRepository.Edit(group);
            }
            catch (Exception ex)
            {
                throw new
                    InvalidOperationException("произошла ошибка при редактирование группы", ex);
            }

            return group;
        }
        private Entities.Group AddGroup(Entities.Group newGroup)
        {
            if (_groupsRepository
                .GetGroups()
                .Any(x => x.Name.CompareTo(newGroup.Name) == 0))
                throw new ValidationException("группа с таким наименованием уже существует");

            int newId = 0;
            try
            {
                newId = _groupsRepository.Add(newGroup);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("произошла ошибка при добавление новой группы", ex);
            }

            return new Entities.Group(newId, newGroup.Name);
        }

        public override void DeleteGroup(int groupId)
        {
            if (!_groupsRepository
             .GetGroups()
             .Any(x => x.Id == groupId))
                throw new ValidationException("группа не найдена");

            _groupsRepository.Delete(groupId);
        }
    }
}
