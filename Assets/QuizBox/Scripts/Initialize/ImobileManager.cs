using UnityEngine;
using System.Collections;

public class ImobileManager : MonoBehaviour {

	public string publisherId;
	private static ImobileManager sInstance;
	private int mBannerViewId = -1;
	private int mIconViewId = -1;
	private int mRectangleViewId = -1;
	public Account account;

	void Awake () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		string bannerMediaId;
		string bannerSpotId;
		string iconMediaId;
		string iconSpotId;
		string wallMediaId;
		string wallSpotId;
		string interstitialMediaId;
		string interstitialSpotId;
		string rectangleMediaId;
		string rectangleSpotId;
#if UNITY_IPHONE
		bannerMediaId = account.iOS.bannerMediaId;
		bannerSpotId = account.iOS.bannerSpotId;
		iconMediaId = account.iOS.iconMediaId;
		iconSpotId = account.iOS.iconSpotId;
		wallMediaId = account.iOS.wallMediaId;
		wallSpotId = account.iOS.wallSpotId;
		interstitialMediaId = account.iOS.interstitialMediaId;
		interstitialSpotId = account.iOS.interstitialSpotId;
		rectangleMediaId = account.iOS.rectangleMediaId;
		rectangleSpotId = account.iOS.rectangleSpotId;
#endif

#if UNITY_ANDROID
		bannerMediaId = account.Android.bannerMediaId;
		bannerSpotId = account.Android.bannerSpotId;
		iconMediaId = account.Android.iconMediaId;
		iconSpotId = account.Android.iconSpotId;
		wallMediaId = account.Android.wallMediaId;
		wallSpotId = account.Android.wallSpotId;
		interstitialMediaId = account.Android.interstitialMediaId;
		interstitialSpotId = account.Android.interstitialSpotId;
		rectangleMediaId = account.Android.rectangleMediaId;
		rectangleSpotId = account.Android.rectangleSpotId;
#endif

		#if !UNITY_EDITOR
		//prepare banner
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, bannerMediaId, bannerSpotId);
		IMobileSdkAdsUnityPlugin.start (bannerSpotId);
		mBannerViewId = IMobileSdkAdsUnityPlugin.show (bannerSpotId, IMobileSdkAdsUnityPlugin.AdType.BANNER, 
		IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER, 
		IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM);

		//prepare icon
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, iconMediaId, iconSpotId);
		IMobileSdkAdsUnityPlugin.start (iconSpotId);
		IMobileIconParams iconParams = new IMobileIconParams ();
		iconParams.iconNumber = 2;
		iconParams.iconTitleEnable = false;
		mIconViewId = IMobileSdkAdsUnityPlugin.show (iconSpotId, IMobileSdkAdsUnityPlugin.AdType.ICON, 0, 10, iconParams);

		//prepare wall
		IMobileSdkAdsUnityPlugin.registerFullScreen (publisherId, wallMediaId, wallSpotId);
		IMobileSdkAdsUnityPlugin.start (wallSpotId);

		//prepare intersticial 
		IMobileSdkAdsUnityPlugin.registerFullScreen (publisherId, interstitialMediaId, interstitialSpotId);
		IMobileSdkAdsUnityPlugin.start (interstitialSpotId);

		//prepare rectangle
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, rectangleMediaId, rectangleSpotId);
		IMobileSdkAdsUnityPlugin.start (rectangleSpotId);
		mRectangleViewId = IMobileSdkAdsUnityPlugin.show (rectangleSpotId, 
		IMobileSdkAdsUnityPlugin.AdType.MEDIUM_RECTANGLE,
		IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER, 
		IMobileSdkAdsUnityPlugin.AdValignPosition.MIDDLE);

		HideBannerAd ();
		HideIconAd (); 
		HideRectangleAd ();
		#endif
	}

	public static ImobileManager Instance {
		get {
			if(sInstance == null){
				sInstance = new ImobileManager ();
			}
			return sInstance;
		}
	}

	public void ShowIconAd () { 
		if (mIconViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, true);
		}
	}

	public void ShowBannerAd () {
		if (mBannerViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, true);
		}
	}

	public void HideIconAd () {
		if (mIconViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, false);
		}
	}

	public void HideBannerAd () {
		if (mBannerViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, false);
		}
	}

	public void ShowRectangleAd () {
		if(mRectangleViewId != -1){
			IMobileSdkAdsUnityPlugin.setVisibility (mRectangleViewId, true);
		}
	}

	public void HideRectangleAd () {
		if(mRectangleViewId != -1){
			IMobileSdkAdsUnityPlugin.setVisibility (mRectangleViewId, false);
		}
	}
		
	public void ShowWallAd () {
		#if UNITY_IPHONE
		IMobileSdkAdsUnityPlugin.show (account.iOS.wallSpotId);
		#endif
#if UNITY_ANDROID
		IMobileSdkAdsUnityPlugin.show (account.Android.wallSpotId);
#endif
	}

	public void ShowInterstitialAd () {
		#if UNITY_IPHONE
		IMobileSdkAdsUnityPlugin.show (account.iOS.interstitialSpotId);
		#endif

		#if UNITY_ANDROID
		IMobileSdkAdsUnityPlugin.show (account.Android.interstitialSpotId);
		#endif
	}
		
	[System.SerializableAttribute]
	public class Account {
		public ImobileID Android;
		public ImobileID iOS;
	}

	[System.SerializableAttribute]
	public class ImobileID {
		public string bannerMediaId;
		public string bannerSpotId;
		public string iconMediaId;
		public string iconSpotId;
		public string wallMediaId;
		public string wallSpotId;
		public string interstitialMediaId;
		public string interstitialSpotId;
		public string rectangleMediaId;
		public string rectangleSpotId;
	}

}
