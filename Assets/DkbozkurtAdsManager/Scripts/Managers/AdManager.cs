
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using DkbozkurtAdsManager.Scripts.Helpers;

namespace DkbozkurtAdsManager.Scripts.Managers
{
    public class AdManager : SingletonBehaviour<AdManager>
    {
        protected override void OnAwake() { }
        
        #region IDS

        public string appID = "";
        /// <summary>
        /// Interstitial ad is the type of advertisement used in in-game round transitions.
        /// </summary>
        public string InterstitialID = "ca-app-pub-3940256099942544/1033173712";
        
        /// <summary>
        /// Banner is the ad type that shown to customer all the time on the screen's specified position.
        /// </summary>
        public string BannedID = "ca-app-pub-3940256099942544/6300978111";
        
        #endregion
        
        private void Start()
        {
            CallBanner();

        }

        #region Banner

        [Header("BANNER")]
        [SerializeField] private AdPosition _bannerPosition = AdPosition.Bottom;
        
        private BannerView _bannerView;

        public void CallBanner()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });
            
            this.RequestBanner();
        }

        private void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNIY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
            
            // Create a 320x50 banner at the top of the screen.
            this._bannerView = new BannerView(adUnitId, AdSize.Banner, _bannerPosition);
            // Ad szie can be changeable by assigning Ad size like the following.
            // AdSize adSize = new AdSize(250, 250);
            
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            
            // Load the banner with the request.
            this._bannerView.LoadAd(request);

        }
        #endregion
        
    }
}
