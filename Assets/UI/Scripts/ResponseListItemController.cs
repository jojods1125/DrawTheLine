using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ResponseListItemController
{

    Button mResponseButton;

    public void SetVisualElement(VisualElement visualElement)
    {
        mResponseButton = visualElement.Q<Button>("ResponseButton");
        mResponseButton.clicked += ResponseClicked;
    }

    public void SetData(ResponseItemDefinition response)
    {
        mResponseButton.text = response.Response;
    }

    public void ResponseClicked()
    {

    }
}
