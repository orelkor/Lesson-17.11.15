using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Students2.Core.Abstract;
using Students2.Core.Concrete;
using Students2.DAL;

namespace Students2.ViewModel
{
    public class ViewModelLocator
    {
        private readonly IGroupsRepository _groupsRepository;
        private readonly GroupsService _groupsService;
             
        public ViewModelLocator()
        {
            _groupsRepository =
                    new GroupsEFRepository();
            _groupsService = 
                new DefaultGroupsServise(_groupsRepository);
        }

        public MainViewModel Main
        {
            get
            {
                return new MainViewModel(_groupsService);
            }
        }       
        public GroupViewModel Group
        {
            get
            {
                return new GroupViewModel(_groupsService);
            }
        }

        public static void Cleanup()
        {
        }
    }
}