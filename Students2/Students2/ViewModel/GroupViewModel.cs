using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Students2.Core.Abstract;
using Students2.Core.Entities;
using Students2.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Students2.ViewModel
{
    public class GroupViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly GroupsService _groupService;
        private int _id;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        public event EventHandler<SavingGroupEventArgs> SavingResult;

        private RelayCommand _save;
        public ICommand Save
        {
            get
            {
                if (_save == null) _save = new RelayCommand(SaveGroup, ModelIsValid);

                return _save;
            }
        }

        private void SaveGroup()
        {
            try
            {
                _groupService.Save(new Core.Entities.Group(_id, _name));
                if (SavingResult != null)
                    SavingResult(this, new SavingGroupEventArgs(true, string.Empty));
            }
            //catch (ValidationException ex)
            //{ }
            //catch (InvalidOperationException ex)
            //{ }
            catch (Exception ex)
            {
                if (SavingResult != null)
                    SavingResult(this, new SavingGroupEventArgs(false, ex.Message));
            }
        }

        private bool ModelIsValid()
        {
            if (string.IsNullOrEmpty(Name)) return false;

            return true;
        }

        public void SetGroup(Group group)
        {
            _id = group.Id;
            Name = group.Name;
        }
        public GroupViewModel(GroupsService groupService)
        {
            if (groupService == null) throw new ArgumentNullException("groupService");

            this._groupService = groupService;
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && string.IsNullOrEmpty(Name)) return "введите наименование";

                return null;
            }
        }
    }

    public class SavingGroupEventArgs : EventArgs
    {
        public bool Result { get; private set; }
        public string Message { get; private set; }

        public SavingGroupEventArgs(bool result, string message)
        {
            this.Result = result;
            this.Message = message;
        }
    }
}
