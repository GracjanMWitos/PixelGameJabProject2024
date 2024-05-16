using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TextBox
{
    public string discription;
    public string textToDisplay;
}
public class DisplayText : MonoBehaviour
{
    [SerializeField] List<TextBox> textBox = new List<TextBox>();
    private TextMeshProUGUI textMeshPro;
    private int index = -1;;
    public bool nextText;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (nextText)
        {
            NextText();
            
        }
    }
    public void NextText()
    {

        if (index < textBox.Count - 1)
        {
            index++;
        }
        else
        {
            Debug.Log("That was last text box");
        }
        textMeshPro.text = textBox[index].textToDisplay;
        nextText = false;
    }
}
