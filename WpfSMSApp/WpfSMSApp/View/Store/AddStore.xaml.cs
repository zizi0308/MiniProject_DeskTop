﻿using System;
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
    public partial class AddStore : Page
    {
        public AddStore()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
                LblStoreName.Visibility = LblStoreLocation.Visibility =
                     Visibility.Hidden;

                TxtStoreID.Text = TxtStoreLocation.Text = TxtStoreName.Text = "";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        bool IsValid = true;

        public bool IsValidInput()
        {
            if (string.IsNullOrEmpty(TxtStoreName.Text))
            {
                LblStoreName.Visibility = Visibility.Visible;
                LblStoreName.Text = "창고명을 입력하세요";
                IsValid = false;
            }
            else
            {
                var cnt = Logic.DataAcess.GetStores().Where(u => u.StoreName.Equals(TxtStoreName.Text)).Count();
                if (cnt > 0)
                {
                    LblStoreName.Visibility = Visibility.Visible;
                    LblStoreName.Text = "중복된 창고명이 존재합니다";
                    IsValid = false;
                }
            }

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
            LblStoreLocation.Visibility = LblStoreName.Visibility =
                   Visibility.Hidden;

            var store = new Model.Store();
            isValid = IsValidInput(); // 유효성체크 >> 중복값, 입력값 확인 DB에 제대로 잘 들어갔는지 확인하기 위해 필수!!
            

            if (isValid)
            {
                //MessageBox.Show("DB 수정처리");
                store.StoreName = TxtStoreName.Text;
                store.StoreLocation = TxtStoreLocation.Text;

                try
                {
                    var result = Logic.DataAcess.SetStore(store);
                    if (result == 0)
                    {
                        // 수정안됨
                        Commons.LOGGER.Error("AddStore.xaml.cs 창고저장 오류발생");
                        Commons.ShowMessageAsync("오류", "저장 시 오류가 발생했습니다");
                        return;
                    }
                    else
                    {
                        //NavigationService.Navigate(new UserList());
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
