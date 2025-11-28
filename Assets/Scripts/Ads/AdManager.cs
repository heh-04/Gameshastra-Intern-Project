using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    public AdService adService;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        GoogleAds.instance.OnAdClosed += OnAdClosed;
        UnityAds.instance.OnAdClosed += OnAdClosed;
        MetaAds.instance.OnAdClosed += OnAdClosed;

        PlayerEvents.OnPlayerDeath += OnGameEnd;
        PlayerEvents.OnPlayerFinish += OnGameEnd;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowAd(AdType adType)
    {
        switch (adType)
        {
            case AdType.InterstitialAd:
                {
                    switch (adService)
                    {
                        case AdService.Google:
                            {
                                GoogleAds.instance.LoadInterstitialAd();
                                return;
                            }

                        case AdService.Unity:
                            {
                                UnityAds.instance.LoadInterstitialAd();
                                return;
                            }

                        case AdService.Meta:
                            {
                                MetaAds.instance.LoadInterstitialAd();
                                return;
                            }
                    }
                    return;
                }

            case AdType.RewardedAd:
                {
                    switch (adService)
                    {
                        case AdService.Google:
                            {
                                GoogleAds.instance.LoadRewardedAd();
                                return;
                            }

                        case AdService.Unity:
                            {
                                UnityAds.instance.LoadRewardedAd();
                                return;
                            }

                        case AdService.Meta:
                            {
                                MetaAds.instance.LoadRewardedAd();
                                return;
                            }
                    }
                    return;
                }

            case AdType.BannerAd:
                {
                    switch (adService)
                    {
                        case AdService.Google:
                            {
                                GoogleAds.instance.LoadBannerAd();
                                return;
                            }

                        case AdService.Unity:
                            {
                                UnityAds.instance.LoadBannerAd();
                                return;
                            }

                        case AdService.Meta:
                            {
                                MetaAds.instance.LoadBannerAd();
                                return;
                            }
                    }
                    return;
                }
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        UnityAds.instance.ClearBanner();
        MetaAds.instance.CloseBanner();

        if (scene == SceneManager.GetSceneByName("MainMenu"))
        {
            ShowAd(AdType.BannerAd);
        }

        else if (scene == SceneManager.GetSceneByName("LoadingScreen"))
        {
            ShowAd(AdType.InterstitialAd);
        }
    }

    public void OnAdClosed()
    {
        SceneLoader.instance.allowSceneSwitch = true;
    }

    public void OnGameEnd()
    {
        ShowAd(AdType.BannerAd);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GoogleAds.instance.OnAdClosed -= OnAdClosed;
        UnityAds.instance.OnAdClosed -= OnAdClosed;
        MetaAds.instance.OnAdClosed -= OnAdClosed;

        PlayerEvents.OnPlayerDeath -= OnGameEnd;
        PlayerEvents.OnPlayerFinish -= OnGameEnd;
    }
}

public enum AdType
{
    InterstitialAd,
    RewardedAd,
    BannerAd
}

public enum AdService
{
    Unity,
    Google,
    Meta
}
