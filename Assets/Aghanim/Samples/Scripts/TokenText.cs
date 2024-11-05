﻿using Aghanim.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts
{
    public class TokenText : MonoBehaviour
    {
        [SerializeField]
        private Text _tokenText;

        private void Awake()
        {
            AghanimSDK.onTokenReceived += OnTokenReceived;
        }

        private void OnTokenReceived(string token)
        {
            _tokenText.text = token;
        }
    }
}