using Aghanim.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts
{
    [HelpURL("https://github.com/aghanim-sdk/unity#samples")]
    public class SamplePurchaseItemDialog : MonoBehaviour
    {
        [SerializeField]
        private InputField _sku, _itemName, _description, _imageUrl;
        [SerializeField]
        private Button _sendButton;

        private void Awake()
        {
            _sendButton.onClick.AddListener(Send);
        }

        private void Send()
        {
            AghanimSDK.PurchaseItem(_sku.text, _itemName.text, _description.text, _imageUrl.text);
        }
    }
}