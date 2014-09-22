using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Translate (0,0.01f,0);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("aaaa");
			int depth =  collision.gameObject.GetComponent<UISprite>().depth;
		Debug.Log ("d = " + depth);
		GetComponent<UISprite>().depth = depth +1;
	}
}
