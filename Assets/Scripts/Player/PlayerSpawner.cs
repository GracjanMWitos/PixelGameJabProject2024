using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    private void Start()
    {
        GameObject spawnedPlayer = Instantiate(player, transform.position, Quaternion.identity, transform);
        GameManager.Instance.player = spawnedPlayer;
        cinemachine.Follow = spawnedPlayer.transform;
        GameManager.Instance.StartLevel();
    }
}
