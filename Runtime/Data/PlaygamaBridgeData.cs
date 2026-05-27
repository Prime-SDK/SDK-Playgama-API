using PrimeGames.SDK.Common;
using Playgama;
using System;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IData))]
    public class PlaygamaData : CommonData {

        private const string dataKey = "json-data";

        public PlaygamaData(IEventDispatcher dispatcher) : base(dispatcher) {
            dispatcher.Start += OnStart;
        }

        private void OnStart() {
            ReadJson((json) => {
                Logger.CreateText(this, "ReadJson", Naming.Quote(json));
                ParseContainers(json);
            });
        }

        protected override void ReadJson(Action<string> jsonRequest) {
            Bridge.storage.Get(dataKey, (isSuccess, json) => {
                if (isSuccess) {
                    Logger.CreateText(this, "Success", Naming.Quote(json));
                    jsonRequest?.Invoke(json);
                }
                else {
                    Logger.CreateError(this, "Failed to get data - returning empty json");
                    jsonRequest?.Invoke(Naming.EmptyJson);
                }
            });
        }

        protected override void WriteJson(string json) {
            Bridge.storage.Set(dataKey, json, (isSuccess) => {
                if (isSuccess) {
                    Logger.CreateText(this, "Success", Naming.Quote(json));
                }
                else {
                    Logger.CreateError(this, "Failed to write data");
                }
            });
        }

    }

}