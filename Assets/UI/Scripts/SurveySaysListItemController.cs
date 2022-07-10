using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class SurveySaysListItemController
{

    Label mResponseLabel;
    Label mNumberLabel;
    Color white = new Color(1f, 1f,1f);
    Color yellow = new Color(1f, 0.9490196f, 0.8235294f);

    public void SetVisualElement(VisualElement visualElement)
    {
        mResponseLabel = visualElement.Q<Label>("ResponseLabel");
    }

    public void SetData(ResponseData response)
    {
        mResponseLabel.text = response.Response;
    }
}
