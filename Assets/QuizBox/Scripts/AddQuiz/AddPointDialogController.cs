using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPointDialogController : MonoBehaviour {

	public void On100ptButtonClicked () {
		purchaseProduct (0);
	}

	public void On600ptButtonClicked () {
		purchaseProduct (4);
	}

	public void On1350ptButtonClicked () {
		purchaseProduct (1);
	}

	public void On3000ptButtonClicked () {
		purchaseProduct (3);
	}

	public void On7800ptButtonClicked () {
		purchaseProduct (5);
	}

	public void On25000ptButtonClicked () {
		purchaseProduct (2);
	}

	private void purchaseProduct (int index) {
		List<StoreKitProduct> productList = ProductListKeeper.instance.productList;
		StoreKitProduct product = productList [index];
		
		Debug.Log ("preparing to purchase product: " + product.productIdentifier);
		StoreKitBinding.purchaseProduct (product.productIdentifier, 1);
		FenceInstanceKeeper.Instance.SetActive(true);
		EtceteraBinding.showBezelActivityViewWithLabel ("Loading");
	}
}
