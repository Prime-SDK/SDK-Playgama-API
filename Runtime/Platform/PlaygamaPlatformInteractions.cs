using PrimeGames.SDK.Common;
using Playgama;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPlatformInteractions))]
    public class PlaygamaPlatformInteractions : CommonPlatformInteractions {

        protected override void RateGameImpl() {
            if (Bridge.social.isRateSupported) {
                Bridge.social.Rate((isSuccess) => {
                    if (isSuccess) {
                        Logger.CreateText("rate game success");
                    }
                    else {
                        Logger.CreateText("rate game error");
                    }
                });
            }
        }

        protected override void ShareGameImpl(string messageText) {
            Bridge.social.Share(messageText, (isSuccess) => {
                if (isSuccess) {
                    Logger.CreateText("share game success");
                }
                else {
                    Logger.CreateText("share game error");
                }
            });
        }

    }

}
