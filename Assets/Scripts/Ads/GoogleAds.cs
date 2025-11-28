using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class GoogleAds : MonoBehaviour
{
    public static GoogleAds instance;

    public string interstitialId = " ";
    public string rewardedId = " ";
    public string bannerId = " ";

    public event Action OnAdClosed;

    private BannerView _bannerView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("Google Ads Initialized");
        });
    }

    public void LoadInterstitialAd()
    {
        var adRequest = new AdRequest();
        InterstitialAd.Load(interstitialId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log("Ad failed to load");
                return;
            }

            if (ad != null && ad.CanShowAd())
            {
                ad.Show();
            }

            ad.OnAdFullScreenContentClosed += () =>
            {
                OnAdClosed?.Invoke();
                DestroyAd(ad);
            };
        });
    }

    public void LoadRewardedAd()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load(rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log("Ad failed to load");
                return;
            }

            if (ad != null && ad.CanShowAd())
            {
                ad.Show((Reward reward) =>
                {
                    Debug.Log("Reward Earned");
                });
            }

            ad.OnAdFullScreenContentClosed -= () =>
            {
                OnAdClosed?.Invoke();
                DestroyAd(ad);
            };
        });
    }

    public void LoadBannerAd()
    {
        BannerView bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
        _bannerView = bannerView;
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }

    public void DestroyAd(RewardedAd ad)
    {
        if (ad != null)
        {
            ad.Destroy();
            ad = null;
        }
    }

    public void DestroyAd(InterstitialAd ad)
    {
        if (ad != null)
        {
            ad.Destroy();
            ad = null;
        }
    }

    public void DestroyBanner(BannerView bannerView)
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }
}
