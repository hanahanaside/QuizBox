using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreKitEventListener : MonoBehaviour
{

#if UNITY_IPHONE
	void OnEnable()
	{
		// Listens to all the StoreKit events. All event listeners MUST be removed before this object is disposed!
		StoreKitManager.transactionUpdatedEvent += transactionUpdatedEvent;
		StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent += purchaseFailedEvent;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailedEvent;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailedEvent;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinishedEvent;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;

	}
	
	
	void OnDisable()
	{
		// Remove all the event handlers
		StoreKitManager.transactionUpdatedEvent -= transactionUpdatedEvent;
		StoreKitManager.productPurchaseAwaitingConfirmationEvent -= productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent -= purchaseFailedEvent;
		StoreKitManager.productListReceivedEvent -= productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailedEvent;
		StoreKitManager.restoreTransactionsFailedEvent -= restoreTransactionsFailedEvent;
		StoreKitManager.restoreTransactionsFinishedEvent -= restoreTransactionsFinishedEvent;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent -= paymentQueueUpdatedDownloadsEvent;

	}
	
	
	
	void transactionUpdatedEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "transactionUpdatedEvent: " + transaction );
	}

	
	void productListReceivedEvent( List<StoreKitProduct> productList )
	{
		Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );
		hideActivityView();
		
		// print the products to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
	}
	
	
	void productListRequestFailedEvent( string error )
	{
		Debug.Log( "productListRequestFailedEvent: " + error );
		hideActivityView();
		string[] buttons = new string[] {"OK"};
		string title = "\u63a5\u7d9a\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f\u3002";
		string message = "\u3082\u3046\uff11\u5ea6\u304a\u8a66\u3057\u304f\u3060\u3055\u3044 ";
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
	}
	

	void purchaseFailedEvent( string error )
	{
		Debug.Log( "purchaseFailedEvent: " + error );
		hideActivityView();
		string message = "\u8cfc\u5165\u3057\u307e\u305b\u3093\u3067\u3057\u305f";
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(error,message,buttons);
	}
	

	void purchaseCancelledEvent( string error )
	{
		Debug.Log( "purchaseCancelledEvent: " + error );
		hideActivityView();
	}
	
	
	void productPurchaseAwaitingConfirmationEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "productPurchaseAwaitingConfirmationEvent: " + transaction );
	}
	
	
	void purchaseSuccessfulEvent( StoreKitTransaction transaction )
	{
		Debug.Log("purchaseSuccessfulEvent");
		//complete purchase
		Debug.Log("productIdentifier = "+transaction.productIdentifier);
		hideActivityView();
		string productIdentifer = transaction.productIdentifier;
		int addPoint = 0;
		if(productIdentifer == "100pt_quiz"){
			addPoint = 100;
		}
		if(productIdentifer == "600pt_quiz"){
			addPoint = 600;
		}
		if(productIdentifer == "1350pt_quiz"){
			addPoint = 1350;
		}
		if(productIdentifer == "3000pt_quiz"){
			addPoint = 3000;
		}
		if(productIdentifer == "7800pt_quiz"){
			addPoint = 7800;
		}
		if(productIdentifer == "25000pt_quiz"){
			addPoint = 25000;
		}
		Debug.Log("addPoint = "+addPoint);
		PrefsManager.Instance.AddUserPoint(addPoint);
		TopController.instance.UpdateUserPointLabel ();
	}
	
	
	void restoreTransactionsFailedEvent( string error )
	{
		Debug.Log( "restoreTransactionsFailedEvent: " + error );
	}
	
	
	void restoreTransactionsFinishedEvent()
	{
		Debug.Log( "restoreTransactionsFinished" );
	}
	
	
	void paymentQueueUpdatedDownloadsEvent( List<StoreKitDownload> downloads )
	{
		Debug.Log( "paymentQueueUpdatedDownloadsEvent: " );
		foreach( var dl in downloads )
			Debug.Log( dl );
	}

	private void hideActivityView(){
		FenceInstanceKeeper.Instance.SetActive(false);
		EtceteraBinding.hideActivityView();
	}
	
#endif
}

