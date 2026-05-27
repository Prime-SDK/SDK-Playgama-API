using PrimeGames.SDK.Common;
using Playgama;
using System;
using System.Collections.Generic;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPlayerAccount))]
    public class PlaygamaPlayerAccount : CommonPlayerAccount {

        public PlaygamaPlayerAccount() : base() {
            SetInitialized();
        }

        protected override string GetDisplayNameImpl() {
            return Bridge.player.name;
        }

        protected override string GetFirstNameImpl() {
            return string.Empty;
        }

        protected override string GetLastNameImpl() {
            return string.Empty;
        }

        protected override string GetUniqueIdImpl() {
            return Bridge.player.id;
        }

        protected override string GetUsernameImpl() {
            return string.Empty;
        }

        protected override void InvokeLoginImpl(Action onLoginSuccess = null, Action onLoginError = null) {
            Dictionary<string, object> options = new();
            switch (Bridge.platform.id) {
                case "yandex": {
                    options.Add("scopes", true);
                    break;
                }
            }
            Bridge.player.Authorize(options, (isSuccess) => {
                if (isSuccess) {
                    onLoginSuccess?.Invoke();
                }
                else {
                    onLoginError?.Invoke();
                }
            });
        }

        protected override bool IsLoggedInImpl() {
            return Bridge.player.isAuthorized;
        }

    }

}