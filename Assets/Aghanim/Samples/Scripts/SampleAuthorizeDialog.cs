using Aghanim.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Aghanim.Samples.Scripts
{
    [HelpURL("https://github.com/aghanim-sdk/unity#samples")]
    public class SampleAuthorizeDialog : MonoBehaviour
    {
        [SerializeField]
        private InputField _playerId, _playerName, _avatarUrl;
        [SerializeField]
        private Button _sendButton;

        private void Awake()
        {
            _sendButton.onClick.AddListener(Send);
        }

        private void Send()
        {
            AghanimSDK.Authorize(_playerId.text, _playerName.text, _avatarUrl.text);
        }
    }
}