using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealhPointController : MonoBehaviour
{

    public void SetHealthPointActivityState(bool activityState)
    {
        transform.GetChild(0).gameObject.SetActive(activityState);
    }
}
