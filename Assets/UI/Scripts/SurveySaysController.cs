using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SurveySaysController : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;
    public GameObject NextPage;

    Label m_Prompt;
    Button m_Submit;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var surveySaysListController = new SurveySaysListController();
        surveySaysListController.InitializeResponseList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }
}
