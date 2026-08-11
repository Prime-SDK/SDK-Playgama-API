using PrimeGames.SDK.Common;
using Playgama;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPause))]
    public class PlaygamaPause : CommonPause {

        public PlaygamaPause(IEventAggregator aggregator, IEventDispatcher dispatcher) : base(aggregator) {
            dispatcher.OnApplicationPause += OnApplicationPause;
            dispatcher.OnApplicationFocus += OnApplicationFocus;
            dispatcher.Start += () => {
                Bridge.platform.pauseStateChanged += OnPauseStateChanged;
            };
        }

        private void OnPauseStateChanged(bool isPaused) {
            Logger.CreateText(this, nameof(OnPauseStateChanged), isPaused.ToString());
            Register(nameof(OnPauseStateChanged), isPaused);
        }

        public void OnApplicationFocus(bool focusStatus) {
            Register(nameof(OnApplicationFocus), !focusStatus);
        }

        public void OnApplicationPause(bool pauseStatus) {
            Register(nameof(OnApplicationPause), pauseStatus);
        }

    }

}
