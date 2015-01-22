using UnityEngine;
using System.Collections;

public class SettingDialogController : MonoBehaviour {

	public UISprite BGMModeButton;
	public UISprite SEButton;
	public GameObject userPolicyDialogPrefab;
	public GameObject privacyPolicyDialogPrefab;
	public GameObject faqDialogPrefab;

	void Start () {
		if (PrefsManager.Instance.IsBGMON ()) {
			BGMModeButton.spriteName = "bt_sound_on";
		} else {
			BGMModeButton.spriteName = "bt_sound_off";
		}
		if(PrefsManager.Instance.IsSEON()){
			SEButton.spriteName = "bt_sound_on";
		}else {
			SEButton.spriteName = "bt_sound_off";
		}
	}

	public void OnBGMButtonClicked () {
		if (PrefsManager.Instance.IsBGMON ()) {
			PrefsManager.Instance.SaveBGMON (false);
			BGMModeButton.spriteName = "bt_sound_off";
			SoundManager.Instance.StopBGM ();
		} else {
			PrefsManager.Instance.SaveBGMON (true);
			BGMModeButton.spriteName = "bt_sound_on";
			SoundManager.Instance.PlayBGM (SoundManager.BGM_CHANNEL.Main);
		}
	}

	public void OnSEButtonClicked(){
		if(PrefsManager.Instance.IsSEON()){
			PrefsManager.Instance.SaveSEON (false);
			SEButton.spriteName = "bt_sound_off";
		}else {
			PrefsManager.Instance.SaveSEON (true);
			SEButton.spriteName = "bt_sound_on";
		}
	}

	public void OnReviewClicked () {
		PrefsManager.Instance.SetReviewed ();
		#if UNITY_IPHONE
		Application.OpenURL ("https://itunes.apple.com/app/id527092979");
		#endif

		#if UNITY_ANDROID
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.app.quizbox");
		#endif
	}

	public void OnInformationClicked () {
		string adress = "hanauta.app@gmail.com";
		string message = "お問い合わせ内容を書いてください";
#if UNITY_IPHONE
		EtceteraBinding.showMailComposer (adress, "クイズボックス(i)", message, false);
#endif

		#if UNITY_ANDROID
		EtceteraAndroid.showEmailComposer(adress,"クイズボックス(A)",message,false);
		#endif
	}

	public void OnFAQClicked () {
		ShowDialog (faqDialogPrefab);
	}

	public void OnUsePolicyClicked () {
		ShowDialog (userPolicyDialogPrefab);
	}

	public void OnPrivacyPolicyClicked () {
		ShowDialog (privacyPolicyDialogPrefab);
	}

	private void ShowDialog (GameObject dialogPrefab) {
		GameObject dialogObject = Instantiate (dialogPrefab) as GameObject;
		dialogObject.transform.parent = UIRootInstanceKeeper.Instance.transform;
		dialogObject.transform.localScale = new Vector3 (1, 1, 1);
	}
}
