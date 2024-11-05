using UnityEngine;

namespace Aghanim.Components
{
    public class DontDestroyComponent : MonoBehaviour 
    {
        private void Start() 
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}