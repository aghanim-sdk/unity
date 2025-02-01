using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using Aghanim.Responses;
using UnityEngine;
using UnityEngine.Events;

namespace Aghanim.Components
{
    [HelpURL("https://github.com/aghanim-sdk/unity")]
    public class AghanimSDK : AghanimProvider
    {
        private static AghanimSDK instance;
        
        [DllImport("__Internal")]
        private static extern void Loaded();
        
        [DllImport("__Internal")]
        private static extern void GetItemsList();
        
        [DllImport("__Internal")]
        private static extern void GetUnhandledPaidOrders();
    
        [DllImport("__Internal")]
        private static extern void Order(string item, string itemName, string description, string imageUrl);
        
        [DllImport("__Internal")]
        private static extern void Auth(string playerId, string playerName, string avatarUrl);
        [DllImport("__Internal")]
        private static extern void AppsFlyerSendEvent(string eventName, string eventValueString);
        [DllImport("__Internal")]
        private static extern void AppsFlyerSetCustomerUserId(string userId);

        [DllImport("__Internal")]
        private static extern void CallReloadPage();

        [SerializeField, Tooltip("Use {0} for player id and {1} for item sku")]
        private string ExternalBuyLink = "https://westland-survival-cowboy-rpg.aghanim.dev/go/checkout?player_id={0}&item_sku={1}";
        [SerializeField, Tooltip("Session init endpoint on the game provider server")]
        private string InitEndpoint = "https://backend/api/session/init";
        [SerializeField, Space]
        private UnityEvent<SessionInitResponse> OnSessionInit;
        [SerializeField, Space]
        private UnityEvent<string> OnSessionInitError;
        [SerializeField, Space]
        private UnityEvent<OrderStatus> OnOrderStatusReceived;
        
        private readonly Queue<Action<ItemList>> _itemsListListeners = new();
        private readonly Queue<Action<OrderStatusList>> _unhandledPaidOrdersListeners = new();
        private readonly Queue<Action<OrderStatus>> _orderStatusListeners = new();

        public static Action<string> onTokenReceived = _ => {};
        
        public static class AppsFlyer
        {
            /// <summary>
            /// Sends an event to AppsFlyer for tracking.
            /// </summary>
            /// <param name="eventName">The name of the event to track.</param>
            /// <param name="eventParameters">The object containing event-specific parameters.</param>
            public static void SendEvent<T>(string eventName, T eventParameters)
            {
                if (string.IsNullOrEmpty(eventName))
                {
                    throw new ArgumentException("Event name cannot be null or empty", nameof(eventName));
                }
                if (eventParameters == null)
                {
                    throw new ArgumentNullException(nameof(eventParameters), "Event parameters cannot be null");
                }
                var eventValueString = JsonUtility.ToJson(eventParameters);
                AppsFlyerSendEvent(eventName, eventValueString);
            }
            
            /// <summary>
            /// Sets the customer user ID in AppsFlyer.
            /// </summary>
            /// <param name="userId">The unique identifier of the user.</param>
            public static void SetCustomerUserId(string userId)
            {
                AppsFlyerSetCustomerUserId(userId);
            }
        }
        
        /// <summary>
        /// Generates external purchase link.
        /// </summary>
        /// <param name="itemSku">The unique identifier for the item to be purchased.</param>
        /// <returns>Link for Application.OpenURL()</returns>
        public static string GetExternalBuyLink(string itemSku)
        {
            return string.Format(instance.ExternalBuyLink, instance.token, itemSku);
        }

        /// <summary>
        /// Retrieves a list of items and adds a listener to handle the result once the list is available.
        /// </summary>
        /// <param name="listener">The action to be executed when the item list is retrieved.</param>
        public static void GetItems(Action<ItemList> listener)
        {
            instance._itemsListListeners.Enqueue(listener);
            GetItemsList();
        }

