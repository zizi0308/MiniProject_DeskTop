using NLog;
using WpfSMSApp.Model;

namespace WpfSMSApp
{
    public class Commons
    {
        // Nlog 정적 인스턴스 생성
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        // 로그인한 유저 정보 인스턴스
        public static User LOGINSD_USER;
    }
}
