using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Notion
{
    /// <summary>
    /// Notion の database の項目リスト
    /// </summary>
    public class List : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Text titleText = null;
        [SerializeField] UnityEngine.UI.Text idText = null;

        [SerializeField] Button openBtn = null;

        public delegate void OpenBtnHandler(Result result);
        public event OpenBtnHandler eventOpenBtn;

        Result result = null;

        private void Awake()
        {
            openBtn.onClick.AddListener(() =>
            {
                eventOpenBtn?.Invoke(result);
            });
        }

        public void SetData(Result result_)
        {
            result = result_;

            idText.text = result.id;
            titleText.text = result.properties.Name.title[0].text.content;
        }
    }
}