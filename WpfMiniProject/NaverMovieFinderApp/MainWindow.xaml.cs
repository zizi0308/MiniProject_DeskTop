using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NaverMovieFinderApp.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaverMovieFinderApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// MetroWindow 기본설정
    /// UI 구현 및 이벤트 생성
    /// 네이버 API 받아오기
    /// API 목록 DB구현
    /// EntityFrameWork로 DB데이터 받아옴 (추가 - 새항목 - 데이터 - ADO.NET 엔티티)
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            StsResult.Content = ""; // 상태바의 내용이 없을 때
            ImgPoster.Source = new BitmapImage(new Uri("no_picture.jpg", UriKind.RelativeOrAbsolute));  // 이미지 사용 시 필요한 Uri(상대경로)


            if (string.IsNullOrEmpty(TxtMovieName.Text))
            {
                StsResult.Content = "검색할 영화명을 입력 후, 검색버튼을 눌러주세요.";
                Commons.ShowMessageAsync("검색", "검색할 영화명을 입력 후, 검색버튼을 눌러주세요.");
                return;
            }

            try
            {
                //Commons.ShowMessageAsync("결과", $"{TxtMovieName.Text}");
                PorcSearchNaverApi(TxtMovieName.Text);
                Commons.ShowMessageAsync("검색", "영화검색 완료");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }

            Commons.IsFavorite = false; // 즐겨찾기 아님
        }

        /*Naver API 사용을 위한 사용자 정보입력 부분*/
        private void PorcSearchNaverApi(string movieName)
        {
            // API 호츨
            string clientID = "kOQI3W4J8QmJV8UukmBZ";       // 발급받은 API 아이디
            string clientSecret = "e9EjOaik91";             // 발급받은 API 비밀번호
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?start=1&display=30&query={movieName}";

            string resJson = Commons.GetOpenApiResult(openApiUrl, clientID, clientSecret);
            JObject parsedJson = JObject.Parse(resJson);    // API 에서 발급받은 정보(string)를 JSON으로 파싱

            int total = Convert.ToInt32(parsedJson["total"]);
            int display = Convert.ToInt32(parsedJson["display"]);

            StsResult.Content = $"{total} 중 {display} 호출 성공";

            JToken items = parsedJson["items"];
            JArray json_array = (JArray)items;

            List<MovieItem> movieItems = new List<MovieItem>(); // API에서 불러온 데이터들을 저장할 리스트 선언
            foreach (var item in json_array)
            {
                MovieItem movie = new MovieItem(
                    Commons.StripHtmlTag(item["title"].ToString()),
                    item["link"].ToString(),
                    item["image"].ToString(),
                    item["subtitle"].ToString(),
                    item["pubDate"].ToString(),
                    Commons.StripPipe(item["director"].ToString()),
                    Commons.StripPipe(item["actor"].ToString()),
                    item["userRating"].ToString() );
                movieItems.Add(movie);
            }

            this.DataContext = movieItems;      // 그리드에 데이터 바인딩
        }

        private void TxtMovieName_KeyDown(object sender, KeyEventArgs e)    // 엔터 시 검색버튼 클릭
        {
            if (e.Key == Key.Enter) BtnSearch_Click(sender, e);
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)   // 셀 선택변경시 이미치 변경처리
        {
            //if (GrdData.SelectedItem == null)
            //{
            //    Commons.ShowMessageAsync("오류", "영화를 선택하세요");
            //    return;
            //}

            if (GrdData.SelectedItem is MovieItem)
            {
                var movie = GrdData.SelectedItem as MovieItem;
                //Commons.ShowMessageAsync("결과", $"{movie.Image}");
                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("no_picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                }
            }

            if (GrdData.SelectedItem is NaverFavoriteMovies)
            {
                var movie = GrdData.SelectedItem as NaverFavoriteMovies;
                //Commons.ShowMessageAsync("결과", $"{movie.Image}");
                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("no_picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                }
            }

        }

        private void BtnAddWatchList_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("오류", "즐겨찾기에 추가할 영화를 선택하세요(복수선택가능)");
                return;
            }

            if (Commons.IsFavorite)
            {
                // 이미 즐겨찾기 한 내용을 막아줌
                Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회내용을 다시 즐겨찾기할 수 없습니다");
                return;
            }

            List<NaverFavoriteMovies> list = new List<NaverFavoriteMovies>();
            
            foreach (MovieItem item in GrdData.SelectedItems)
            {
                NaverFavoriteMovies temp = new NaverFavoriteMovies()
                {
                    Title = item.Title,
                    Link = item.Link,
                    Image = item.Image,
                    SubTitle = item.SubTitle,
                    PubDate = item.PubDate,
                    Director = item.Director,
                    Actor = item.Actor,
                    UserRating = item.UserRating,
                    RegDate = DateTime.Now
                };

                list.Add(temp);
            }

            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    // ctx.NaverFavoriteMovies.AddRange(list); // 아래와 차이없음
                    ctx.Set<NaverFavoriteMovies>().AddRange(list);
                    ctx.SaveChanges();
                }

                Commons.ShowMessageAsync("저장", "즐겨찾기에 추가했습니다");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
            }

        }

        // 즐겨찾기 보기 버튼 클릭 이벤트
        private void BtnViewWatchList_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            TxtMovieName.Text = "";

            // List<MovieItem> listData = new List<MovieItem>();
            List<NaverFavoriteMovies> list = new List<NaverFavoriteMovies>();   // DB값을 저장할 리스트 선언

            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    list = ctx.NaverFavoriteMovies.ToList();
                }
                this.DataContext = list;
                StsResult.Content = $"즐겨찾기 {list.Count}개 조회";
                if (Commons.IsDelete)
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기삭제 완료");
                
                else 
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기보기 조회 완료");
                
                Commons.IsFavorite = true; // 즐겨찾기한 부분
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"예외발생 : {ex}");
                Commons.LOGGER.Error($"예외발생 : {ex}");
                Commons.IsFavorite = false; // 한번 더 명시
            }

            /* // 변환 필요없음
            foreach (var item in list)
            {
                listData.Add(new MovieItem(
                    item.Title,
                    item.Link,
                    item.Image,
                    item.SubTitle,
                    item.PubDate,
                    item.Director,
                    item.Actor,
                    item.UserRating
                ));
            }

            this.DataContext = listData;
            StsResult.Content = $"즐겨찾기 {listData.Count}개 조회";
            Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기보기 조회 완료");
            Commons.IsFavorite = true; // 즐겨찾기한 부분
            */
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Commons.IsFavorite == false)
            {
                Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 내용이 아니면 삭제할 수 없습니다.");
                return;
            }

            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("즐겨찾기", "삭제할 즐겨찾기 영화를 선택하세요");
                return;
            }

            //List<NaverFavoriteMovies> removelist = new List<NaverFavoriteMovies>();
            foreach (NaverFavoriteMovies item in GrdData.SelectedItems)
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    // ctx.NaverFavoriteMovies.Remove(item);
                    var itemDelete = ctx.NaverFavoriteMovies.Find(item.Idx); // 삭제할 영화 객체 검색 후 생성
                    ctx.Entry(itemDelete).State = EntityState.Deleted; // 검색객채 상태를 삭제로 변경
                    ctx.SaveChanges(); // commit
                }
            }

            Commons.IsDelete = true;

            // 조회쿼리 다시불러오기
            BtnViewWatchList_Click(sender, e);
            
        }

        private void BtnWatchTrailer_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("유튜브 영화", "영화를 선택하세요");
                return;
            }

            if (GrdData.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("유튜브 영화", "영화를 하나만 선택하세요");
                return;
            }

            string movieName = "";
            if (Commons.IsFavorite) // 즐겨찾기
            {
                var item = GrdData.SelectedItem as NaverFavoriteMovies;
                // MessageBox.Show(item.Link);
                movieName = item.Title;
            }
            else // 네이버API
            {
                var item = GrdData.SelectedItem as MovieItem;
                // MessageBox.Show(item.Link);
                movieName = item.Title;
            }

            var trailerWindow = new TrailerWindow(movieName);
            trailerWindow.Owner = this;
            trailerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            trailerWindow.ShowDialog();

        }

        private void BtnNaverMovieLink_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("네이버 영화", "영화를 선택하세요");
                return;
            }

            if (GrdData.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("네이버 영화", "영화를 하나만 선택하세요");
                return;
            }

            // 선택된 네이버영화 URL 가져오기
            string linkUrl = "";
            if (Commons.IsFavorite) // 즐겨찾기
            {
                var item = GrdData.SelectedItem as NaverFavoriteMovies;
                // MessageBox.Show(item.Link);
                linkUrl = item.Link;
            }
            else // 네이버API
            {
                var item = GrdData.SelectedItem as MovieItem;
                // MessageBox.Show(item.Link);
                linkUrl = item.Link;
            }

            Process.Start(linkUrl); // 웹브라우저 띄우기
            
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"즐겨찾기 여부는 {Commons.IsFavorite}");
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
