using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Notion;
using System.Threading.Tasks;

public class NotionREST : MonoBehaviour
{
    readonly string url = "https://api.notion.com/v1";

    REST rest = null;

    private void Awake()
    {
        rest = new REST();
    }

    string Authenticate(string token)
    {
        string auth = "Bearer " + token;
        return auth;
    }

    // -----
    // DB 一覧の取得、jsonをパラメータにし、filterで取得することが可能
    // https://developers.notion.com/reference/post-database-query
    public async Task<ResultPage> QueryDatabase(string token, string database_id)
    {
        try
        {
            string auth = Authenticate(token);
            string _url = url + "/databases/" + database_id + "/query";

            string result = await rest.Post(_url, auth, "");
            var resultPage = JsonUtility.FromJson<ResultPage>(result);
            return resultPage;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    // -----
    // https://developers.notion.com/reference/get-database
    public async Task RetrieveDatabase(string token, string database_id, Action<string> callback)
    {
        try
        {
            string auth = Authenticate(token);
            string _url = url + "/databases/" + database_id;

            string result = await rest.Get(_url, auth);
            Debug.LogWarning(result);
            callback?.Invoke(result);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // -----
    // https://developers.notion.com/reference/get-block-children
    public async Task RetrieveBlockChildren(string token, string block_id, Action<string> callback)
    {
        try
        {
            string auth = Authenticate(token);
            string _url = url + "/blocks/" + block_id + "/children";

            string result = await rest.Get(_url, auth);
            Debug.LogWarning(result);
            callback?.Invoke(result);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // -----
    // DB に Page を Create
    // https://developers.notion.com/reference/post-page
    public async Task CreatePage(string token, Page createPage)
    {
        try
        {
            string auth = Authenticate(token);
            string _url = url + "/pages";

            string jsonStr = JsonUtility.ToJson(createPage, true);
            string result = await rest.Post(_url, auth, jsonStr);
            Debug.Log("result: " + result);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    // -----
    // https://developers.notion.com/reference/get-page
    public async Task RetrievePage(string token, string page_id, Action<string> callback)
    {
        try
        {
            string auth = Authenticate(token);
            string _url = url + "/pages/" + page_id;

            string result = await rest.Get(_url, auth);
            Debug.LogWarning(result);
            callback?.Invoke(result);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

}
