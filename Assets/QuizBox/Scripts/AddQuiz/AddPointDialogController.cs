using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPointDialogController : MonoBehaviour {

	public void OnCloseClick () {
		Destroy (transform.parent.gameObject);
	}

	public void On100ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (0);
#endif
	}

	public void On600ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (4);
#endif
	}

	public void On1350ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (1);
#endif
	}

	public void On3000ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (3);
#endif
	}

	public void On7800ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (5);
#endif
	}

	public void On25000ptButtonClicked () {
		#if UNITY_IPHONE
		purchaseProduct (2);
		#endif
	}

#if UNITY_IPHONE
	private void purchaseProduct (int index) {
		List<StoreKitProduct> productList = ProductListKeeper.instance.productList;
		if (productList == null) {
			return;
		}
		StoreKitProduct product = productList [index];
		
		Debug.Log ("preparing to purchase product: " + product.productIdentifier);
		StoreKitBinding.purchaseProduct (product.productIdentifier, 1);
		FenceInstanceKeeper.Instance.SetActive (true);
		EtceteraBinding.showBezelActivityViewWithLabel ("Loading");
	}
#endif
}
