﻿using UnityEngine;
using System.Collections;

public class SettingDialogController : MonoBehaviour {

	public UISprite soundModeButton;
	public GameObject userPolicyDialogPrefab;
	public GameObject privacyPolicyDialogPrefab;
	public GameObject faqDialogPrefab;

	void Start () {
		if (PrefsManager.Instance.IsSoundON ()) {
			soundModeButton.spriteName = "bt_sound_on";
		} else {
			soundModeButton.spriteName = "bt_sound_off";
		}
	}

	public void OnSoundClicked () {
		if (PrefsManager.Instance.IsSoundON ()) {
			PrefsManager.Instance.SaveSoundON (false);
			soundModeButton.spriteName = "bt_sound_off";
		} else {
			PrefsManager.Instance.SaveSoundON (true);
			soundModeButton.spriteName = "bt_sound_on";
		}
	}

	public void OnReviewClicked () {
		Application.OpenURL ("https://itunes.apple.com/app/id906906783");
	}

	public void OnInformationClicked () {
#if UNITY_IPHONE
		EtceteraBinding.showMailComposer("hanauta.app@gmail.com","クイズボックス(i)","お問い合わせ内容を書いてください",false);
#endif
	}

	public void OnFAQClicked () {
		GameObject faqDialog = Instantiate(faqDialogPrefab) as GameObject;
		ShowDialog(faqDialog);
	}

	public void OnUsePolicyClicked () {
		GameObject userPolicyDialog = Instantiate (userPolicyDialogPrefab)as GameObject;
		ShowDialog (userPolicyDialog);
	}

	public void OnPrivacyPolicyClicked () {
		GameObject privacyPolicyDialog = Instantiate (privacyPolicyDialogPrefab) as GameObject;
		ShowDialog (privacyPolicyDialog);
	}

	private void ShowDialog (GameObject dialog) {
		TopController.Instance.SetCurrentDialog (dialog);
		dialog.transform.parent = UIRootInstanceKeeper.Instance.transform;
		dialog.transform.localScale = new Vector3 (1, 1, 1);
		Destroy (transform.parent.gameObject);

	}
}
