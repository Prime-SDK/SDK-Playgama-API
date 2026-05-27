using PrimeGames.SDK.Common;
using Playgama;
using System.Collections.Generic;

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
            Dictionary<string, object> options = new();
            switch (Bridge.platform.id) {
                case "vk": {
                    options.Add("link", "");
                    break;
                }
                case "facebook": {
                    options.Add("image", "");
                    options.Add("text", "");
                    break;
                }
                case "msn": {
                    options.Add("title", "");
                    options.Add("image", "");
                    options.Add("text", "");
                    break;
                }
            }
            Bridge.social.Share(options, (isSuccess) => {
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