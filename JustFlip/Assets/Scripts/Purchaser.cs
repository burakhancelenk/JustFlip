using System ;
using UnityEngine.Purchasing;
using UnityEngine;
using UnityEngine.UI ;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour, IStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        private static bool buyEMP50Trigger ;
        private static bool buyEMP100Trigger ;
        private static bool buyEMP250Trigger ;

        public GameObject infPanel ;
        public Text purchaseFailedInformation ;
        public CircleCollider2D backButtonCollider ;

        public Button buyEMP50Button ;
        public Button buyEMP100Button ;
        public Button buyEMP250Button ;
        
        
        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
        public static string EMP50ID = "emp50consumableobject";   
        public static string EMP100ID = "emp100consumableobject";
        public static string EMP250ID =  "emp250consumableobject"; 
        
        void Start()
        {
            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }

            buyEMP50Trigger = false ;
            buyEMP100Trigger = false ;
            buyEMP250Trigger = false ;
            
            buyEMP50Button.onClick.AddListener(BuyEMP50);
            buyEMP100Button.onClick.AddListener(BuyEMP100);
            buyEMP250Button.onClick.AddListener(BuyEMP250);
        }

        public static bool GetBuyEMPTrigger(Type refType ,string key)
        {
            if (refType == typeof(ScoreHandler))
            {
                switch (key)
                {
                    case "50":
                        return buyEMP50Trigger ;
                    case "100":
                        return buyEMP100Trigger ;
                    case "250":
                        return buyEMP250Trigger ;
                }

                return false ;
            }
  
            return false ;
        }

        public static void ResetBuyEMPTrigger(Type refType,string key)
        {
            if (refType == typeof(ScoreHandler))
            {
                switch (key)
                {
                    case "50":
                        buyEMP50Trigger = false ;
                        break;
                    case "100":
                        buyEMP100Trigger = false ;
                        break;
                    case "250":
                        buyEMP250Trigger = false ;
                        break;
                } 
            }
           
        }
        
        public void InitializePurchasing() 
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }
            
            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            // Add a product to sell / restore by way of its identifier, associating the general identifier
            // with its store-specific identifiers.
            builder.AddProduct(EMP50ID, ProductType.Consumable);
            builder.AddProduct(EMP100ID, ProductType.Consumable);
            builder.AddProduct(EMP250ID, ProductType.Consumable);
            
            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }
        
        
        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }
        
        
        public void BuyEMP50()
        {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(EMP50ID);
            
            if (GameManager.audioManager!=null)
            {
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
            else
            {
                GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
        }
        
        
        public void BuyEMP100()
        {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(EMP100ID);
            
            if (GameManager.audioManager!=null)
            {
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
            else
            {
                GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
        }
        
        
        public void BuyEMP250()
        {
            // Buy the subscription product using its the general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            // Notice how we use the general product identifier in spite of this ID being mapped to
            // custom store-specific identifiers above.
            BuyProductID(EMP250ID);
            
            if (GameManager.audioManager!=null)
            {
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
            else
            {
                GameManager.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                GameManager.audioManager.PlaySoundFX("OtherButtonClick",1f);
            }
        }
        
        
        void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);
                
                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        
        
        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }
            
            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer || 
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");
                
                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            // Otherwise ...
            else
            {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }
        
        
        //  
        // --- IStoreListener
        //
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");
            
            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }
        
        
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }
        
        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
        {
            // A consumable product has been purchased by this user.
            if (String.Equals(args.purchasedProduct.definition.id, EMP50ID, StringComparison.Ordinal))
            {
               infPanel.SetActive(true);
                backButtonCollider.enabled = false ;
                if (Tutorial.languageEnglish)
                {
                    purchaseFailedInformation.text = "Purchase process completed! You purchased 50 EMPs" ; 
                }
                else
                {
                    purchaseFailedInformation.text = "Satın alma işlemi tamamlandı! 50 EMP satın aldın" ; 
                }
                
                buyEMP50Trigger = true ;


            }
            // Or ... a non-consumable product has been purchased by this user.
            else if (String.Equals(args.purchasedProduct.definition.id, EMP100ID, StringComparison.Ordinal))
            {
                infPanel.SetActive(true);
                backButtonCollider.enabled = false ;
                if (Tutorial.languageEnglish)
                {
                    purchaseFailedInformation.text = "Purchase process completed! You purchased 100 EMPs" ; 
                }
                else
                {
                    purchaseFailedInformation.text = "Satın alma işlemi tamamlandı! 100 EMP satın aldın" ; 
                }
                buyEMP100Trigger = true ;
            }
            // Or ... a subscription product has been purchased by this user.
            else if (String.Equals(args.purchasedProduct.definition.id, EMP250ID, StringComparison.Ordinal))
            {
                infPanel.SetActive(true);
                backButtonCollider.enabled = false ;
                if (Tutorial.languageEnglish)
                {
                    purchaseFailedInformation.text = "Purchase process completed! You purchased 250 EMPs" ; 
                }
                else
                {
                    purchaseFailedInformation.text = "Satın alma işlemi tamamlandı! 250 EMP satın aldın" ; 
                }
                buyEMP250Trigger = true ;
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }
        
        
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            infPanel.SetActive(true);
            backButtonCollider.enabled = false ;
            if (Tutorial.languageEnglish)
            {
                purchaseFailedInformation.text = "Opps! Purchase process failed. Please try again." ;
            }
            else
            {
                purchaseFailedInformation.text = "Opps! Satın alma işlemi başarısız. Lütfen tekrar deneyin." ;
            }
           
            
        }

       
    }
