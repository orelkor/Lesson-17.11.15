using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls;
using Students2.Core.Abstract;
using Students2.Core.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace Students2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly GroupsService _groupsService;

        public ObservableCollection<Group> Groups
        {
            get
            {
                return new ObservableCollection<Group>(_groupsService.Groups());
            }
        }

        public Group SelectedGroup { get; set; }

        private RelayCommand _addOpen;
        public ICommand AddOpen
        {
            get
            {
                if (_addOpen == null)
                    _addOpen = new RelayCommand(() =>
                    {
                        if (Students2.View.Group.Instance == null)
                        {
                            Students2.View.Group win = new Students2.View.Group();
                            ((GroupViewModel)win.DataContext).SavingResult += MainViewModel_SavingResult;
                            win.Show();
                        }
                        else
                            Students2.View.Group.Instance.Focus();
                    });

                return _addOpen;
            }
        }

        private RelayCommand _editOpen;
        public ICommand EditOpen
        {
            get
            {
                if (_editOpen == null)
                    _editOpen = new RelayCommand(() =>
                    {
                        if (Students2.View.Group.Instance == null)
                        {
                            Students2.View.Group win = new Students2.View.Group();
                            ((GroupViewModel)win.DataContext).SavingResult += MainViewModel_SavingResult;
                            ((GroupViewModel)win.DataContext).SetGroup(SelectedGroup);
                            win.Show();
                        }
                        else
                            Students2.View.Group.Instance.Focus();
                    },
                    () =>
                    { return SelectedGroup != null; });

                return _editOpen;
            }
        }

        private RelayCommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                    _delete = new RelayCommand(DeleteGroup,
                    () => { return SelectedGroup != null; });

                return _delete;
            }
        }

        private void DeleteGroup()
        {
            if (SelectedGroup == null) throw new InvalidOperationException("группа не выбрана");

            try
            {
                _groupsService.DeleteGroup(SelectedGroup.Id);
                RaisePropertyChanged("Groups");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Ошибка");
            }
        }

        private async void ShowMessage(string message, string title)
        {
            await ((MetroWindow)Application.Current.MainWindow)
                  .ShowMessageAsync(title, message);
        }

        private async void MainViewModel_SavingResult(object sender, SavingGroupEventArgs e)
        {
            if (e.Result)
            {
                RaisePropertyChanged("Groups");
                Students2.View.Group.Instance.Close();
            }
            else
            {
                Students2.View.Group.Instance.Close();
                ShowMessage(e.Message, "Ошибка");
            }
        }

        public class DeletingGroupEventArgs : EventArgs
        {
            public bool Result { get; private set; }
            public string Message { get; private set; }

            public DeletingGroupEventArgs(bool result, string message)
            {
                this.Result = result;
                this.Message = message;
            }
        }

        public MainViewModel(GroupsService groupsService)
        {
            if (groupsService == null) throw new ArgumentNullException("groupsService");

            this._groupsService = groupsService;
        }
    }
}