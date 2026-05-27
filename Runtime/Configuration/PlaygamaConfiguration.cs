using PrimeGames.SDK.Common;

namespace PrimeGames.SDK.Playgama {

    [Configuration]
    public class PlaygamaConfiguration : Configuration {

        public override string Name { get; } = nameof(PlaygamaConfiguration);
        public override string Description { get; } = "Using PlaygamaBridge SDK API";
        public override string IconName { get; } = "Playgama";
        public override bool ReadOnly { get; } = false;

        public override string AchievementsProviderName { get; } = nameof(PlaygamaAchievements);
        public override string AdsProviderName { get; } = nameof(PlaygamaAds);
        public override string EventsReporterProviderName { get; } = "FallbackEventsReporter";
        public override string GameplayReporterProviderName { get; } = nameof(PlaygamaGameplayReporter);
        public override string AddressablesProviderName { get; } = "UnityEngineAddressables";
        public override string AssetBundlesProviderName { get; } = "UnityEngineAssetBundles";
        public override string StreamingAssetsProviderName { get; } = "UnityEngineStreamingAssets";
        public override string AudioProviderName { get; } = nameof(PlaygamaAudio);
        public override string BootstrapProviderName { get; } = "FallbackBootstrap";
        public override string DataProviderName { get; } = nameof(PlaygamaData);
        public override string DeviceBrowserProviderName { get; } = "UnityEngineDeviceBrowser";
        public override string DeviceCursorProviderName { get; } = "UnityEngineDeviceCursor";
        public override string DeviceInfoProviderName { get; } = nameof(PlaygamaDeviceInfo);
        public override string FlagsProviderName { get; } = nameof(PlaygamaFlags);
        public override string LanguageInfoProviderName { get; } = nameof(PlaygamaLanguageInfo);
        public override string PauseProviderName { get; } = nameof(PlaygamaPause);
        public override string PaymentsProviderName { get; } = nameof(PlaygamaPayments);
        public override string PlatformInfoProviderName { get; } = nameof(PlaygamaPlatformInfo);
        public override string PlatformInteractionsProviderName { get; } = nameof(PlaygamaPlatformInteractions);
        public override string PlayerAccountProviderName { get; } = nameof(PlaygamaPlayerAccount);
        public override string DateTimeProviderName { get; } = "SystemDateTime";
        public override string TimeScaleProviderName { get; } = "UnityEngineTimeScale";

    }

}