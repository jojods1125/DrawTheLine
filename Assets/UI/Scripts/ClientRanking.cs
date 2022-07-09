using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClientRanking : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;


    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var ResponseListController = new ResponseListController();
        ResponseListController.InitializeResponseList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }



}
