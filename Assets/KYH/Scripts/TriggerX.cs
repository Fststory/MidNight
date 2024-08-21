using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerX : MonoBehaviour
{
    public QuizManagerKYH quizManagerKYH;
    public QuizEnum quizEnum;
    public TriggerO triO;
    public bool isOnX = false;

    void Update()
    {
        if (!quizEnum.answerCheck)      // 만약, 정답 체크를 안 했다면 트리거에 있는지 판단한다.
        {
            if (quizEnum.onTrigger)   // 만약, 트리거에 올라가 있다면
            {
                if (quizEnum.currentTriggerType == QuizEnum.TriggerType.X && quizManagerKYH.resData.answer == false)     // 퀴즈의 정답이 true이고 내가 O 발판 위에 있다면
                {
                    quizManagerKYH.countReq.correct++;      // 맞춘 수를 +1 한다.
                    print("정답은 X 이고 맞췄습니다!");
                    quizEnum.shopManager.playerPoints += 50;
                }
                quizEnum.answerCheck = true;            // 정답 체크를 했다고 표시한다.
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // 발판 위에 올라가면 발판 위에 있다고 판단하고 어떤 발판 위에 있는지 알려준다.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;      // 트리거에 올라와 있다고 체크
            quizEnum.currentTriggerType = QuizEnum.TriggerType.X;       // O 트리거에 올라와 있다고 체크
        }
    }

    private void OnTriggerExit(Collider other)      // 발판에서 나오면 정답 유무 판별을 하지 않는다.
    {
        quizEnum.onTrigger = false;     // 트리거에서 내려왔다고 체크
        quizEnum.currentTriggerType = QuizEnum.TriggerType.None;       // 아무 트리거에도 올라와 있지 않다고 체크
    }
}
