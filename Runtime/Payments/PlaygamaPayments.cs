using PrimeGames.SDK.Common;
using Playgama;
using System;
using System.Collections.Generic;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IPayments))]
    public class PlaygamaPayments : CommonPayments {

        private ProductData[] products = Array.Empty<ProductData>();
        private string[] purchases = Array.Empty<string>();

        private bool isProductsReady = false;
        private bool isPurchasesReady = false;

        public PlaygamaPayments(IData data, IEventDispatcher dispatcher) : base(data) {
            if (IsPaymentsAvailable == false) {
                SetInitialized();
                return;
            }
            dispatcher.Start += OnStart;
        }

        private void OnStart() {
            Bridge.payments.GetCatalog((isSuccess, catalog) => {
                isProductsReady = true;
                if (isSuccess == true) {
                    List<ProductData> products = new();
                    foreach (Dictionary<string, string> item in catalog) {
                        string productTag = item["id"];
                        string priceValue = item["priceValue"];
                        string priceCurrency = item["priceCurrencyCode"];
                        Logger.CreateText(this, $"Product ({productTag}, {priceValue}, {priceCurrency})");
                        float priceFloat = float.Parse(priceValue);
                        ProductData product = new(productTag, priceFloat, priceCurrency);
                        products.Add(product);
                    }
                    this.products = products.ToArray();
                }
                CheckForReady();
            });
            Bridge.payments.GetPurchases((isSuccess, inventory) => {
                isPurchasesReady = true;
                if (isSuccess == true) {
                    List<string> purchases = new();
                    foreach (Dictionary<string, string> purchase in inventory) {
                        purchases.Add(purchase["id"]);
                    }
                    this.purchases = purchases.ToArray();
                }
                CheckForReady();
            });
        }

        private void CheckForReady() {
            if (isProductsReady && isPurchasesReady) {
                SetInitialized();
            }
        }

        public override bool IsPaymentsAvailable => Bridge.payments.isSupported;

        protected override ProductData GetProductDataImpl(string productTag) {
            return Array.Find(products, p => p.Tag == productTag);
        }

        protected override bool IsAlreadyPurchasedImpl(string productTag) {
            return Array.Exists(purchases, p => p == productTag);
        }

        protected override void PurchaseImpl(string productTag, Action onSuccess, Action onError = null) {
            Bridge.payments.Purchase(productTag, (isSuccess, purchase) => {
                if (isSuccess) {
                    onSuccess?.Invoke();
                }
                else {
                    onError?.Invoke();
                }
            });
        }

        protected override void RestorePurchasesImpl(Action<IRestoreData> onRestoreData) {
            Bridge.payments.GetPurchases((isSuccess, inventory) => {
                if (isSuccess == true) {
                    List<string> purchases = new();
                    foreach (Dictionary<string, string> purchase in inventory) {
                        purchases.Add(purchase["id"]);
                    }
                    this.purchases = purchases.ToArray();

                    RestoreData restoreData = new(this, purchases.ToArray());
                    onRestoreData?.Invoke(restoreData);
                }
            });
        }

    }

}