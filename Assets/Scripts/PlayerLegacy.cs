using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerLegacy : MonoBehaviour
{
    public string playerName;
    //public Image icon;
    public SpriteRenderer sprite;

    // Input Fields and Button

    List<string> answers = new List<string>();

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            // Update our answers Text
        }
    }

    
}
