using System.Collections.Generic;
using Aghanim.Components;
using Aghanim.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts 
{
    public class SampleShop : MonoBehaviour 
    {
        [SerializeField]
        private Button _getItemsButton;
        [SerializeField]
        private SampleShopItem _itemPrefab;
        [SerializeField]
        private SampleShopItem _itemExternalPrefab;
        [SerializeField]
        private RectTransform _itemsContainer;

        private List<SampleShopItem> _itemsInstances = new();

        private void Awake() 
        {
            _getItemsButton.onClick.AddListener(GetItems);
        }

        private void GetItems()
        {
            AghanimSDK.GetItems(OnItemsReceived);
        }

        private void OnItemsReceived(ItemList itemList)
        {
            ClearItems();
            itemList.items.ForEach(CreateItem);
            itemList.items.ForEach(CreateExternalItem);
        }

        private void BuyItem(string sku) 
        {
            AghanimSDK.PurchaseItem(sku);
        }
        
        private void BuyExternalItem(string sku) 
        {
            Application.OpenURL(AghanimSDK.GetExternalBuyLink(sku));
        }

        private void CreateItem(Item item)
        {
            _itemsInstances.Add(Instantiate(_itemPrefab, _itemsContainer).SetContent(item, BuyItem));
        }
        
        private void CreateExternalItem(Item item)
        {
            _itemsInstances.Add(Instantiate(_itemExternalPrefab, _itemsContainer).SetContent(item, BuyExternalItem));
        }

        private void ClearItems()
        {
            _itemsInstances.ForEach(i => Destroy(i.gameObject));
            _itemsInstances.Clear();
        }
    }
}
