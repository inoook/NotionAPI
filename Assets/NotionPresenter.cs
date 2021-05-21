using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Notion;
using System.Threading.Tasks;

public class NotionPresenter : MonoBehaviour
{
    [SerializeField] string settingFile = "setting.json";

    [SerializeField] NotionSetting setting = null;

    [SerializeField] NotionREST notionREST = null;
    [SerializeField] NotionCanvas notionCanvas = null;

    void Awake()
    {
        LoadSetting();
        Ui_Init();
    }

    // -----
    // Setting
    void LoadSetting()
    {
        string path = System.IO.Path.Combine(Application.dataPath, "../../", settingFile);
        setting = NotionSetting.CreateFromFile(path);
    }

    [ContextMenu("WriteSetting")]
    void WriteSetting()
    {
        string path = System.IO.Path.Combine(Application.dataPath, "../../", settingFile);
        NotionSetting.WriteToFile(setting, path);
    }

    // -----
    // UI
    private void Ui_Init()
    {
        notionCanvas.eventCreateBtnClick += () =>
        {
            _ = CreatePage(notionCanvas.PageTitleText);
        };
        notionCanvas.eventLoadDbBtnClick += () =>
        {
            _ = LoadDatabase();
        };
        //
        notionCanvas.eventOpenPage += (Result result) =>
        {
            _ = notionREST.RetrievePage(setting.token, result.id, (string str) =>
            {
                Debug.LogWarning("Do something >> " + str);
            });
        };
    }


    // -----
    // Notion API

    /// <summary>
    /// Pageの作成
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    async Task CreatePage(string text)
    {
        Page createPage = new Page();
        createPage.CreatePageData(setting.database_id, text);
        await notionREST.CreatePage(setting.token, createPage);
    }

    /// <summary>
    /// database の内容取得
    /// </summary>
    [SerializeField] ResultPage resultPage = null;
    async Task LoadDatabase()
    {
        resultPage = await notionREST.QueryDatabase(setting.token, setting.database_id);
        Debug.Log("resultPage: " + resultPage);

        notionCanvas.CreateResultList(resultPage);
    }

    //
    //[SerializeField] Rect drawRect = new Rect(10,10,200,200);
    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(drawRect);
    //    if (GUILayout.Button("CreatePage"))
    //    {
    //        _ = CreatePage();
    //    }
    //    if (GUILayout.Button("LoadDatabase"))
    //    {
    //        _ = LoadDatabase();
    //    }
    //    GUILayout.EndArea();
    //}
}
