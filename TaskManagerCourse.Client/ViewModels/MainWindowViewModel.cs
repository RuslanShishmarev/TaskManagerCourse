using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Services;
using TaskManagerCourse.Client.Views;
using TaskManagerCourse.Client.Views.Pages;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CommonViewService _viewService;
        private int _workTimeMinutes;

        #region COMMANDS

        public DelegateCommand OpenMyInfoPageCommand { get; private set; }
        public DelegateCommand OpenProjectsPageCommand { get; private set; }
        public DelegateCommand OpenDesksPageCommand { get; private set; }
        public DelegateCommand OpenTasksPageCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand OpenUsersManagementCommand;

        #endregion

        public MainWindowViewModel(AuthToken token, UserModel currentUser, Window currentWindow, int workTimeMinutes)
        {
            _viewService = new CommonViewService();
            _workTimeMinutes = workTimeMinutes;

            Token = token;
            CurrentUser = currentUser;
            _currentWindow = currentWindow;

            OpenMyInfoPageCommand = new DelegateCommand(OpenMyInfoPage);
            NavButtons.Add(_userInfoBtnName, OpenMyInfoPageCommand);

            OpenProjectsPageCommand = new DelegateCommand(OpenProjectsPage);
            NavButtons.Add(_userProjectsBtnName, OpenProjectsPageCommand);

            OpenDesksPageCommand = new DelegateCommand(OpenDesksPage);
            NavButtons.Add(_userDesksBtnName, OpenDesksPageCommand);

            OpenTasksPageCommand = new DelegateCommand(OpenTasksPage);
            NavButtons.Add(_userTasksBtnName, OpenTasksPageCommand);

            if(CurrentUser.Status == UserStatus.Admin)
            {
                OpenUsersManagementCommand = new DelegateCommand(OpenUsersManagement);
                NavButtons.Add(_manageUsersBtnName, OpenUsersManagementCommand);
            }

            LogoutCommand = new DelegateCommand(Logout);
            NavButtons.Add(_logoutBtnName, LogoutCommand);

            StartWork(_workTimeMinutes);

            OpenMyInfoPage();
        }

        #region PROPERTIES

        private readonly string _userProjectsBtnName = "My projects";
        private readonly string _userDesksBtnName = "My desks";
        private readonly string _userTasksBtnName = "My tasks";
        private readonly string _userInfoBtnName = "My info";
        private readonly string _logoutBtnName = "Logout";

        private readonly string _manageUsersBtnName = "Users";

        private Window _currentWindow;

        private AuthToken _token;

        public AuthToken Token
        {
            get => _token;
            private set 
            { 
                _token = value;
                RaisePropertyChanged(nameof(Token));
            }
        }

        private UserModel _currentUser;

        public UserModel CurrentUser
        {
            get => _currentUser;
            private set 
            { 
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }


        private Dictionary<string, DelegateCommand> _navButtons = new Dictionary<string, DelegateCommand>();

        public Dictionary<string, DelegateCommand> NavButtons
        {
            get => _navButtons;
            set 
            {
                _navButtons = value;
                RaisePropertyChanged(nameof(NavButtons));
            }
        }

        private string _selectedPageName;
        public string SelectedPageName
        {
            get => _selectedPageName;
            set 
            { 
                _selectedPageName = value;
                RaisePropertyChanged(nameof(SelectedPageName));
            }
        }


        private Page _selectedPage;
        public Page SelectedPage
        {
            get =>  _selectedPage;
            set 
            { 
                _selectedPage = value;

                RaisePropertyChanged(nameof(SelectedPage));
            }
        }


        #endregion

        #region METHODS

        private void OpenMyInfoPage()
        {
            var page = new UserInfoPage();
            OpenPage(page, _userInfoBtnName, this);
        }
        private void OpenProjectsPage()
        {
            var page = new ProjectsPage();
            OpenPage(page, _userProjectsBtnName, new ProjectsPageViewModel(Token, this));
        }

        private void OpenDesksPage()
        {
            var page = new UserDesksPage();
            OpenPage(page, _userDesksBtnName, new UserDesksPageViewModel(Token));
        }


        private void OpenTasksPage()
        {
            var page = new UserTasksPage();
            OpenPage(page, _userTasksBtnName, new UserTasksPageViewModel(Token));
        }

        private void Logout()
        {
            var question = MessageBox.Show("Are you sure?", "Logout", MessageBoxButton.YesNo);
            if(question == MessageBoxResult.Yes && _currentWindow != null)
            {
                Login login = new Login();
                login.Show();
                _currentWindow.Close();
            }
        }

        private void StopWork()
        {
            Login login = new Login();
            login.Show();
            _currentWindow.Close();            
        }

        private void OpenUsersManagement()
        {
            SelectedPageName = _manageUsersBtnName;
            var page = new UsersPage();
            OpenPage(page, _manageUsersBtnName, new UsersPageViewModel(Token));
        }

        #endregion

        public void OpenPage(Page page, string pageName, BindableBase viewModel)
        {
            SelectedPageName = pageName;
            SelectedPage = page;
            SelectedPage.DataContext = viewModel;
        }

        private async void StartWork(int minutes)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(minutes * 1000);
            });
            StopWork();
        }
    }
}
