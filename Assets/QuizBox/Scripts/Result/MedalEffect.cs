using UnityEngine;
using System.Collections;

public class MedalEffect : MonoBehaviour {

	[SerializeField]
	private int
		_RenderQueue = 4000;
	
	void Start () {

		renderer.material.renderQueue = _RenderQueue;
		
		Transform trans = transform;
		for (int i = 0; i < trans.childCount; i++) {
			trans.GetChild (i).gameObject.renderer.material.renderQueue = _RenderQueue;
		}
		Invoke ("DestroyObject",3.0f);
	}

	private void DestroyObject(){
		Destroy (gameObject);
	}
		
}
