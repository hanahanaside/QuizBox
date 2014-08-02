using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public void Line(){
		Application.OpenURL("http://line.naver.jp/R/msg/text/?" + WWW.EscapeURL("テキスト", System.Text.Encoding.UTF8));
	}

}
