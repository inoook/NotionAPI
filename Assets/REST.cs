using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class REST
{
    public async Task<string> Post(string url, string authorization, string jsonStr)
    {
        Debug.Log("Post_url: " + url);
        using (var request = new UnityWebRequest())
        {
            request.url = url;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                byte[] body = Encoding.UTF8.GetBytes(jsonStr);
                request.uploadHandler = new UploadHandlerRaw(body);
            }
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Authorization", authorization);
            request.SetRequestHeader("Notion-Version", "2021-05-13");
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

            request.method = UnityWebRequest.kHttpVerbPOST;

            await request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogWarning(request.error);
                return "";
            }
            else
            {
                Debug.LogWarning("responseCode: "+request.responseCode);
                return request.downloadHandler.text;
            }
        }
    }

    public async Task<string> Get(string url, string authorization)
    {
        Debug.Log("Get_url: " + url);
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", authorization);
            request.SetRequestHeader("Notion-Version", "2021-05-13");
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
            request.method = UnityWebRequest.kHttpVerbGET;
            await request.SendWebRequest();

            Debug.Log("responseCode: " + request.responseCode);
            if (request.isNetworkError || request.isHttpError)
            {
                return "";
            }
            else
            {
                return request.downloadHandler.text;
            }
        }
    }

    // 変更分を送信
    //public IEnumerator Patch(string url, string authorization, string jsonStr)
    //{
    //    var request = new UnityWebRequest();
    //    request.url = url;
    //    byte[] body = Encoding.UTF8.GetBytes(jsonStr);
    //    request.uploadHandler = new UploadHandlerRaw(body);
    //    request.downloadHandler = new DownloadHandlerBuffer();

    //    request.SetRequestHeader("Authorization", authorization);
    //    request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
    //    request.method = "PATCH";

    //    yield return request.SendWebRequest();

    //    if (request.isNetworkError)
    //    {
    //        Debug.Log(request.error);
    //    }
    //    else
    //    {
    //        if (request.responseCode == 201)
    //        {
    //            Debug.Log("success");
    //        }
    //        else
    //        {
    //            Debug.Log("failed: " + request.responseCode);
    //        }

    //        Debug.Log(request.downloadHandler.text);
    //    }
    //}

    //
    //public IEnumerator Delete(string url, string authorization)
    //{
    //    var request = new UnityWebRequest();
    //    request.url = url;
    //    request.downloadHandler = new DownloadHandlerBuffer();

    //    request.SetRequestHeader("Authorization", authorization);
    //    request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
    //    request.method = "DELETE";

    //    yield return request.SendWebRequest();

    //    if (request.isNetworkError)
    //    {
    //        Debug.Log(request.error);
    //    }
    //    else
    //    {
    //        if (request.responseCode == 204)
    //        {
    //            Debug.Log("No Content");
    //        }
    //        else
    //        {
    //            Debug.Log("failed: " + request.responseCode);
    //        }

    //        Debug.Log(request.downloadHandler.text);
    //    }
    //}

    //public IEnumerator GetImage(string url, System.Action<UnityWebRequest> successAct)
    //{
    //    UnityWebRequest request = UnityWebRequest.Get(url);
    //    request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
    //    request.method = UnityWebRequest.kHttpVerbGET;
    //    yield return request.SendWebRequest();

    //    if (request.isNetworkError || request.isHttpError)
    //    {
    //        Debug.LogWarning(request.error + " / " + url);
    //    }
    //    else
    //    {
    //        successAct?.Invoke(request);

    //        //  または、結果をバイナリデータとして取得します
    //        //byte[] results = request.downloadHandler.data;
    //    }
    //}

}
