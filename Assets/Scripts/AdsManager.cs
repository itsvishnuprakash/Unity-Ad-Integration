using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4882197");
        Advertisement.AddListener(this); // For Rewarded Ads
        ShowBanner(); // Banner is displayed from starting
    }

    // This function is called whenever an interstetial ad has to be played
    public void PlayInterAd()
    {
        Advertisement.Show("level_end_inter");
    }

    // Function for displaying rewarded ad
    public void PlayrewardedAd()
    {
        if(Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else
        {
            //Do What you want
            Debug.Log("Rewarded ad is not Ready");
        }
    }

    // Interface Methods
    public void OnUnityAdsReady(string placementID)
    {
        Debug.Log("Ads are Ready");
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error : "+message);
    }
    public void OnUnityAdsDidStart(string placementID)
    {
        Debug.Log("Video started");
    }
    public void OnUnityAdsDidFinish(string placementID,ShowResult showResult)
    {
        // Do What has to be given after rewarded video is finished
        if(placementID=="Rewarded_Android" && showResult==ShowResult.Finished)
        {
            Debug.Log("player should be rewarded");
        }
    }

    // For showing banner ads
    public void ShowBanner()
    {
        if(Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_Android");
        }
        else
        {
            // If Banner is not ready, calling a coroutine to wait and load banner again.
            StartCoroutine(RepeatShowBanner());
        }
    }
    // Incase of hiding Banner
    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1f);
        ShowBanner();
    }
}
