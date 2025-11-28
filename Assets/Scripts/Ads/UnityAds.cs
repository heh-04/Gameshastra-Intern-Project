using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public static UnityAds instance;

    public string gameId = " ";

    public string interstitialId = " ";
    public string rewardedId = " ";
    public string bannerId = " ";

    private bool testMode = true;

    public event Action OnAdClosed;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads Initialization Failed");
    }


    public void LoadInterstitialAd()
    {
        Advertisement.Load(interstitialId, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(rewardedId, this);
    }

    public void LoadBannerAd()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Show(bannerId);
        }

        else
        {
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

            BannerLoadOptions loadOptions = new BannerLoadOptions
            {
                loadCallback = () => Advertisement.Banner.Show(bannerId),
                errorCallback = (message) => Debug.Log("Banner Load Failed")
            };

            Advertisement.Banner.Load(bannerId, loadOptions);
        }
    }

    public void ClearBanner()
    {
        Advertisement.Banner.Hide();
    }



    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        OnAdClosed?.Invoke();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) 
    {
        OnAdClosed ?.Invoke();
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
