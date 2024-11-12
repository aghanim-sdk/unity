using UnityEngine;

namespace Aghanim.Components
{
    [HelpURL("https://github.com/aghanim-sdk/unity")]
    public class DontDestroyComponent : MonoBehaviour 
    {
        private void Start() 
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}