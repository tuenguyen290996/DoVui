using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;

    public Button btnComp;

    public void SetAnswerText(string content)
    {
        if (answerText)
        {
            answerText.text = content;
        }
    }
}
