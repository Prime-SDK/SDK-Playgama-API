using PrimeGames.SDK.Common;
using Playgama;
using Playgama.Modules.Platform;
using Logger = PrimeGames.SDK.Common.Logger;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IGameplayReporter))]
    public class PlaygamaGameplayReporter : CommonGameplayReporter {

        public PlaygamaGameplayReporter() {
            SetInitialized();
        }

        protected override void GameIsReadyImpl() {
            Bridge.platform.SendMessage(PlatformMessage.GameReady);
        }

        protected override void GameplayRestartImpl(int level = 0) {
            Logger.NotAvailableWarning(this, nameof(GameplayRestartImpl));
        }

        protected override void GameplayStartImpl(int level = 0) {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStarted);
        }

        protected override void GameplayStopImpl(int level = 0) {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStopped);
        }

    }

}