using UnityEngine;

public class MyAnswer : MonoBehaviour
{
    // 내가 O에 서있는지 X에 서있는지 판단하는 클래스

    public QuizManagerKYH quizManagerKYH;       // 퀴즈 정답을 비교하기 위해 선언

    public ShopManager shopManager;     // ShopManager를 받아와서 문제를 맞추면 포인트를 누적시킨다.

    public bool checkTime = false;

    public enum MyAnswerType
    {
        None,
        O,
        X
    }

    public MyAnswerType myAnswerType = MyAnswer.MyAnswerType.None;

    public bool isOn = false;   // 트리거에 올라와 있는지


    private void Update()
    {
        if (checkTime)
        {
            AnswerCheck();
        }
    }

    public void AnswerCheck()
    {
        checkTime = false;

        if (isOn)   // 발판 트리거에 올라와 있다면..
        {
            if (quizManagerKYH.resData.answer == true)    // 퀴즈의 정답이 O 라면..
            {
                if (myAnswerType == MyAnswer.MyAnswerType.O)    // 내가 제출한 답이 O 라면..
                {
                    quizManagerKYH.countReq.correct++;      // 맞춘 수를 +1 한다.
                    shopManager.playerPoints += 50;         // 재화 50 획득!
                    print("정답은 O 이고 맞췄습니다!");
                }
            }
            else if (quizManagerKYH.resData.answer == false)  // 퀴즈의 정답이 X 라면..
            {
                if (myAnswerType == MyAnswer.MyAnswerType.X)    // 내가 제출한 답이 X 라면..
                {
                    quizManagerKYH.countReq.correct++;      // 맞춘 수를 +1 한다.
                    shopManager.playerPoints += 50;         // 재화 50 획득!
                    print("정답은 X 이고 맞췄습니다!");
                }
            }
        }
        quizManagerKYH.PutCount();      // 채점 후 서버 DB에 Put Count 한다.
        Invoke("NextQuiz", 5.0f);
    }

    void NextQuiz()
    {
        quizManagerKYH.GetQuiz();       // 이어서 다음 문제를 받아온다.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerO"))
        {
            myAnswerType = MyAnswer.MyAnswerType.O;
            isOn = true;      // 트리거에 올라와 있다고 체크
        }
        else if (other.CompareTag("TriggerX"))
        {
            myAnswerType = MyAnswer.MyAnswerType.X;
            isOn = true;      // 트리거에 올라와 있다고 체크
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerO") || other.CompareTag("TriggerX"))
        {
            myAnswerType = MyAnswer.MyAnswerType.None;
            isOn = false;
        }
    }
}
