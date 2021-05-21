using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Notion;

public class NotionCanvas : MonoBehaviour
{
    [SerializeField] Transform parentTrans = null;
    [SerializeField] List listPrefab = null;

    [SerializeField] Button loadDBBtn = null;
    [SerializeField] Button createBtn = null;
    [SerializeField] InputField createInputField = null;

    public delegate void BtnClickHandler();
    public event BtnClickHandler eventCreateBtnClick;
    public event BtnClickHandler eventLoadDbBtnClick;

    public delegate void OpenPageHandler(Result result);
    public event OpenPageHandler eventOpenPage;

    public string PageTitleText => createInputField.text;

    List<List> resultList = new List<List>();


    private void Awake()
    {
        createBtn.onClick.AddListener(() => {
            eventCreateBtnClick?.Invoke();
        });
        loadDBBtn.onClick.AddListener(() => {
            eventLoadDbBtnClick?.Invoke();
        });

    }


    public void CreateResultList(ResultPage result)
    {
        ClearResultList();

        if (resultList == null){
            resultList = new List<List>();
        }
        foreach (var r in result.results)
        {
            var list = GameObject.Instantiate<List>(listPrefab);
            list.transform.SetParent(parentTrans);

            list.SetData(r);
            list.eventOpenBtn += List_eventOpenBtn;

            resultList.Add(list);
        }
    }

    private void List_eventOpenBtn(Result result)
    {
        eventOpenPage?.Invoke(result);
    }

    public void ClearResultList()
    {
        if (resultList == null)
        {
            return;
        }
        foreach (var list in resultList)
        {
            list.eventOpenBtn -= List_eventOpenBtn;
            GameObject.Destroy(list.gameObject);
        }
        resultList.Clear();
    }
}