        /// <summary>
        /// Retrieves a list of orders and adds a listener to handle the result once the list is available.
        /// </summary>
        /// <param name="listener">The action to be executed when the orders list is retrieved.</param>
        public static void GetUnhandledPaidOrders(Action<OrderStatusList> listener)
        {
            instance._unhandledPaidOrdersListeners.Enqueue(listener);
            GetUnhandledPaidOrders();
        }
        
        // ReSharper disable once UnusedMember.Global
        public void OnItemsReceived(string itemListJson)
        {
            var itemList = JsonUtility.FromJson<ItemList>(itemListJson);
            
            while (_itemsListListeners.Count > 0)
            {
                _itemsListListeners.Dequeue()?.Invoke(itemList);
            }
        }
        
        /// <summary>
        /// Initiates a purchase with the specified SKU.
        /// </summary>
        /// <param name="sku">The unique identifier for the item to be purchased.</param>
        /// <param name="listener">The action to be executed when the status is retrieved.</param>
        public static void PurchaseItem(string sku)
        {
            Order(sku, string.Empty, string.Empty, string.Empty);
        }

        // ReSharper disable once UnusedMember.Global
        public void OnItemPurchaseStatus(string orderJson)
        {
            var orderStatus = JsonUtility.FromJson<OrderStatus>(orderJson);
            OnOrderStatusReceived?.Invoke(orderStatus);
        }
        
        // ReSharper disable once UnusedMember.Global
        public void OnUnhandledPaidOrdersReceived(string unhandledPaidOrdersJson)
        {
            var orderStatusList = JsonUtility.FromJson<OrderStatusList>(unhandledPaidOrdersJson);
            
            while (_unhandledPaidOrdersListeners.Count > 0)
            {
                _unhandledPaidOrdersListeners.Dequeue()?.Invoke(orderStatusList);
            }
        }
        
        /// <summary>
        /// Initiates a purchase with customizable item information.
        /// </summary>
        /// <param name="sku">The unique identifier for the item to be purchased.</param>
        /// <param name="itemName">The name of the item (optional).</param>
        /// <param name="description">The description of the item (optional).</param>
        /// <param name="imageUrl">The URL of the item’s image (optional).</param>
        public static void PurchaseItem(string sku, string itemName, string description, string imageUrl)
        {
            Order(sku, itemName, description, imageUrl);
        }

        /// <summary>
        /// Authorizes a player without saving player data.
        /// </summary>
        public static void Authorize()
        {
            Auth(string.Empty, string.Empty, string.Empty);
        }
        
        /// <summary>
        /// Authorizes a player with specified player ID and name.
        /// </summary>
        public static void Authorize(string playerId, string playerName)
        {
            Auth(playerId, playerName, string.Empty);
        }
        
        /// <summary>
        /// Authorizes a player with specified player ID, name, and avatar URL.
        /// </summary>
        public static void Authorize(string playerId, string playerName, string avatarUrl)
        {
            Auth(playerId, playerName, avatarUrl);
        }
        
        /// <summary>
        /// Reloads the current page.
        /// </summary>
        public static void ReloadPage()
        {
            CallReloadPage();
        }
        
        private void Start()
        {
            Loaded();
            
            var uri = new Uri(Application.absoluteURL);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
        
            token = queryParams.Get("token");
        
            if (string.IsNullOrEmpty(token)) 
            {
                LogError("Missing 'token' parameter.");
                return;
            }

            onTokenReceived(token);
        
            ApiGet<SessionInitResponse>($"{InitEndpoint}{uri.Query}", SessionInitCallback, SessionInitError);
        }

        private void SessionInitCallback(SessionInitResponse sessionInitResponse) 
        {
            Log("The session was successfully initialized.");
            OnSessionInit?.Invoke(sessionInitResponse);
        }
        
        private void SessionInitError(string error) 
        {
            LogError("Session initialization failed.");
            OnSessionInitError?.Invoke(error);
        }
        
        private void Awake() 
        {
            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer) 
            {
                Destroy(gameObject);
            }
            instance = this;
        }
    }
}