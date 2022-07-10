using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResponseListController
{
    List<ResponseData> m_SpectrumItems;
    VisualTreeAsset m_ListEntryTemplate;

    ListView m_ResponseList;
    ListView m_SpectrumList;

    public void InitializeResponseList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllResponses();

        m_ListEntryTemplate = listElementTemplate;

        m_ResponseList = root.Q<ListView>("ResponsesListView");
        m_SpectrumList = root.Q<ListView>("SpectrumListView");

        FillResponseLists();

        m_ResponseList.onSelectionChange += OnResponseSelected;
        m_SpectrumList.onSelectionChange += OnSpectrumSelected;
    }

    void EnumerateAllResponses()
    {

        m_SpectrumItems = new List<ResponseData>();

    }

    void FillResponseLists()
    {
        // Set up a make item function for a list entry
        m_ResponseList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new ResponseListItemController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_ResponseList.bindItem = (item, index) =>
        {

            var data = GameManager.Instance.ResponseDatasUnranked[index];
            data.Ranking = index;
            (item.userData as ResponseListItemController).SetData(data);
        };

        m_ResponseList.itemsSource = GameManager.Instance.ResponseDatasUnranked;

        /////////////////////////// spectrum /////////////////////

        // Set up a make item function for a list entry
        m_SpectrumList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new ResponseListItemController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script

            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_SpectrumList.bindItem = (item, index) =>
        {
            var data = m_SpectrumItems[index];
            data.Ranking = index;
            (item.userData as ResponseListItemController).SetData(data);
        };

        m_SpectrumList.itemsSource = m_SpectrumItems;
    }

    void OnResponseSelected(IEnumerable<object> selectedItems)
    {
        var selectedResponse = (ResponseData)m_ResponseList.selectedItem;
        // Move to Spectrum
        m_SpectrumItems.Add(selectedResponse);
        GameManager.Instance.ResponseDatasUnranked.Remove(selectedResponse);
        GameManager.Instance.ResponseDatasRanked[0 /*TODO CHANGE TO PLAYER ID*/] = m_SpectrumItems.ToArray();
        FillResponseLists();
    }

    void OnSpectrumSelected(IEnumerable<object> selectedItems)
    {
        var selectedResponse = (ResponseData)m_SpectrumList.selectedItem;

        // Move to Spectrum
        GameManager.Instance.ResponseDatasUnranked.Add(selectedResponse);
        m_SpectrumItems.Remove(selectedResponse);
        GameManager.Instance.ResponseDatasRanked[0 /*TODO CHANGE TO PLAYER ID*/] = m_SpectrumItems.ToArray();
        FillResponseLists();
    }
}
