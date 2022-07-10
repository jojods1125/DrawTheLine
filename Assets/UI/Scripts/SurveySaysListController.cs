using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SurveySaysListController
{

    VisualTreeAsset m_ListEntryTemplate;

    ListView m_ResponseList;
    GameManager gm;

    public void InitializeResponseList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        gm = GameManager.Instance;
        m_ListEntryTemplate = listElementTemplate;

        m_ResponseList = root.Q<ListView>("ResultsList");

        FillResponseLists();
    }

    void FillResponseLists()
    {
        // Set up a make item function for a list entry
        m_ResponseList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new SurveySaysListItemController();

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
            Debug.Log("index: " + index);
            //Debug.Log("index: " +  index + " " + gm.AverageResponseDatas[0] + " " + gm.AverageResponseDatas[1].ToString() + " " + gm.AverageResponseDatas[2].ToString());
            //var data = gm.AverageResponseDatas[index];
            //data.Ranking = index;
            //(item.userData as SurveySaysListItemController).SetData(data);
        };

        m_ResponseList.itemsSource = gm.AverageResponseDatas;
    }

}
