using AudienceNetwork;
using UnityEngine;
using System;

public class MetaAds : MonoBehaviour
{
    public static MetaAds instance;

    public string interstitialId = " ";
    public string rewardedId = " ";
    public string bannerId = " ";

    public event Action OnAdClosed;

    private AdView adView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AudienceNetworkAds.Initialize();
    }

    public void LoadInterstitialAd()
    {
        InterstitialAd interstitialAd = new InterstitialAd(interstitialId);
        interstitialAd.Register(gameObject);

        interstitialAd.InterstitialAdDidLoad = delegate ()
        {
            interstitialAd.Show();
        };

        interstitialAd.InterstitialAdDidFailWithError = delegate (string error) { };

        interstitialAd.InterstitialAdDidClick = delegate () { };

        interstitialAd.interstitialAdDidClose = delegate ()
        {
            OnAdClosed?.Invoke();
            interstitialAd?.Dispose();
        };

        interstitialAd.LoadAd();
    }

    public void LoadRewardedAd()
    {
        //RewardData rewardData = new RewardData()
        //{
        //    UserId = "USER_ID",
        //    Currency = "REWARD_ID"
        //};

        RewardedVideoAd rewardedAd = new RewardedVideoAd(rewardedId);
        rewardedAd.Register(gameObject);

        rewardedAd.RewardedVideoAdDidLoad = delegate ()
        {
            rewardedAd.Show();
        };

        rewardedAd.RewardedVideoAdDidFailWithError = delegate (string error) { };

        rewardedAd.RewardedVideoAdDidClick = delegate () { };

        rewardedAd.RewardedVideoAdDidClick = delegate ()
        {
            rewardedAd?.Dispose();
        };

        rewardedAd.LoadAd();

        rewardedAd.RewardedVideoAdDidSucceed = delegate () { };

        rewardedAd.RewardedVideoAdDidFail = delegate () { };
    }

    public void LoadBannerAd()
    {
        CloseBanner();

        adView = new AdView(bannerId, AdSize.BANNER_HEIGHT_50);
        adView.Register(gameObject);

        adView.AdViewDidLoad = delegate ()
        {
            adView.Show(100);
        };

        adView.AdViewDidFailWithError = delegate (string error) { };

        adView.AdViewWillLogImpression = delegate () { };

        adView.AdViewDidClick = delegate () { };

        adView.LoadAd();
    }

    public void CloseBanner()
    {
        if(adView)
        {
            adView.Dispose();
        }
    }
}

