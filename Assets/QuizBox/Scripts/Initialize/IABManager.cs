using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABManager : MonoSingleton<IABManager> {
	public string encodePublicKey = "";
	public string[] skuArray;

	#if UNITY_ANDROID

	void OnEnable () {
		// Listen to all events for illustration purposes
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}

	void OnDisable () {
		// Remove all event handlers
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	void Awake () {
	#if !UNITY_EDITOR
		DontDestroyOnLoad (gameObject);
		GoogleIAB.init (encodePublicKey);
	#endif
	}
		
	void purchaseFailedEvent (string error) {
		Debug.Log ("purchaseFailedEvent: " + error);
	}

	void consumePurchaseSucceededEvent (GooglePurchase purchase) {
		Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
	}

	void consumePurchaseFailedEvent (string error) {
		Debug.Log ("consumePurchaseFailedEvent: " + error);
	}

	void billingSupportedEvent () {
		Debug.Log ("billingSupportedEvent");
		GoogleIAB.queryInventory (skuArray);
	}

	void billingNotSupportedEvent (string error) {
		Debug.Log ("billingNotSupportedEvent: " + error);
	}

	void queryInventorySucceededEvent (List<GooglePurchase> purchases, List<GoogleSkuInfo> skus) {
		Debug.Log (string.Format ("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
		Prime31.Utils.logObject (purchases);
		Prime31.Utils.logObject (skus);
	}

	void queryInventoryFailedEvent (string error) {
		Debug.Log ("queryInventoryFailedEvent: " + error);
	}

	void purchaseCompleteAwaitingVerificationEvent (string purchaseData, string signature) {
		Debug.Log ("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
	}

	void purchaseSucceededEvent (GooglePurchase purchase) {
		Debug.Log ("purchaseSucceededEvent: " + purchase);
		Debug.Log ("id = " + purchase.productId);
		string productId = purchase.productId;
		int addPoint = 0;
		if(productId == "100pt_quiz"){
			addPoint = 100;
		}
		if(productId == "600pt_quiz"){
			addPoint = 600;
		}
		if(productId == "1350pt_quiz"){
			addPoint = 1350;
		}
		if(productId == "3000pt_quiz"){
			addPoint = 3000;
		}
		if(productId == "7800pt_quiz"){
			addPoint = 7800;
		}
		if(productId == "25000pt_quiz"){
			addPoint = 25000;
		}
		GoogleIAB.consumeProduct (purchase.productId);
		PrefsManager.Instance.AddUserPoint(addPoint);
		TopController.instance.UpdateUserPointLabel ();
	}

	public void PurchaseSku (int skuIndex) {
		string sku = skuArray [skuIndex];
		Debug.Log ("sku = " + sku);
		GoogleIAB.purchaseProduct (sku);
	}
	#endif
}
