using Aghanim.Responses;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts
{
    [HelpURL("https://github.com/aghanim-sdk/unity#samples")]
    public class SampleStatusDialog : MonoBehaviour
    {
        [SerializeField]
        private GameObject _container;
        [SerializeField]
        private Text _sku, _status;
        
        public void Open(OrderStatus orderStatus)
        {
            _sku.text = orderStatus.item_sku;
            _status.text = orderStatus.is_success ? "Success" : "Failed";
            _container.SetActive(true);
        }

        public void Close() => _container.SetActive(false);
    }
}