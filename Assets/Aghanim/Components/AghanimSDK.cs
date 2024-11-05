using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using Aghanim.Responses;
using UnityEngine;
using UnityEngine.Events;

namespace Aghanim.Components
{
    public class AghanimSDK : AghanimProvider
    {
        private static AghanimSDK instance;
        
        [DllImport("__Internal")]
        private static extern void Loaded();
        
        [DllImport("__Internal")]
        private static extern void GetItemsList();
    
        [DllImport("__Internal")]
        private static extern void Order(string item, string itemName, string description, string imageUrl);
        
        [DllImport("__Internal")]
        private static extern void Auth(string playerId, string playerName, string avatarUrl);

        [SerializeField]
        private string ExternalBuyLink = "https://westland-survival-cowboy-rpg.aghanim.dev/go/checkout?player_id={0}&item_sku={1}";
        [SerializeField]
        private string InitEndpoint = "https://backend/api/session/init";
        [SerializeField, Space]
        private UnityEvent<SessionInitResponse> OnSessionInit;
        
        private readonly Queue<Action<ItemList>> _itemsListListeners = new();

        public static Action<string> OnTokenReceived = token => {};

        private string _token;
        
        public static string GetExternalBuyLink(string itemSku)
        {
            return string.Format(instance.ExternalBuyLink, instance._token, itemSku);
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
        public static void PurchaseItem(string sku)
        {
            Order(sku, string.Empty, string.Empty, string.Empty);
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
        
        private void Start()
        {
            Loaded();
            
            var uri = new Uri(Application.absoluteURL);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
        
            _token = queryParams.Get("token");
        
            if (string.IsNullOrEmpty(_token)) 
            {
                LogError("Missing 'token' parameter.");
                return;
            }

            OnTokenReceived(_token);
        
            ApiGet<SessionInitResponse>($"{InitEndpoint}{uri.Query}", SessionInitCallback);
        }

        private void SessionInitCallback(SessionInitResponse sessionInitResponse) 
        {
            Log("The session was successfully initialized.");
            OnSessionInit?.Invoke(sessionInitResponse);
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