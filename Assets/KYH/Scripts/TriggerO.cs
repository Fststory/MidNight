using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerO : MonoBehaviour
{
    // O 발판에 올라갈 떄 체크해주는 클래스

    public QuizManagerKYH quizManagerKYH;       // 퀴즈 정보를 비교하기 위해 선언
    public QuizEnum quizEnum;                   // 어떤 발판에 올라가 있는지 판단
    public bool isOnO = false;


    void Update()
    {
        if (!quizEnum.answerCheck)      // 만약, 정답 체크를 안 했다면
        {
            if (quizEnum.onTrigger)   // 만약, 트리거에 올라가 있다면
            {
                if (quizEnum.currentTriggerType == QuizEnum.TriggerType.O && quizManagerKYH.resData.answer)     // 퀴즈의 정답이 true이고 내가 O 발판 위에 있다면
                {
                    quizManagerKYH.countReq.correct++;
                    print("정답은 O 이고 맞췄습니다!");
                    quizEnum.answerCheck = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // 발판 위에 올라가면 발판 위에 있다고 판단하고 어떤 발판 위에 있는지 알려준다.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;
            isOnO = true;
        }
    }

    private void OnTriggerExit(Collider other)      // 발판에서 나오면 정답 유무 판별을 하지 않는다.
    {
        quizEnum.onTrigger = false;
    }
}
