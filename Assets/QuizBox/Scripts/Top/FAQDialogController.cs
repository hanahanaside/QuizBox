using UnityEngine;
using System.Collections;

public class FAQDialogController : MonoBehaviour {

	public void OnCloseButtonClicked(){
		Destroy (transform.parent.gameObject);
	}
}
