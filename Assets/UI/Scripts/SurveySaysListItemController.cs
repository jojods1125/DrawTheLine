using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SurveySaysListItemController
{

    Label mResponseLabel;
    VisualElement m_Line;
    public void SetVisualElement(VisualElement visualElement)
    {
        mResponseLabel = visualElement.Q<Label>("ResponseLabel");
    }

    public void SetData(ResponseData response)
    {
        mResponseLabel.text = response.Response;

        if (response.LineDrawnAfter)
        {
            m_Line.style.backgroundColor = new Color(1.0f, 0f, 0f);
        }

    }
}
