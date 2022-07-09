using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        PlayerLegacy playerLegacy = player.GetComponent<PlayerLegacy>();
        Color randomColor = Random.ColorHSV();
        playerLegacy.sprite.color = new Color(randomColor.r, randomColor.g, randomColor.b);
    }
}
