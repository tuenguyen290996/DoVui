using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Ins;

    public Text timeText;

    public Text questionText;

    public Dialog dialog;

    public AnswerButton[] answerButtons;

    private void Awake()
    {
        MakeSingleton();
    }

 

    public void SetTimeText(string content)
    {
        if (timeText)
        {
            timeText.text = content;
        }

    }

    public void SetQuestionText(string content)
    {
        if (questionText)
            questionText.text = content;
    }

    public void ShuffleAnswers()
    {
        if(answerButtons !=null && answerButtons.Length > 0)
        {
            for (int i = 0 ; i < answerButtons.Length; i++)
            {
                if (answerButtons[i])
                {
                    answerButtons[i].tag = "Untagged";
                }
            }

            int randIdx = Random.Range(0, answerButtons.Length);

            if (answerButtons[randIdx])
            {
                answerButtons[randIdx].tag = "RightAnswer";
            }
        }
    }

    public void MakeSingleton()
    {
        if(Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
