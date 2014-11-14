using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABManager : MonoBehaviour {
	public string[] skuArray;
	#if UNITY_ANDROID
	private const string ENCODE_PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnJQKrNPiuR3RDojGvgE1ln/6OkhA60hp28gUZ7jwlE0uPfTr6NpYVHXM5kuvYldxOSiJjAT2HXKKHmLzpuqHJLcgQco/qs1Xq3QQtndueM4jxsG2H4Uyq6/upS04p/56hoTD4lob6CVZyDJcwPoM4x/CW6DXUWpV+3Y1gxUhEVRPg3F5aeQRBBybLTngCLV6VZ0KnBNa4oB4v2H/1pgNlKfCTqv2GNIK7Cr8ZibF07caH0pz7zkCrISmdPghvdzxBR+/ltRLs4U/6kE5A1azOtlnG0XqsnGGjCuhsungzEvB1e//pe8/dLRnfVCRY7AkdeYwdENNu/UuGyHDYLqdWwIDAQAB";

	private static IABManager sInstance;

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
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		GoogleIAB.init (ENCODE_PUBLIC_KEY);
	}

	void Start () {

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
		TopController.Instance.UpdateUserPointLabel ();
	}

	public static IABManager Instance{
		get{
			return sInstance;
		}
	}

	public void PurchaseSku (int skuIndex) {
		string sku = skuArray [skuIndex];
		Debug.Log ("sku = " + sku);
		GoogleIAB.purchaseProduct (sku);
	}
	#endif
}
