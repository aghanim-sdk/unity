using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Components
{
    public class TokenText : MonoBehaviour
    {
        [SerializeField]
        private Text _tokenText;

        private void Awake()
        {
            AghanimSDK.OnTokenReceived += OnTokenReceived;
        }

        private void OnTokenReceived(string token)
        {
            _tokenText.text = token;
        }
    }
}