using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Notion
{
    // TODO: UnityのJsonUtilityだと記述が煩雑になるので再考する

    [System.Serializable]
    public class NotionSetting
    {
        public string token = "";
        public string database_id = "";

        public static NotionSetting CreateFromFile(string path)
        {
            string jsonStr = FileUtils.Read(path);
            return JsonUtility.FromJson<NotionSetting>(jsonStr);
        }
        public static void WriteToFile(NotionSetting setting, string path)
        {
            string jsonStr = JsonUtility.ToJson(setting, true);
            FileUtils.Write(path, jsonStr);
        }
    }


    //
    [System.Serializable]
    public class Page
    {
        public Parent parent;
        public Properties properties;

        //
        public void CreatePageData(string database_id, string text)
        {
            //Page createPage = new Page();
            this.properties = new Properties();
            this.properties.Name = new Name();
            Parent parent = new Parent();
            parent.database_id = database_id;
            this.parent = parent;
            Title[] titles = new Title[1];
            Title title = new Title();
            title.text = new Text();
            title.text.content = text;
            titles[0] = title;
            this.properties.Name.title = titles;
        }
    }

    [System.Serializable]
    public class Parent
    {
        public string database_id = "";
    }

    [System.Serializable]
    public class Properties
    {
        public Name Name;
    }
    [System.Serializable]
    public class Name
    {
        public Title[] title;
    }
    [System.Serializable]
    public class Title
    {
        public Text text;
    }
    [System.Serializable]
    public class Text
    {
        public string content = "content---";
    }
    //

    // -----
    //
    [System.Serializable]
    public class ResultPage
    {
        //public string object = "";
        public Result[] results = null;
        public bool next_cursor = false;
        public string has_more = "";
    }
    [System.Serializable]
    public class Result
    {
        //public string object = "";
        public string id = "";
        public string created_time = "";
        public string last_edited_time = "";
        public ResultProperties properties;
    }
    [System.Serializable]
    public class ResultProperties
    {
        public Result_Name Name;
    }
    [System.Serializable]
    public class Result_Name
    {
        public string id = "";
        public string type = "";
        public Result_Title[] title;
    }
    [System.Serializable]
    public class Result_Title
    {
        public string type = "";
        public Result_Text text = null;
    }
    [System.Serializable]
    public class Result_Text
    {
        public string content = "";
    }
}
