using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	IEnumerator Start () {
		SoundManager.Instance.PlaySESound (SoundManager.HANAUTA_SOUND_ID);
		yield return new WaitForSeconds (3.0f);
		Application.LoadLevel ("Top");
	}
}
