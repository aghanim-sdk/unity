using System;
using UnityEngine;
using UnityEngine.UI;

namespace WebPlatform.Components
{
    public class TokenText : MonoBehaviour
    {
        [SerializeField]
        private WebPlatformClient _webPlatformClient;
        [SerializeField]
        private Text _tokenText;

        private void Awake()
        {
            _webPlatformClient.OnTokenReceived += OnTokenReceived;
        }

        private void OnTokenReceived(string token)
        {
            _tokenText.text = token;
        }
    }
}