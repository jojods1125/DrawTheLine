using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawLineListController
{
    List<ResponseItemDefinition> m_ResponseItems;

    VisualTreeAsset m_ListEntryTemplate;

    ListView m_ResponseList;

    public void InitializeResponseList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllResponses();

        m_ListEntryTemplate = listElementTemplate;

        m_ResponseList = root.Q<ListView>("LineList");

        FillResponseLists();

        m_ResponseList.onSelectionChange += OnResponseSelected;
    }

    void EnumerateAllResponses()
    {
        m_ResponseItems = new List<ResponseItemDefinition>();
        m_ResponseItems.AddRange(Resources.LoadAll<ResponseItemDefinition>("Responses/Average"));
        Debug.Log("Length of response list: " + m_ResponseItems.Count);
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
            m_ResponseItems[index].Ranking = index;
            (item.userData as DrawLineListItemController).SetData(m_ResponseItems[index]);
        };

        m_ResponseList.itemsSource = m_ResponseItems;
    }

    void OnResponseSelected(IEnumerable<object> selectedItems)
    {
        
        var selectedResponse = m_ResponseList.selectedItem as ResponseItemDefinition;
        
        if (!selectedResponse.LineDrawnAfter)
        {
            selectedResponse.LineDrawnAfter = true;
            foreach (ResponseItemDefinition response in m_ResponseItems)
            {
                if (response != selectedResponse)
                {
                    response.LineDrawnAfter = false;
                }
            }
        }
        Debug.Log("Clicked: " + selectedResponse.Ranking + " " + selectedResponse.LineDrawnAfter);
        m_ResponseList.Rebuild();

    }

}
