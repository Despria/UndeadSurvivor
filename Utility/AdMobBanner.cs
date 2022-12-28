using GoogleMobileAds.Api;
using System.Collections.Generic;
using UnityEngine;

// Sample App ID: ca-app-pub-3940256099942544~3347511713

[CreateAssetMenu(fileName = "AdMob Banner", menuName = "Scriptable Objects/AdMob/Banner", order = 0)]
public class AdMobBanner : SingletonScriptableObject<AdMobBanner>
{
    readonly string unitID = "실제 광고단위 ID";
    readonly string testUnitID = "ca-app-pub-3940256099942544/6300978111";
    readonly string testDeviceID = "테스트 기기용 ID";

    BannerView banner;
    public AdPosition position;

    public void InitAd()
    {
        string id = Debug.isDebugBuild ? testUnitID : unitID;

        int adWidth = MobileAds.Utils.GetDeviceSafeWidth();
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(adWidth);

        banner = new BannerView(id, adSize, position);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    public void ShowAd()
    {
        banner.Show();
    }

    public void HideAd()
    {
        banner.Hide();
    }

    public void SetTestDeviceID()
    {
        List<string> deviceID = new List<string>
        {
            testDeviceID
        };

        RequestConfiguration requestConfiguration = new RequestConfiguration
                                                        .Builder()
                                                        .SetTestDeviceIds(deviceID)
                                                        .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
    }

    public void SetTestDeviceID(string testDeviceID)
    {
        List<string> deviceID = new List<string>();
        deviceID.Add(testDeviceID);

        RequestConfiguration requestConfiguration = new RequestConfiguration
                                                        .Builder()
                                                        .SetTestDeviceIds(deviceID)
                                                        .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
    }

    public void SetTestDeviceIDs(List<string> testDeviceIDs)
    {
        List<string> deviceIDs = new List<string>();
        for (int i = 0; i < testDeviceIDs.Count; i++)
        {
            deviceIDs.Add(testDeviceIDs[i]);
        }

        RequestConfiguration requestConfiguration = new RequestConfiguration
                                                        .Builder()
                                                        .SetTestDeviceIds(deviceIDs)
                                                        .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
    }
}
