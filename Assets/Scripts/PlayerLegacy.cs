using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerLegacy : MonoBehaviour
{
    public string name;
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

    public void SendAnswers(string firstAnswer, string secondAnswer)
    {
        view.RPC("RPC_SendAnswers", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void RPC_SendAnswers(string firstAnswer, string secondAnswer)
    {
        answers.Add(firstAnswer);
        answers.Add(secondAnswer);
    }
}
