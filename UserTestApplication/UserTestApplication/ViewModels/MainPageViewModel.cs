using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestApplication.Models;
using UserTestApplication.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UserTestApplication.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region FIELDS
        private readonly IUserServices _userServices;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private UserModel _selectedUser;

        private string _connectionErrorText;
        private string _userRequestErrorText;
        private string _errorMessageText;

        private bool _isListVisible;

        private List<UserModel> _userList;
        #endregion

        #region PROPERTIES
        public UserModel SelectedUser { get => _selectedUser; set => SetProperty(ref _selectedUser, value); }
        public string ErrorMessageText { get => _errorMessageText; set => SetProperty(ref _errorMessageText, value); }
        public bool IsListVisible { get => _isListVisible; set => SetProperty(ref _isListVisible, value); }
        public List<UserModel> UserList { get => _userList; set => SetProperty(ref _userList, value); }
        #endregion

        #region COMMANDS
        private DelegateCommand _selectUserCommand;
        public DelegateCommand SelectUserCommand => _selectUserCommand ?? (_selectUserCommand = new DelegateCommand(ExecuteSelectUser));
        #endregion

        #region CTOR
        public MainPageViewModel(INavigationService navigationService,IUserServices userServices, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            _userServices = userServices;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            Title = "Main Page";
        }
        #endregion

        #region NAVIGATION
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Initialize();

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
                await PopulateListView();
            else 
            {
                IsListVisible = false;
                ErrorMessageText = _connectionErrorText;
            }
               
        }

        #endregion

        #region METHODS
        private void Initialize()
        {
            _connectionErrorText = "Connection Error";
            _userRequestErrorText = "User Request Error";
            IsListVisible = true;
        }

        private async Task PopulateListView()
        {

            var list = await _userServices.GetUsers();

            if (list != null)
                UserList = list.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            else 
            {
                IsListVisible = false;
                ErrorMessageText = _userRequestErrorText;
            }
        }

        private async void ExecuteSelectUser()
        {
            NavigationParameters parameters = new NavigationParameters();
          
            parameters.Add("SelectedUser", SelectedUser);

            await _navigationService.NavigateAsync("UserDetailPage", parameters);

        }
        #endregion
    }
}
