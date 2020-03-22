using UnityEngine;
using StartApp;
using UnityEngine.Purchasing ;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
public class ADStarter : MonoBehaviour
{
	
    #if UNITY_ANDROID
	private class VideoListenerImplementation : StartAppWrapper.VideoListener , StartAppWrapper.AdEventListener
	{
		
		public void onVideoCompleted()
		{
		   // User has been rewarded. Startapp
			ScoreHandler.SetAdsTrigger(typeof(ADStarter));
			
		}
		
		public void onReceiveAd()
		{
			// Video has been loaded. StartApp
			StartAppWrapper.showAd();
		}
	
		public void onFailedToReceiveAd()
		{
			// Video couldn't be loaded . StartApp
		}
		
	}
	private VideoListenerImplementation videoListener ;
    #endif

	public static bool showAd ;
	

	
	
	void Start ()
	{
		showAd = false ;
		
        #if UNITY_ANDROID
		StartAppWrapper.init();
		videoListener = new VideoListenerImplementation ();
		StartAppWrapper.setVideoListener (videoListener);
        #endif
		
		#if UNITY_IOS
		AppLovin.SetSdkKey("x0q5Zv8N_lT4hi3CGTJTDbGJCRwRT1cT1RTbLh-KQRO79RKQvdz7DDRQeMtBNUfPwD_Fv-aKQ-RaPn_JOyRc9t");
		AppLovin.InitializeSdk();
	    AppLovin.SetUnityAdListener("Camera");
	    #endif	
	}
	
	
	
	
	
	

	private void Update()
	{
		if (showAd )
		{
			
            #if UNITY_ANDROID
			StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO,videoListener);
			showAd = false ;
            #endif
			
			#if UNITY_IOS
	        AppLovin.LoadRewardedInterstitial();
	        showAd = false;
	        #endif

		}

	}
    
	
	#if UNITY_IOS
	private void onAppLovinEventReceived(string appLovinEvent)
	{
		if (appLovinEvent.Contains("LOADEDREWARDED"))
		{
			// Video has been loaded. AppLovin
			AppLovin.ShowRewardedInterstitial();
			showAd = false ;
		}
		if (appLovinEvent.Contains("LOADREWARDEDFAILED"))
		{
			// Video could't be loaded. AppLovin
			AppLovin.LoadRewardedInterstitial();
		}

		if (appLovinEvent.Contains("REWARDAPPROVEDINFO"))
		{
			// User has been rewarded. AppLovin
			ScoreHandler.SetAdsTrigger(typeof(ADStarter));
		}
	}
	#endif
	
	
}
