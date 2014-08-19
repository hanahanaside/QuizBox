using UnityEngine;
using System.Collections;

public class UIRootAdjustScript : MonoBehaviour {

	// 横幅を 640 px で固定
	int FixedWidth = 640;
	
	// Use this for initialization
	void Awake () {
		// UI Root の取得
		UIRoot root = GetComponent<UIRoot> ();
		// 高さ固定モードに
		root.scalingStyle = UIRoot.Scaling.FixedSize;
		// 画面サイズから、横幅を固定した場合の縦幅を計算して設定する
		root.manualHeight = Mathf.FloorToInt ((Screen.height * FixedWidth) / Screen.width);
	}
}
