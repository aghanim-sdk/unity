using System;
using System.Collections;
using System.Web;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using WebPlatform.Responses;
using Debug = UnityEngine.Debug;

namespace WebPlatform.Components
{
    public class WebPlatformClient : MonoBehaviour
    {
        public string GetLink(string itemSku) => string.Format(ExternalBuyLink, _token, itemSku);
        
        public Action< string > OnTokenReceived = token => {};

        [SerializeField]
        private string ExternalBuyLink = "https://your-game-hub/go/checkout?player_id={0}&item_sku={1}";
        [SerializeField]
        private string InitEndpoint = "https://backend/api/session/init";
        [SerializeField, Space]
        private UnityEvent<SessionInitResponse> OnSessionInit;
    
        private string _token;

        private void Start() 
        {
            var uri = new Uri(Application.absoluteURL);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
        
            _token = queryParams.Get("token");
        
            if (string.IsNullOrEmpty(_token)) 
            {
                LogError("Missing 'token' parameter.");
                return;
            }

            OnTokenReceived(_token);
        
            ApiGet<SessionInitResponse>($"{InitEndpoint}{uri.Query}", SessionInitCallback);
        }

        private void SessionInitCallback(SessionInitResponse sessionInitResponse) 
        {
            Log("The session was successfully initialized.");
            OnSessionInit?.Invoke(sessionInitResponse);
        }

        private Coroutine ApiGet<TResponse>(string route, Action<TResponse> callback) 
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
    
        private static void Log(object value) => Debug.Log($"[{nameof(WebPlatformClient)}] {value}");
        private static void LogError(object value) => Debug.LogError($"[{nameof(WebPlatformClient)}] {value}");
    }
}
