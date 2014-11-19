using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class GameFeatObserver : MonoBehaviour {
	void Awake () {
		//GAMEFEAT initialize
		GFUnityPlugin.activate("media_id", true, true, true);

		// IconAd Sample
		GFUnityPlugin.initGFIconController();

		// set Icon
		GFUnityPlugin.addIconView(30 , 250, 57, 57);
		GFUnityPlugin.addIconView(100 , 250, 57, 57);
		GFUnityPlugin.addIconView(170, 250, 57, 57);
		// no Title set Icon
		GFUnityPlugin.addNoTitleIconView(240, 250, 57, 57);
		
		// drawGFButton1
		GFUnityPlugin.drawGFButton("button1", 10, 60, 100, 28);
		
		// drawGFButton2
		GFUnityPlugin.drawGFButton("button2", 10, 120, 100, 28);
		
		// show Icon
		GFUnityPlugin.iconLoadAd("media_id");
		GFUnityPlugin.setIconAdRefreshTiming(10);
		

		
	} 
	
	void Start () {
		//show Popup
		GFUnityPlugin.initGFPopupView();
		GFUnityPlugin.setPopupAdSchedule(1);
		GFUnityPlugin.popupLoadAd("media_id");
	}
	
	void OnDisable() {
		// stop Icon
		GFUnityPlugin.iconStopAd();
	}

	// callback from offer wall
	void didShowGameFeat(string msg) {
	}

	// callback from close event
	void didCloseGameFeat(string msg) {
	}
	
	// callback from show popup
	void didShowGameFeatPopup(string msg) {
	}
	
	// callback from close popup
	void didCloseGameFeatPopup(string msg) {
	}
	
	// callback from not close popup
	void failGameFeatPopupData(string msg) {
	}

	void OnGUI() {
		Rect rect = new Rect(10, 10, 400, 100);
		bool isClicked = GUI.Button(rect, "GAME FEAT");
		if (isClicked) {
			loadGF();
		}
	
		Rect rect2 = new Rect(10, 310, 400, 100);
		bool isClicked2 = GUI.Button(rect2, "invisible icon");
		if (isClicked2) {
			GFUnityPlugin.invisibleIconAd();
		}

		Rect rect3 = new Rect(10, 410, 400, 100);
		bool isClicked3 = GUI.Button(rect3, "visible icon");
		if (isClicked3) {
			GFUnityPlugin.visibleIconAd();
		}
		
		Rect rect4 = new Rect(10, 510, 400, 100);
		bool isClicked4 = GUI.Button(rect4, "show popup");
		if (isClicked4) {
			GFUnityPlugin.initGFPopupView();
			GFUnityPlugin.setPopupAdSchedule(1);
			GFUnityPlugin.popupLoadAd("media_id");
		}
	}

	void loadGF(){
		// normal offer wall sample
		// GFUnityPlugin.start("media_id");

		// callback offer wall sample
		GFUnityPlugin.startWtCallback("media_id","Main Camera");
	}

}
