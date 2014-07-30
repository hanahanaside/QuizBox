using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public UISprite sprite;
	public UIAtlas atlas;

	void Start(){
		sprite.spriteName = "incorrect_answer";
		int height = atlas.GetSprite("incorrect_answer").height;
		int width = atlas.GetSprite("incorrect_answer").width;
		sprite.SetDimensions(width,height);
	}

	void OnClick(){
		Debug.Log("aaaaa");
	}
}
