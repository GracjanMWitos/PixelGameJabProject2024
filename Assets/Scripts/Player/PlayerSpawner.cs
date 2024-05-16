using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public GameObject SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity, transform);
        return player;
    }
}
