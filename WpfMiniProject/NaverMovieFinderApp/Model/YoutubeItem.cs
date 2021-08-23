using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NaverMovieFinderApp.Model
{   // 프로퍼티
    public class YoutubeItem    // 접근한정자 데이터타입 필드명
    {   
        public string Title { get; set; }   // 구현, 코드를 숨기는 동시에 값을 가져오고 설정하는 방법을 공개적으로 노출
        public string Author { get; set; }  // get 접근자 : 속성 값을 반환(읽기전용),  set 접근자 : 새 값을 할당(쓰기전용)
        public string URL { get; set; }
        public BitmapImage Thumbnail { get; set; }
    }
}
