using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timePerQuestion;
    float m_curTime;

    int m_rightCount;

    private void Awake()
    {
        m_curTime = timePerQuestion;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateQuestion();

        StartCoroutine(TimeCountingDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateQuestion()
    {
        QuestionData qs = QuestionManager.Ins.GetRandomQuestion();

        if(qs != null)
        {
            UIManager.Ins.SetQuestionText(qs.question);

            string[] wrongAnswers = new string[] { qs.answerA, qs.answerB, qs.answerC };

            UIManager.Ins.ShuffleAnswers();

            var temp = UIManager.Ins.answerButtons;

            if(temp != null && temp.Length > 0)
            {
                int wrongAnswerCount = 0;

                for (int i = 0; i < temp.Length; i++)
                {
                    int answerID = i;

                    if(string.Compare(temp[i].tag, "RightAnswer") == 0)
                    {
                        temp[i].SetAnswerText(qs.rightAnswer);
                    }
                    else
                    {
                        temp[i].SetAnswerText(wrongAnswers[wrongAnswerCount]);
                        wrongAnswerCount++;
                    }

                    temp[answerID].btnComp.onClick.RemoveAllListeners();
                    temp[answerID].btnComp.onClick.AddListener(() => CheckRightAnswerEvent(temp[answerID]));
                }
            }
        }
    }

    void CheckRightAnswerEvent(AnswerButton answerButton)
    {
        if (answerButton.CompareTag("RightAnswer"))
        {

            m_curTime = timePerQuestion;
            UIManager.Ins.SetTimeText("00: " + m_curTime);

            m_rightCount++;

            if (m_rightCount == (QuestionManager.Ins.questions.Length))
            {
                UIManager.Ins.dialog.SetDialogContent("Bạn đã chiến thắng!");
                UIManager.Ins.dialog.Show(true);
                AudioController.Ins.PlayWinSound();
                StopAllCoroutines();
            }
            else
            {             
                CreateQuestion();
                AudioController.Ins.PlayRightSound();
            }           
        }
        else
        {
            UIManager.Ins.dialog.SetDialogContent("Bạn đã thua, trò chơi kết thúc!");
            UIManager.Ins.dialog.Show(true);
            AudioController.Ins.PlayLoseSound();
            Debug.Log("Ban da tra loi sai, Tro choi ket thuc");
        }
    }

    IEnumerator TimeCountingDown()
    {
        yield return new WaitForSeconds(1);
        if (m_curTime > 0)
        {
            m_curTime--;
            StartCoroutine(TimeCountingDown());
            UIManager.Ins.SetTimeText("00: " + m_curTime);
        }
        else
        {
            UIManager.Ins.dialog.SetDialogContent("Đã hết thời gian. Bạn đã thua, trò chơi kết thúc!");
            UIManager.Ins.dialog.Show(true);
            StopAllCoroutines();
            AudioController.Ins.PlayLoseSound();
        }    
    }
    public void Replay()
    {
        AudioController.Ins.StopMusic();
        SceneManager.LoadScene("Gameplay");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
