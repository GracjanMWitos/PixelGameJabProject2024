using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    private static GUIManager _instance;
    public static GUIManager Instance { get { return _instance; } }

    [SerializeField] private GameObject healthPointsParent;
    [SerializeField] private GameObject noteControllersParent;
    [SerializeField] private GameObject spacingIndicationParent;

    private UIHealhPointController[] healhPointControllers;
    public NoteController[] noteControllers;
    [SerializeField] private Transform[] spacingIndications;

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
        #region Assigning to arrays
        healhPointControllers = healthPointsParent.GetComponentsInChildren<UIHealhPointController>();
        noteControllers = noteControllersParent.GetComponentsInChildren<NoteController>();
        spacingIndications = new Transform[spacingIndicationParent.transform.childCount];
        for (int i = 0; i < spacingIndicationParent.transform.childCount; i++)
            spacingIndications[i] = spacingIndicationParent.transform.GetChild(i).transform;
        #endregion

        SetStartPositionForNote();
    }
    public Transform GetNoteWithIndexZero()
    {
        foreach (NoteController noteController in noteControllers)
        {
            if (noteController.spacingIndicationsArrayIndex == 0)
            {
                return noteController.transform;
            }

        }
        return null;
    }

    #region HP in GUI
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
    #endregion
    #region Notes movement
    public void SetStartPositionForNote()
    {
        for (int i = 0; i < noteControllers.Length; i++)
        {
            noteControllers[i].previousTargetTransform = spacingIndications[i + 1].transform;
            noteControllers[i].transform.position = noteControllers[i].previousTargetTransform.position;
            noteControllers[i].spacingIndicationsArrayIndex = i;
        }
    }
    public void MoveNotesToNextTarget()
    {
        for (int i = 0; i < noteControllers.Length; i++)
        {
            int index = noteControllers[i].spacingIndicationsArrayIndex;
            if (index == 0)
            {
                noteControllers[i].StartFromBeginning(spacingIndications[spacingIndications.Length - 1], spacingIndications[spacingIndications.Length - 2]);
                noteControllers[i].spacingIndicationsArrayIndex = spacingIndications.Length - 2;
            }
            else
            {
                noteControllers[i].SelectNextTarget(spacingIndications[index - 1].transform);
                noteControllers[i].spacingIndicationsArrayIndex = noteControllers[i].spacingIndicationsArrayIndex - 1;
            }
        }
    }
    #endregion
}
