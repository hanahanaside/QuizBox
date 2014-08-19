using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductListKeeper : MonoBehaviour {
	
	public string[] productIdentifiers;
	#if UNITY_IPHONE
	private static ProductListKeeper sInstance;
	private static List<StoreKitProduct> _products;

	void OnEnable()
	{
		StoreKitManager.productListReceivedEvent += ReceivedProductsList;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailedEvent;
	}
	
	void OnDisable()
	{
		StoreKitManager.productListReceivedEvent -= ReceivedProductsList;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailedEvent;
	}
	
	
	// Use this for initialization
	void Start () {
		if(sInstance == null){
			sInstance = this;
		}
		if(_products == null){
			requestProductData();
		}
	}

	void productListRequestFailedEvent(string error){
		Debug.Log("productListRequestFailedEvent");
		Debug.Log(error);
		requestProductData();
	}
	
	void ReceivedProductsList(List<StoreKitProduct> productsList){
		if(productsList != null && productsList.Count >0){
			_products = productsList;
			Debug.Log ("received total products: " + productsList.Count);
			Debug.Log("name = "+productList[0].productIdentifier);
		}
	}

	public static ProductListKeeper instance{
		get{
			return sInstance;
		}
	}

	public List<StoreKitProduct> productList{
		get{
			return _products;
		}
	}

	public void requestProductData(){
		Debug.Log("requestProductData");
		StoreKitBinding.requestProductData (productIdentifiers);
	}


#endif

}
