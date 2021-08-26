using Prism.Commands;
using Prism.Navigation;
using UserTestApplication.Models;
using Xamarin.Forms;

namespace UserTestApplication.ViewModels
{
    public class UserDetailPageViewModel : ViewModelBase
    {
        #region FIELDS
        private readonly INavigationService _navigationService;

        private UserModel _selectedUser;
        private string _userName;
        private ImageSource _userImageSource;

        #endregion

        #region PROPERTIES

        public UserModel SelectedUser { get => _selectedUser; set => SetProperty(ref _selectedUser, value); }
        public string UserName { get => _userName; set => SetProperty(ref _userName, value); }
        public ImageSource UserImageSource { get => _userImageSource; set => SetProperty(ref _userImageSource, value); }

        private DelegateCommand _backCommand;
        public DelegateCommand BackCommand => _backCommand ?? (_backCommand = new DelegateCommand(ExecuteBack));
        #endregion

        #region CTOR
        public UserDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            Title = "User Detail Page";
        }
        #endregion

        #region NAVIGATION
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("SelectedUser"))
            {
                SelectedUser = parameters.GetValue<UserModel>("SelectedUser");
            }

            Initialize();
        }
        #endregion

        #region METHODS
        private void Initialize()
        {
            UserImageSource = SelectedUser.ImageUrl;
            UserName = SelectedUser.Name;
        }

        private async void ExecuteBack() 
        {
            await _navigationService.NavigateAsync("MainPage", null);
        }
        #endregion
    }
}
