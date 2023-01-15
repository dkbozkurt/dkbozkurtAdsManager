// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using UnityEngine;
using GoogleMobileAds.Api;
using DkbozkurtAdsManager.Scripts.Helpers;

namespace DkbozkurtAdsManager.Scripts.Managers
{
    /// <summary>
    /// In this script id info is just for test you have to change id's for your game by learning
    /// ids from the admob website.
    ///
    /// https://admob.google.com/intl/tr/home/
    /// 
    /// Ref : https://www.youtube.com/watch?v=KKL3k-Uk5JU
    /// Ref : https://developers.google.com/admob/unity/app-open
    /// </summary>
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
            // CallBanner();

            // CallInterstitial();

            CallRewarded();
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

        #region Interstitial
        
        private InterstitialAd _interstitialAd;

        public void CallInterstitial()
        {
            MobileAds.Initialize(initStatus => { });
            
            RequestInterstitial();

            if (_interstitialAd.IsLoaded())
                _interstitialAd.Show();
        }
        
        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            this._interstitialAd = new InterstitialAd(adUnitId);
        }

        #endregion

        #region Rewarded

        private RewardedAd _rewardedAd;

        public void CallRewarded()
        {
            MobileAds.Initialize(initialize => { });
            
            RequestRewardedVideo();
            
            if(_rewardedAd.IsLoaded())
                _rewardedAd.Show();
        }
        
        private void RequestRewardedVideo()
        {
            string adUnitId;
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

            this._rewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            this._rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this._rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this._rewardedAd.LoadAd(request);
        }
        
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            // string type = args.Type;
            // double amount = args.Amount;
            // MonoBehaviour.print(
            //     "HandleRewardedAdRewarded event received for "
            //     + amount.ToString() + " " + type);
            
            Debug.Log("You earned the price.");
        }

        #endregion

    }
}
