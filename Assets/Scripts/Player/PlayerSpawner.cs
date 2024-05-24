using UnityEngine;
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParent;
    public GameObject SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity, playerParent);
        return player;
    }
}
