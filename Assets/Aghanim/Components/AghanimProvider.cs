using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Aghanim.Components
{
    public abstract class AghanimProvider : MonoBehaviour
    {
        protected string token;
        
        protected Coroutine ApiGet<TResponse>(string route, Action<TResponse> callback, Action<string> error) 
        {
            return StartCoroutine(ApiGetRequest(route, callback, error));
        }

        private static IEnumerator ApiGetRequest<TResponse>(string route, Action<TResponse> callback, Action<string> error) 
        {
            using var webRequest = UnityWebRequest.Get(route);
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError) 
            {
                LogError(webRequest.error);
                error?.Invoke(webRequest.error);
                yield break; 
            }
        
            callback(JsonUtility.FromJson<TResponse>(webRequest.downloadHandler.text));
        }
    
        // ReSharper disable once StringLiteralTypo
        protected static void Log(object value) => Debug.Log($"[Aghanim] {value}");
        // ReSharper disable once StringLiteralTypo
        protected static void LogError(object value) => Debug.LogError($"[Aghanim] {value}");
    }
}
