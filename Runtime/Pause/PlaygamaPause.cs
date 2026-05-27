using PrimeGames.SDK.Common;
using Playgama;
using Playgama.Modules.Game;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPause))]
    public class PlaygamaPause : CommonPause {

        public PlaygamaPause(IEventAggregator aggregator, IEventDispatcher dispatcher) : base(aggregator) {
            dispatcher.OnApplicationPause += OnApplicationPause;
            dispatcher.OnApplicationFocus += OnApplicationFocus;
            dispatcher.Start += () => {
                Bridge.game.visibilityStateChanged += OnVisibilityStateChanged;
                OnVisibilityStateChanged(Bridge.game.visibilityState);
            };
        }

        private void OnVisibilityStateChanged(VisibilityState state) {
            Logger.CreateText(this, nameof(OnVisibilityStateChanged), state.ToString());
            Register(nameof(OnVisibilityStateChanged), state == VisibilityState.Hidden);
        }

        public void OnApplicationFocus(bool focusStatus) {
            Register(nameof(OnApplicationFocus), !focusStatus);
        }

        public void OnApplicationPause(bool pauseStatus) {
            Register(nameof(OnApplicationPause), pauseStatus);
        }

    }

}