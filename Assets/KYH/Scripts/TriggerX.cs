using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerX : MonoBehaviour
{
    public QuizManagerKYH quizManagerKYH;
    public QuizEnum quizEnum;
    public TriggerO triO;
    public bool isOnX = false;
    

    private void Update()
    {
        if (quizEnum.onTrigger)   // 만약, 트리거에 올라가 있다면
        {
            if (isOnX == !quizManagerKYH.resData.answer)     // 퀴즈의 정답이 false이고 내가 X 발판 위에 있다면
            {
                quizManagerKYH.countReq.correct++;
                print("정답은 X 이고 맞췄습니다!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // 발판 위에 올라가면 발판 위에 있다고 판단하고 어떤 발판 위에 있는지 알려준다.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;
            triO.isOnO = false;
            isOnX = true;
        }
    }

    private void OnTriggerExit(Collider other)      // 발판에서 나오면 정답 유무 판별을 하지 않는다.
    {
        quizEnum.onTrigger = false;
    }
}
