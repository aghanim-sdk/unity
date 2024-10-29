using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebPlatform.Components;
using WebPlatform.Responses;

namespace Samples 
{
    public class SampleShop : MonoBehaviour 
    {
        [SerializeField]
        private WebAppWrapper _webAppWrapper;
        [SerializeField]
        private WebPlatformClient _webPlatformClient;
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
            _webAppWrapper.GetItems(OnItemsReceived);
        }

        private void OnItemsReceived(ItemList itemList)
        {
            ClearItems();
            itemList.items.ForEach(CreateItem);
            itemList.items.ForEach(CreateExternalItem);
        }

        private void BuyItem(string sku) 
        {
            _webAppWrapper.PurchaseItem(sku);
        }
        
        private void BuyExternalItem(string sku) 
        {
            Application.OpenURL(_webPlatformClient.GetLink(sku));
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
