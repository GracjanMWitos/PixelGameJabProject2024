using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    private static GUIManager _instance;
    public static GUIManager Instance { get { return _instance; } }

    [SerializeField] private Transform healthPointsParent;

    private UIHealhPointController[] healhPointControllers;

    void Awake()
    {
        #region Instance check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }
    void Start()
    {
        healhPointControllers = healthPointsParent.GetComponentsInChildren<UIHealhPointController>();
    }

    public void LowerNumberOfHealthPointsInUI(int currentHealthPointsCount, int numberOfHealthPointsSubtract)
    {
        for (int i = currentHealthPointsCount + numberOfHealthPointsSubtract - 1; i > currentHealthPointsCount - 1; i--) // Disactivate points from max (10) to current points count
        {
            healhPointControllers[i].SetHealthPointActivityState(false);
        }
    }
    public void RaiseNumberOfHealthPointsInUI(int currentHealthPointsCount, int numberOfHealthPoints)
    {
        for (int i = currentHealthPointsCount - numberOfHealthPoints; i <= currentHealthPointsCount - 1; i++) // Disactivate points from max (10) to current points count
        {
            healhPointControllers[i].SetHealthPointActivityState(true);
        }
    }
}
