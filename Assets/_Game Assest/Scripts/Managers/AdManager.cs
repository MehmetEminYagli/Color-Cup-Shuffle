using System;
using System.Collections;
using UnityEngine;
//using GoogleMobileAds.Api;

public static class AdManager
{
    private static float _lastInterstitialRequestTime;

    public static void ShowInterstitial(string placement)
    {
        if (Time.time < _lastInterstitialRequestTime)
        {
            return;
        }

        // if (DataManager.IsTutorial || TutorialController.NextInterstitialTime > Time.realtimeSinceStartup)
        // {
        //     LogManager.Log($"Player In Tutorial {DataManager.IsTutorial} or Tutorial time " +
        //                    $"{TutorialController.NextInterstitialTime} not meet {Time.realtimeSinceStartup} ");
        //     return;
        // }

        placement += "_interstitial";

        //TODO: REQUEST AD HERE

        _lastInterstitialRequestTime = Time.time + 1;
    }

    public static void ShowRewarded(string placement, Action<bool> onComplete)
    {
        placement += "_rewarded";

#if UNITY_EDITOR
        onComplete?.Invoke(true);
        onComplete += (success) =>
        {
            if (success)
            {
                GameManager.Instance.uiOverlay.RewardPlayerForAdWatching();
            }
        };
#else
        //TODO: REQUEST AD HERE


#endif
    }

    public static void ActivateBanner()
    {
        //GameManager.Instance.StartCoroutine(BannerActivationCheck());
    }

    //private static BannerView _bannerView;

    //private static IEnumerator BannerActivationCheck()
    //{
    //    var checkInterval = new WaitForSeconds(.5f);
    //    string adUnitId = "ca-app-pub-6836607607325829~3968615925"; // AdMob'dan aldığınız banner reklam birim kimliği.

    //    // Banner reklamı oluştur
    //    _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

    //    // Banner reklam talebi oluştur ve yükle
    //    AdRequest request = new AdRequest();
    //    _bannerView.LoadAd(request);

    //    // Banner'ı göster
    //    _bannerView.Show();

    //    yield return checkInterval;
    //}
}