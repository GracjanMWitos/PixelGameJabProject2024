using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelFinishingSequence : MonoBehaviour
{
    [SerializeField] private bool CameraFollowsPlayer = true;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;

    private Animator animator;
    private GetGridTile getGridTile = new GetGridTile();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (CameraFollowsPlayer == false)
        {
            cinemachineVirtual.Follow = null;
        }
        if (getGridTile.GetTile(transform.position) == GameManager.Instance.currentPlayerTile)
        {
            animator.SetTrigger("Finish");
        }
    }
}

