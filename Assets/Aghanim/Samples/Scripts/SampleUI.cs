using System.Collections.Generic;
using Aghanim.Components;
using Aghanim.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts 
{
    [HelpURL("https://github.com/aghanim-sdk/unity#samples")]
    public class SampleUI : MonoBehaviour 
    {
        [SerializeField]
        private Button _getItemsButton;
        [SerializeField]
        private Button _authorize1Button;
        [SerializeField]
        private Button _authorize2Button;
        [SerializeField]
        private Button _buyItemButton;
        [SerializeField]
        private SampleShopItem _itemPrefab;
        [SerializeField]
        private SampleShopItem _itemExternalPrefab;
        [SerializeField]
        private RectTransform _itemsContainer;
        [SerializeField]
        private SampleAuthorizeDialog _authorizeDialog;
        [SerializeField]
        private SamplePurchaseItemDialog _purchaseItemDialog;

        private List<SampleShopItem> _itemsInstances = new();

        private void Awake() 
        {
            _getItemsButton.onClick.AddListener(GetItems);
            _authorize1Button.onClick.AddListener(AghanimSDK.Authorize);
            _authorize2Button.onClick.AddListener(OpenAuthorizeDialog);
            _buyItemButton.onClick.AddListener(OpenPurchaseItemDialog);
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
        
        private void OpenAuthorizeDialog() => _authorizeDialog.gameObject.SetActive(true);
        private void OpenPurchaseItemDialog() => _purchaseItemDialog.gameObject.SetActive(true);
    }
}
