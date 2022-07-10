using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ResponseListItemController
{

    Label mResponseLabel;
    Label mNumberLabel;
    Color white = new Color(1f, 1f,1f);
    Color yellow = new Color(1f, 0.9490196f, 0.8235294f);

    public void SetVisualElement(VisualElement visualElement)
    {
        mResponseLabel = visualElement.Q<Label>("ResponseLabel");
        mNumberLabel = visualElement.Q<Label>("Number");
    }

    public void SetData(ResponseItemDefinition response)
    {
        if(response.Ranking % 2 == 0)
        {
            //Set yellow
            mNumberLabel.style.backgroundColor = yellow;
            mResponseLabel.style.backgroundColor = yellow;
        }
        else
        {
            //Set white
            mNumberLabel.style.backgroundColor = white;
            mResponseLabel.style.backgroundColor = white;
        }
        mResponseLabel.text = response.Response;
        mNumberLabel.text = response.Ranking.ToString();
    }

    public void ResponseClicked()
    {

    }
}
