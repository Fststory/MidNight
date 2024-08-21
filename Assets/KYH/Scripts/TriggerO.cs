using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerO : MonoBehaviour
{
    // O ���ǿ� �ö� �� üũ���ִ� Ŭ����

    public QuizManagerKYH quizManagerKYH;       // ���� ������ ���ϱ� ���� ����
    public QuizEnum quizEnum;                   // � ���ǿ� �ö� �ִ��� �Ǵ�
    public bool isOnO = false;


    void Update()
    {
        if (!quizEnum.answerCheck)      // ����, ���� üũ�� �� �ߴٸ�
        {
            if (quizEnum.onTrigger)   // ����, Ʈ���ſ� �ö� �ִٸ�
            {
                if (quizEnum.currentTriggerType == QuizEnum.TriggerType.O && quizManagerKYH.resData.answer)     // ������ ������ true�̰� ���� O ���� ���� �ִٸ�
                {
                    quizManagerKYH.countReq.correct++;
                    print("������ O �̰� ������ϴ�!");
                    quizEnum.answerCheck = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // ���� ���� �ö󰡸� ���� ���� �ִٰ� �Ǵ��ϰ� � ���� ���� �ִ��� �˷��ش�.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;
            isOnO = true;
        }
    }

    private void OnTriggerExit(Collider other)      // ���ǿ��� ������ ���� ���� �Ǻ��� ���� �ʴ´�.
    {
        quizEnum.onTrigger = false;
    }
}
