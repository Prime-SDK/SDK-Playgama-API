using PrimeGames.SDK.Common;
using Playgama;
using Playgama.Modules.Advertisement;
using System;
using Logger = PrimeGames.SDK.Common.Logger;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IAds))]
    public class PlaygamaAds : CommonAds {

        public PlaygamaAds(IEventAggregator eventAggregator) : base(eventAggregator) {
            Bridge.advertisement.bannerStateChanged += OnBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
            SetInitialized();
        }

        private void OnBannerStateChanged(BannerState state) {
            switch (state) {
                case BannerState.Shown: {
                    Logger.CreateText(this, "Banner shown");
                    IsBannerVisible = true;
                    break;
                }
                case BannerState.Hidden: {
                    Logger.CreateText(this, "Banner hidden");
                    IsBannerVisible = false;
                    break;
                }
            }
        }

        private void OnInterstitialStateChanged(InterstitialState state) {
            switch (state) {
                case InterstitialState.Opened: {
                    Logger.CreateText(this, "Interstitial opened");
                    IsInterstitialVisible = true;
                    onInterstitialOpen?.Invoke();
                    break;
                }
                case InterstitialState.Closed: {
                    Logger.CreateText(this, "Interstitial closed");
                    IsInterstitialVisible = false;
                    onInterstitialClose?.Invoke(true);
                    break;
                }
                case InterstitialState.Failed: {
                    Logger.CreateText(this, "Interstitial failed");
                    IsInterstitialVisible = false;
                    onInterstitialClose?.Invoke(false);
                    break;
                }
            }
        }

        private void OnRewardedStateChanged(RewardedState state) {
            switch (state) {
                case RewardedState.Opened: {
                    Logger.CreateText(this, "Rewarded opened");
                    IsRewardedVisible = true;
                    onRewardedOpen?.Invoke();
                    break;
                }
                case RewardedState.Rewarded: {
                    Logger.CreateText(this, "Rewarded success");
                    isPlayerRewarded = true;
                    break;
                }
                case RewardedState.Closed: {
                    Logger.CreateText(this, "Rewarded closed");
                    IsRewardedVisible = false;
                    if (isPlayerRewarded) {
                        isPlayerRewarded = false;
                        onRewardedClose?.Invoke(true);
                    }
                    else {
                        onRewardedClose?.Invoke(false);
                    }
                    break;
                }
                case RewardedState.Failed: {
                    Logger.CreateText(this, "Rewarded failed");
                    IsRewardedVisible = false;
                    onRewardedClose?.Invoke(false);
                    break;
                }
            }
        }

        private static Action onInterstitialOpen;
        private static Action<bool> onInterstitialClose;

        private static bool isPlayerRewarded;
        private static Action onRewardedOpen;
        private static Action<bool> onRewardedClose;

        public override bool IsBannerReady => true;
        public override bool IsBannerVisible { get; protected set; } = false;
        public override bool IsBannerAvailable => Bridge.advertisement.isBannerSupported;

        protected override void InvokeBannerImpl() {
            Bridge.advertisement.ShowBanner();
        }

        protected override void RefreshBannerImpl() { }

        protected override void DisableBannerImpl() {
            Bridge.advertisement.HideBanner();
        }

        public override bool IsInterstitialReady => true;
        public override bool IsInterstitialVisible { get; protected set; } = false;
        public override bool IsInterstitialAvailable => Bridge.advertisement.isInterstitialSupported;

        protected override void InvokeInterstitialImpl(InterstitialParameters parameters, Action onOpen, Action<bool> onClose) {
            onInterstitialOpen = onOpen;
            onInterstitialClose = onClose;
            Bridge.advertisement.ShowInterstitial();
        }

        public override bool IsRewardedReady => true;
        public override bool IsRewardedVisible { get; protected set; } = false;
        public override bool IsRewardedAvailable => Bridge.advertisement.isRewardedSupported;

        protected override void InvokeRewardedImpl(RewardedParameters parameters, Action onOpen, Action<bool> onClose) {
            onRewardedOpen = onOpen;
            onRewardedClose = onClose;
            Bridge.advertisement.ShowRewarded();
        }

    }

}