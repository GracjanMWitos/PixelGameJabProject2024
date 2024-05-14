using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void Start()
    {
        GameManager.Instance.player = Instantiate(player, transform.position, Quaternion.identity, transform);
        GameManager.Instance.StartLevel();
    }
}
