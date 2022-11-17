using UnityEngine;

public class Initializeads : MonoBehaviour
{
    string appKey = "1756b968d";
    //private bool isActive = false;
    private void Awake()
    {
        //IronSource.Agent.init(appKey);
        IronSource.Agent.init (
            appKey,
            IronSourceAdUnits.REWARDED_VIDEO,
            IronSourceAdUnits.INTERSTITIAL, 
            IronSourceAdUnits.OFFERWALL, 
            IronSourceAdUnits.BANNER);
    }

    private void Start()
    {
        LoadBanner();
        HideBanner();
    }

    private void Update()
    {
        if (GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            ShowBanner();
        }
        else
        {
            HideBanner();
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void LoadBanner()
    {
        Debug.Log("Load Banner");
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART , IronSourceBannerPosition.TOP);
    }
    public void HideBanner()
    {
        IronSource.Agent.hideBanner();
    }

    public void ShowBanner()
    {
        //Show same banner again
        IronSource.Agent.displayBanner();
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
}
