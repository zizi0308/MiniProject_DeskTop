/// <summary>
/// EntityFramework로 만들어진 프로퍼티 활용
/// 구현, 코드를 숨기는 동시에 값을 가져오고 설정하는 방법을 공개적으로 노출
/// get 접근자 : 속성 값을 반환(읽기전용),  set 접근자 : 새 값을 할당(쓰기전용)
/// </summary>

namespace NaverMovieFinderApp.Model
{
    public class MovieItem
    {
        public string Title { get; set; } 
        public string Link { get; set; } 
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string PubDate { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public string UserRating { get; set; }

        public MovieItem(string title, string link, string image, string subTitle, 
            string pubDate, string director, string actor, string userRating)
        {
            Title = title;
            Link = link;
            Image = image;
            SubTitle = subTitle;
            PubDate = pubDate;
            Director = director;
            Actor = actor;
            UserRating = userRating;
        }
    }
}
