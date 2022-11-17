using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static string uniqueUserId = "demoUserUnity";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("unity-script: MyAppStart Start called");

        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks (true);

        string id = IronSource.Agent.getAdvertiserId ();
        Debug.Log ("unity-script: IronSource.Agent.getAdvertiserId : " + id);
		
        Debug.Log ("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration ();

        Debug.Log ("unity-script: unity version" + IronSource.unityVersion ());

        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;		
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        // SDK init
        Debug.Log ("unity-script: IronSource.Agent.init");
        //IronSource.Agent.init ();
        //IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        //IronSource.Agent.initISDemandOnly (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);

        //Set User ID For Server To Server Integration
        //// IronSource.Agent.setUserId ("UserId");
		
        // Load Banner example
        IronSource.Agent.loadBanner (IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        
        IronSource.Agent.init ("1756b968d", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        
        //For Rewarded Video
        IronSource.Agent.init ("1756b968d", IronSourceAdUnits.REWARDED_VIDEO);
        //For Interstitial
        IronSource.Agent.init ("1756b968d", IronSourceAdUnits.INTERSTITIAL);
        //For Offerwall
        IronSource.Agent.init ("1756b968d", IronSourceAdUnits.OFFERWALL);
        //For Banners
        IronSource.Agent.init ("1756b968d", IronSourceAdUnits.BANNER);
    }

    // Update is called once per frame
    void Update()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        
    }
    private void SdkInitializationCompletedEvent(){}
    void OnApplicationPause(bool isPaused) {                 
      IronSource.Agent.onApplicationPause(isPaused);
    }

    //Banner Events
    void BannerAdLoadedEvent ()
    {
        Debug.Log ("unity-script: I got BannerAdLoadedEvent");
    }
	
    void BannerAdLoadFailedEvent (IronSourceError error)
    {
        Debug.Log ("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode () + ", description : " + error.getDescription ());
    }
	
    void BannerAdClickedEvent ()
    {
        Debug.Log ("unity-script: I got BannerAdClickedEvent");
    }
	
    void BannerAdScreenPresentedEvent ()
    {
        Debug.Log ("unity-script: I got BannerAdScreenPresentedEvent");
    }
	
    void BannerAdScreenDismissedEvent ()
    {
        Debug.Log ("unity-script: I got BannerAdScreenDismissedEvent");
    }
	
    void BannerAdLeftApplicationEvent ()
    {
        Debug.Log ("unity-script: I got BannerAdLeftApplicationEvent");
    }
}
