using UnityEngine;

namespace WebPlatform.Components
{
    public class DontDestroyComponent : MonoBehaviour 
    {
        private void Start() 
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}