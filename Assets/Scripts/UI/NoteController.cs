using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Transform previousTargetTransform;
    public Transform currentTargetTransform;
    public int spacingIndicationsArrayIndex;

    private void MoveNote()
    {
        StartCoroutine(Extns.Tweeng(GameManager.Instance.timeBetweenHalfbeats, 
            (p) => transform.position = p, 
            previousTargetTransform.position, 
            currentTargetTransform.position)
            );
    }
    public void SelectNextTarget(Transform targetTransform)
    {
        previousTargetTransform = currentTargetTransform;
        currentTargetTransform = targetTransform;
        if (previousTargetTransform != null && currentTargetTransform != null)
        {
            MoveNote();
        }
    }
    public void StartFromBeginning(Transform beginningTargetTransform, Transform nextTargetTransform)
    {
        previousTargetTransform = beginningTargetTransform;
        currentTargetTransform = nextTargetTransform;
        if (previousTargetTransform != null && currentTargetTransform != null)
        {
            MoveNote();
        }
    }

}
