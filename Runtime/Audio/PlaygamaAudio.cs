using PrimeGames.SDK.Common;
using Playgama;
using UnityEngine;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IAudio))]
    public class PlaygamaAudio : CommonAudio, IEventListener<PauseChangeEvent> {

        private bool audioEnabled = true;
        private float desiredVolume = 1f;
        private bool desiredPause = false;

        public PlaygamaAudio(IEventAggregator eventAggregator, IEventDispatcher dispatcher) : base(eventAggregator) {
            eventAggregator.Subscribe(this);
            dispatcher.Start += () => {
                Bridge.platform.audioStateChanged += OnAudioStateChanged;
            };
            desiredVolume = AudioListener.volume;
            desiredPause = AudioListener.pause;
            UpdateAudioState();
        }

        private void OnAudioStateChanged(bool isEnabled) {
            audioEnabled = isEnabled;
            UpdateAudioState();
        }

        private void UpdateAudioState() {
            if (audioEnabled) {
                AudioListener.volume = desiredVolume;
                AudioListener.pause = desiredPause;
            }
            else {
                AudioListener.volume = 0.0f;
                AudioListener.pause = true;
            }
        }

        protected override float GetVolumeImpl() {
            return audioEnabled ? desiredVolume : 0.0f;
        }

        protected override void SetVolumeImpl(float volume) {
            desiredVolume = Mathf.Clamp01(volume);
            UpdateAudioState();
        }

        protected override bool GetPauseImpl() {
            return audioEnabled ? desiredPause : true;
        }

        protected override void SetPauseImpl(bool pause) {
            desiredPause = pause;
            UpdateAudioState();
        }

    }

}