using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Aghanim.Components
{
    public abstract class AghanimProvider : MonoBehaviour
    {
        protected Coroutine ApiGet<TResponse>(string route, Action<TResponse> callback) 
        {
            return StartCoroutine(ApiGetRequest(route, callback));
        }

        private static IEnumerator ApiGetRequest<TResponse>(string route, Action<TResponse> callback) 
        {
            using var webRequest = UnityWebRequest.Get(route);
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError) 
            {
                LogError(webRequest.error);
                yield break; 
            }
        
            callback(JsonUtility.FromJson<TResponse>(webRequest.downloadHandler.text));
        }
    
        protected static void Log(object value) => Debug.Log($"[{nameof(AghanimProvider)}] {value}");
        protected static void LogError(object value) => Debug.LogError($"[{nameof(AghanimProvider)}] {value}");
    }
}
