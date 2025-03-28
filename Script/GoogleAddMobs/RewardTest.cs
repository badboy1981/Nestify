using GoogleMobileAds.Api;
using UnityEngine;
using TMPro;
using System;

public class RewardTest : MonoBehaviour
{
    [Header("Ad Settings")]
    [SerializeField] private string androidAdUnitId = "ca-app-pub-3940256099942544/5224354917";
    [SerializeField] private string iosAdUnitId = "ca-app-pub-3940256099942544/1712485313";

    [Header("Game Settings")]
    [SerializeField] private int rewardAmount = 1;
    [SerializeField] private TextMeshProUGUI livesText;

    private RewardedAd rewardedAd;
    private int playerLives = 3;

    void Start()
    {
        UpdateLivesUI();
        MobileAds.Initialize(initStatus => {
            Debug.Log("AdMob initialized");
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        // حذف تبلیغ قبلی
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        string adUnitId = Application.platform == RuntimePlatform.Android ?
            androidAdUnitId : iosAdUnitId;

        // روش صحیح در نسخه 10.0.0 - متد Load مستقیماً تبلیغ را تنظیم نمی‌کند
        RewardedAd.Load(adUnitId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error?.GetMessage());
                Invoke(nameof(LoadRewardedAd), 30f);
                return;
            }

            rewardedAd = ad;
            RegisterEventHandlers();
            Debug.Log("Rewarded ad loaded successfully");
        });
    }

    private void RegisterEventHandlers()
    {
        rewardedAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ad opened");
            Time.timeScale = 0f;
        };

        rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ad closed");
            Time.timeScale = 1f;
            LoadRewardedAd();
        };

        rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Ad failed to show: " + error.GetMessage());
            LoadRewardedAd();
        };
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                playerLives += rewardAmount;
                UpdateLivesUI();
                Debug.Log($"Reward received! Lives: {playerLives}");
            });
        }
        else
        {
            Debug.Log("Ad not ready");
            LoadRewardedAd();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {playerLives}";
    }

    void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
    }
}