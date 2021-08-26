using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NaverMovieFinderApp
{
    public class Commons
    {
        // 즐겨찾기 여부 플래그
        public static bool IsFavorite = false;

        public static bool IsDelete = false; // 즐겨찾기 삭제와 보기 플래그

        // Nlog 정적객체
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static async Task<MessageDialogResult> ShowMessageAsync(
           string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(title, message, style, null);
        }

        public static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
        {
            string result = "";

            try
            {
                WebRequest request = WebRequest.Create(openApiUrl);     // 사용하고자 하는 URL 요청
                request.Headers.Add("X-Naver-Client-Id", clientID);     // 네이버 API를 쓰기 위해 무조건 넣어줘야 함
                request.Headers.Add("X-Naver-Client-Secret", clientSecret);

                WebResponse response = request.GetResponse();           // 요청을 웹서버에 보내고 응답을 받아옴(리턴된 WebResponse는 여러 속성을 가지고 있음)
                Stream stream = response.GetResponseStream();           // 데이터 내용은 GetResponseStream() 메서드로 부터 얻어낸 스트림을 
                StreamReader reader = new StreamReader(stream);         // 읽어 가져옴

                result = reader.ReadToEnd();    // 데이터 내용을 끝까지 읽어 가져오고

                reader.Close();     // 리더 닫고
                stream.Close();     // 스트림 닫고
                response.Close();   // 전부 닫아줌
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외발생 : {ex}");
            }

            return result;
        }

        public static string StripHtmlTag(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", ""); // Html 태그를 삭제하는 정규표현식
        }

        public static string StripPipe(string text)
        {
            if (string.IsNullOrEmpty(text)) 
                return "";
            else
                return text.Substring(0, text.LastIndexOf("|")).Replace("|", ", ");
        }
    }
}
