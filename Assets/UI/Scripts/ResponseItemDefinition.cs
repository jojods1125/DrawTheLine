using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Response", menuName = "Data/Item")]
public class ResponseItemDefinition : ScriptableObject
{
    public int CreatorPlayerId;
    public string CreatorNickname;
    public string Response;
    public int Ranking;
}
