using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawLineListController
{

    VisualTreeAsset m_ListEntryTemplate;

    ListView m_ResponseList;

    private GameManager gm;

    public void InitializeResponseList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        gm = GameManager.Instance;

        m_ListEntryTemplate = listElementTemplate;

        m_ResponseList = root.Q<ListView>("LineList");

        Debug.Log("m_ResponseList: " + m_ResponseList.ToString());

        FillResponseLists();

        m_ResponseList.onSelectionChange += OnResponseSelected;
    }

    void FillResponseLists()
    {
        // Set up a make item function for a list entry
        m_ResponseList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new DrawLineListItemController();

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
            var data = gm.ResponseDatasRanked[gm.NetworkManager.playerID][index];
            (item.userData as DrawLineListItemController).SetData(data);
        };

        m_ResponseList.itemsSource = gm.ResponseDatasRanked[gm.NetworkManager.playerID];
    }

    void OnResponseSelected(IEnumerable<object> selectedItems)
    {
        
        var selectedResponse = (ResponseData)m_ResponseList.selectedItem;
        
        if (!selectedResponse.LineDrawnAfter)
        {
            selectedResponse.LineDrawnAfter = true;
            for(int i = 0; i < gm.ResponseDatasRanked[gm.NetworkManager.playerID].Length; i++)
            {
                gm.ResponseDatasRanked[gm.NetworkManager.playerID][i].LineDrawnAfter = false;
            }
        }
        Debug.Log("Clicked: " + selectedResponse.Ranking + " " + selectedResponse.LineDrawnAfter);
        m_ResponseList.Rebuild();

    }

}
