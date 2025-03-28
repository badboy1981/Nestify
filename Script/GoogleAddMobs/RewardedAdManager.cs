using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedAdManager : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private InterstitialAd _interstitialAd;
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized");
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        // اگر تبلیغ از قبل بارگذاری شده، نیازی به بارگذاری مجدد نیست
        if (_rewardedAd != null) return;

        // ساخت درخواست تبلیغ
        var request = new AdRequest();

        // بارگذاری تبلیغ پاداش‌دهنده
        RewardedAd.Load(_adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error?.GetMessage());
                return;
            }

            Debug.Log("Rewarded ad loaded successfully");
            _rewardedAd = ad;

            // تنظیم گزینه‌های تأیید سرور
            var options = new ServerSideVerificationOptions
            {
                UserId = "USER_ID_HERE", // اختیاری - می‌توانید null بگذارید
                CustomData = "SAMPLE_CUSTOM_DATA_STRING"
            };

            ad.SetServerSideVerificationOptions(options);

            // ثبت رویدادها
            RegisterEventHandlers(ad);
        });
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad opened");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad closed");
            _rewardedAd = null;
            LoadRewardedAd(); // بارگذاری مجدد
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed: " + error.GetMessage());
            _rewardedAd = null;
            LoadRewardedAd(); // تلاش مجدد
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad clicked");
        };
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"Reward received: {reward.Amount} {reward.Type} تبلیغ مشاهده شد");
            });
        }
        else
        {
            Debug.LogWarning("Ad not ready yet.");
            LoadRewardedAd();
        }
    }

    void OnDestroy()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
        }
    }
}