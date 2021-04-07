using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Linq;

namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditStore : Page
    {
        private int StoreID { get; set; }
        
        // 수정할 창고객체
        private Model.Store CurrentStore { get; set; }
        public EditStore()
        {
            InitializeComponent();
        }

        //추가생성자 생성 StoreList에서 storeId를 받아옴
        public EditStore(int storeId) : this()
        {
            StoreID = storeId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
                LblStoreName.Visibility = LblStoreLocation.Visibility =
                     Visibility.Hidden;

                TxtStoreID.Text = TxtStoreLocation.Text = TxtStoreName.Text = "";

            try
            {
                // StoreTbl에서 내용 읽음
                CurrentStore = Logic.DataAcess.GetStores().Where(s => s.StoreID.Equals(StoreID)).FirstOrDefault();
                TxtStoreID.Text = CurrentStore.StoreID.ToString();
                TxtStoreName.Text = CurrentStore.StoreName;
                TxtStoreLocation.Text = CurrentStore.StoreLocation;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"EditStore.xaml.cs Page_Loaded 예외발생 : {ex}");
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        bool IsValid = true; // 지역변수 ---> 전역변수

        public bool IsValidInput()
        {
            if (string.IsNullOrEmpty(TxtStoreName.Text))
            {
                LblStoreName.Visibility = Visibility.Visible;
                LblStoreName.Text = "창고명을 입력하세요";
                IsValid = false;
            }
            /*else
            {
                var cnt = Logic.DataAcess.GetStores().Where(u => u.StoreName.Equals(TxtStoreName.Text)).Count();
                if (cnt > 0)
                {
                    LblStoreName.Visibility = Visibility.Visible;
                    LblStoreName.Text = "중복된 창고명이 존재합니다";
                    IsValid = false;
                }
            }*/

            if (string.IsNullOrEmpty(TxtStoreLocation.Text))
            {
                LblStoreLocation.Visibility = Visibility.Visible;
                LblStoreLocation.Text = "창고위치를 입력하세요";
                IsValid = false;
            }

            return IsValid;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그
            LblStoreLocation.Visibility = LblStoreName.Visibility = Visibility.Hidden;

            isValid = IsValidInput(); // 유효성체크 >> 중복값, 입력값 확인 DB에 제대로 잘 들어갔는지 확인하기 위해 필수!!
            

            if (isValid)
            {
                //MessageBox.Show("DB 수정처리");
                CurrentStore.StoreName = TxtStoreName.Text;
                CurrentStore.StoreLocation = TxtStoreLocation.Text;

                try
                {
                    var result = Logic.DataAcess.SetStore(CurrentStore);
                    if (result == 0)
                    {
                        // 수정안됨
                        Commons.LOGGER.Error("AddStore.xaml.cs 창고정보 수정오류발생");
                        Commons.ShowMessageAsync("오류", "수정 시 오류가 발생했습니다");
                        return;
                    }
                    else
                    {
                        NavigationService.Navigate(new StoreList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }

            }
        }

        private void TxtStoreName_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput(); // 저장버튼누르기 전에 Tap만으로도 유효성체크(IsValidinput)하게 만드는 LostFocus이벤트
        }

        private void TxtStoreLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }
    }
}
