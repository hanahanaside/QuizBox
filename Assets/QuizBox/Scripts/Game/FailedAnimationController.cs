using UnityEngine;
using System.Collections;

public class FailedAnimationController : MonoBehaviour {

	void OnAnimationFinished(){
		Destroy(transform.parent.gameObject);
	}
}
