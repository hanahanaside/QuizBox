using UnityEngine;
using System.Collections;

public class AddPointDialogController : MonoBehaviour {

	public void OnButtonClick(){
		Destroy(transform.parent.gameObject);
	}
}
