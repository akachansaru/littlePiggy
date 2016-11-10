// Adapted from: https://unity3d.com/learn/tutorials/topics/analytics/integrating-unity-iap-your-game
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener {
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	// Product identifiers for all products capable of being purchased: 
	// "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
	// counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
	// also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	// General product identifiers for the consumable, non-consumable, and subscription products.
	// Use these handles in the code to reference which product to purchase. Also use these values 
	// when defining the Product Identifiers on the store.
	public static string productIDHeadItemRound = "RoundHat";
	public static string productIDClothSmall = "ClothSmall";
	public static string productIDClothMedium = "ClothMedium";
	public static string productIDClothLarge = "ClothLarge";

	void Start() {
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null) {
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() {
		if (IsInitialized()) {
			// Already connected to Purchasing
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		builder.AddProduct(productIDClothSmall, ProductType.Consumable);
		builder.AddProduct(productIDClothMedium, ProductType.Consumable);
		builder.AddProduct(productIDClothLarge, ProductType.Consumable);
		// Continue adding the non-consumable product.
		builder.AddProduct(productIDHeadItemRound, ProductType.NonConsumable);

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized() {
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyCloth(string size) {
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		switch (size) {
		case "Small":
			BuyProductID (productIDClothSmall);
			break;
		case "Medium":
			BuyProductID (productIDClothMedium);
			break;
		case "Large":
			BuyProductID (productIDClothLarge);
			break;
		default:
			Debug.LogError("Amount of cloth does not exist. Set up new amount?");
			break;
		}
	}


	public void BuyNonConsumable(string id) {
		// Buy the non-consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID (id);
	}


	void BuyProductID(string productId)	{
		// If Purchasing has been initialized ...
		if (IsInitialized()) {
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)	{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			else {
				// ... report the product look-up failure situation  
				Debug.LogError("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else {
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID: FAIL. Not initialized.");
		}
	}

	//  
	// --- IStoreListener
	//
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error) {
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	/// <summary>
	/// This is where the purchased item is incorporated into the game.
	/// </summary>
	/// <returns>The purchase.</returns>
	/// <param name="args">Arguments.</param>
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
		// A cloth bundle has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, productIDClothSmall, StringComparison.Ordinal))	{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			GlobalControl.Instance.savedData.SafeClothCount += 5;
			Debug.Log (string.Format("Cloth: '{0}'", GlobalControl.Instance.savedData.SafeClothCount));
		} else if (String.Equals(args.purchasedProduct.definition.id, productIDClothMedium, StringComparison.Ordinal))	{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			GlobalControl.Instance.savedData.SafeClothCount += 30;
			Debug.Log (string.Format("Cloth: '{0}'", GlobalControl.Instance.savedData.SafeClothCount));
		} else if (String.Equals(args.purchasedProduct.definition.id, productIDClothLarge, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			GlobalControl.Instance.savedData.SafeClothCount += 250;
			Debug.Log (string.Format("Cloth: '{0}'", GlobalControl.Instance.savedData.SafeClothCount));
		} else if (String.Equals(args.purchasedProduct.definition.id, productIDHeadItemRound, StringComparison.Ordinal)) {
			// Or ... a non-consumable product has been purchased by this user.
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			GlobalControl.Instance.savedData.unlockedHeadItems.Add (new HeadItem(productIDHeadItemRound, SerializableColor.white, new SpriteRenderer(),
				50, 100, false));
		} else {
			// Or ... an unknown product has been purchased by this user.
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed.
		GlobalControl.Instance.Save();
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}