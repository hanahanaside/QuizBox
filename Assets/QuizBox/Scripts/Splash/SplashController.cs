using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	IEnumerator Start () {
		SoundManager.Instance.PlaySESound (SoundManager.SE_CHANNEL.Hanauta);
		yield return new WaitForSeconds (3.0f);
		Application.LoadLevel ("Top");
	}
}
