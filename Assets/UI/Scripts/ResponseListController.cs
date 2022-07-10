using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResponseListController
{
    List<ResponseItemDefinition> m_ResponseItems;
    List<ResponseItemDefinition> m_SpectrumItems;
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
        m_ResponseItems = new List<ResponseItemDefinition>();
        m_ResponseItems.AddRange(Resources.LoadAll<ResponseItemDefinition>("Responses"));
        Debug.Log("Length of response list: " + m_ResponseItems.Count);

        m_SpectrumItems = new List<ResponseItemDefinition>();

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
            m_ResponseItems[index].Ranking = index;
            (item.userData as ResponseListItemController).SetData(m_ResponseItems[index]);
        };

        m_ResponseList.itemsSource = m_ResponseItems;

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
            m_SpectrumItems[index].Ranking = index;
            (item.userData as ResponseListItemController).SetData(m_SpectrumItems[index]);
        };

        m_SpectrumList.itemsSource = m_SpectrumItems;
    }

    void OnResponseSelected(IEnumerable<object> selectedItems)
    {
        var selectedResponse = m_ResponseList.selectedItem as ResponseItemDefinition;
        if (selectedResponse == null)
        {
            //Probably nothing

        }
        else
        {
            // Move to Spectrum
            Debug.Log("Move to spectrum: " + selectedResponse + m_ResponseItems.ToString());
            m_SpectrumItems.Add(selectedResponse);
            m_ResponseItems.Remove(selectedResponse);
            FillResponseLists();
        }
    }

    void OnSpectrumSelected(IEnumerable<object> selectedItems)
    {
        var selectedResponse = m_SpectrumList.selectedItem as ResponseItemDefinition;
        if (selectedResponse == null)
        {
            //Probably nothing

        }
        else
        {
            // Move to Spectrum
            Debug.Log("Remove from spectrum");
            m_ResponseItems.Add(selectedResponse);
            m_SpectrumItems.Remove(selectedResponse);
            FillResponseLists();

        }
    }

    void AddToSpectrum(ResponseItemDefinition response)
    {

    }


}
