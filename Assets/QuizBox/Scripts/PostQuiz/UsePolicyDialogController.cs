using UnityEngine;
using System.Collections;

public class UsePolicyDialogController : MonoBehaviour {

	public void OnCloseButtonClicked(){
		Destroy(transform.parent.gameObject);
	}
}
