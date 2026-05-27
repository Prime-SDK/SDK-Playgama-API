using PrimeGames.SDK.Common;
using Playgama;
using System;
using System.Collections.Generic;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IFlags))]
    public class PlaygamaFlags : IFlags {

        private readonly Dictionary<string, string> collection = new();
        private readonly Stack<Action> waiters = new();

        public PlaygamaFlags(IEventDispatcher dispatcher) {
            dispatcher.Start += OnStart;
        }

        public bool IsFlagsInitialized { get; private set; } = false;
        public bool IsFlagsAvailable { get; private set; } = false;

        public void WaitForFlags(Action onInitialized) {
            if (!IsFlagsInitialized) {
                waiters.Push(onInitialized);
                return;
            }
            onInitialized?.Invoke();
        }

        private void OnStart() {
            try {
                if (Bridge.platform.id != "yandex") {
                    IsFlagsAvailable = false;
                    IsFlagsInitialized = true;
                    waiters.PopInvokeAll();
                    return;
                }
                Dictionary<string, object> options = new();
                Bridge.remoteConfig.Get(options, (isSuccess, config) => {
                    collection.Clear();
                    foreach (KeyValuePair<string, string> pair in config) {
                        collection.Add(pair.Key, pair.Value);
                    }
                    IsFlagsAvailable = true;
                    IsFlagsInitialized = true;
                    waiters.PopInvokeAll();
                });
            }
            catch (Exception exception) {
                Logger.CreateError(this, exception);
                IsFlagsAvailable = false;
                IsFlagsInitialized = true;
                waiters.PopInvokeAll();
            }
        }

        public bool GetBool(string key, bool defaultValue = false) {
            return collection.TryGetValue(key, out string value) ? bool.Parse(value) : defaultValue;
        }

        public float GetFloat(string key, float defaultValue = 0) {
            return collection.TryGetValue(key, out string value) ? float.Parse(value) : defaultValue;
        }

        public int GetInt(string key, int defaultValue = 0) {
            return collection.TryGetValue(key, out string value) ? int.Parse(value) : defaultValue;
        }

        public string GetString(string key, string defaultValue = "") {
            return collection.TryGetValue(key, out string value) ? value : defaultValue;
        }

        public bool HasKey(string key) {
            return collection.ContainsKey(key);
        }

    }

}