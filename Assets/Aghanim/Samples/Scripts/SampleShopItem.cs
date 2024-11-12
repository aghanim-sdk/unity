using System;
using Aghanim.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts
{
    [HelpURL("https://github.com/aghanim-sdk/unity#samples")]
    public class SampleShopItem : MonoBehaviour
    {
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _namePrice;
        [SerializeField]
        private Text _nameCurrency;
        [SerializeField]
        private Button _buyButton;

        private string _sku;
        
        public SampleShopItem SetContent(Item item, Action<string> buyAction)
        {
            _sku = item.sku;
            _nameText.text = item.name;
            _namePrice.text = (item.price / 100f).ToString("F2");;
            _nameCurrency.text = item.currency;
            _buyButton.onClick.AddListener(() => buyAction?.Invoke(_sku));
            return this;
        }
    }
}