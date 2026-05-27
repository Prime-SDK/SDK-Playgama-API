using PrimeGames.SDK.Common;
using Playgama;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPlatformInfo))]
    public class PlaygamaPlatformInfo : CommonPlatformInfo {

        protected override string GetAppIdImpl() {
            Logger.NotImplementedWarning(this, nameof(GetAppIdImpl));
            return string.Empty;
        }

        protected override PlatformType GetCurrentImpl() {
            return Bridge.platform.id switch {
                "playgama" => PlatformType.Playgama,
                "vk" => PlatformType.VK,
                "ok" => PlatformType.OK,
                "yandex" => PlatformType.YandexGames,
                "facebook" => PlatformType.Facebook,
                "crazy_games" => PlatformType.CrazyGames,
                "game_distribution" => PlatformType.GameDistribution,
                "playdeck" => PlatformType.PlayDeck,
                "telegram" => PlatformType.Telegram,
                "y8" => PlatformType.Y8,
                "lagged" => PlatformType.Lagged,
                "msn" => PlatformType.MSN,
                "poki" => PlatformType.Poki,
                "qa_tool" => PlatformType.QATool,
                "discord" => PlatformType.Discord,
                "gamepush" => PlatformType.GamePush,
                "mock" => PlatformType.Unknown,
                _ => PlatformType.Unknown
            };
        }

        protected override DeploymentType GetDeploymentImpl() {
            return DeploymentType.Web;
        }

    }

}