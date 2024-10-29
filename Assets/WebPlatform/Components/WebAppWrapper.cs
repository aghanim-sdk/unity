using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using WebPlatform.Responses;

namespace WebPlatform.Components
{
    public class WebAppWrapper : MonoBehaviour 
    {
        [DllImport("__Internal")]
        private static extern void Loaded();
        
        [DllImport("__Internal")]
        private static extern void GetItemsList();
    
        [DllImport("__Internal")]
        private static extern void Order(string item);

        private readonly Queue<Action<ItemList>> _itemsListListeners = new();

        public void GetItems(Action<ItemList> listener)
        {
            Debug.Log("Unity: GetItems");
            _itemsListListeners.Enqueue(listener);
            GetItemsList();
        }
        
        public void OnItemsReceived(string itemListJson)
        {
            Debug.Log("Unity: OnItemsReceived");
            var itemList = JsonUtility.FromJson<ItemList>(itemListJson);
            
            while (_itemsListListeners.Count > 0)
            {
                _itemsListListeners.Dequeue()?.Invoke(itemList);
            }
        }

        public void PurchaseItem(string sku)
        {
            Debug.Log($"Unity: PurchaseItem {sku}");
            Order(sku);
        }

        private void Start() => Loaded();
    
        private void Awake() 
        {
            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer) 
            {
                Destroy(gameObject);
            }
        }
    }
}