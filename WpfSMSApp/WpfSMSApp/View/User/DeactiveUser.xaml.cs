using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Linq;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeactiveUser : Page
    {
        public DeactiveUser()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 그리드 바인딩
                List<Model.User> users = Logic.DataAcess.GetUsers();
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            if (GrdData.SelectedItem == null)
            {
                await Commons.ShowMessageAsync("오류", $"비활성화할 사용자를 선택하세요");
                //MessageBox.Show("비활성화 할 사용자를 선택하세요");
                return;
            }

            if (isValid)
            {

                try
                {
                    var user = GrdData.SelectedItem as Model.User;
                    user.UserActivated = false; // 사용자비활성화

                    var result = Logic.DataAcess.SetUser(user);
                    if (result == 0)
                    {
                        await Commons.ShowMessageAsync("오류", $"사용자 수정에 실패했습니다.");
                    }
                    else
                    {
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                // 선택된 값 입력창에 나오도록
                var user = GrdData.SelectedItem as Model.User;

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 GrdData_SelectedCellsChanged : {ex}");
            }

        }
      
    }
}
