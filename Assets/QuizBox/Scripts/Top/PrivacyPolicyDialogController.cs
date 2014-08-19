using UnityEngine;
using System.Collections;

public class PrivacyPolicyDialogController : MonoBehaviour {

	public void OnCloseButtonClicked(){
		Destroy(transform.parent.gameObject);
	}
}
