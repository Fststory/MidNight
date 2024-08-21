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
        if (!quizEnum.answerCheck)      // ����, ���� üũ�� �� �ߴٸ� Ʈ���ſ� �ִ��� �Ǵ��Ѵ�.
        {
            if (quizEnum.onTrigger)   // ����, Ʈ���ſ� �ö� �ִٸ�
            {
                if (quizEnum.currentTriggerType == QuizEnum.TriggerType.O && quizManagerKYH.resData.answer == true)     // ������ ������ true�̰� ���� O ���� ���� �ִٸ�
                {
                    quizManagerKYH.countReq.correct++;      // ���� ���� +1 �Ѵ�.
                    print("������ O �̰� ������ϴ�!");
                    quizEnum.shopManager.playerPoints += 50;
                }
                quizEnum.answerCheck = true;            // ���� üũ�� �ߴٰ� ǥ���Ѵ�.
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // ���� ���� �ö󰡸� ���� ���� �ִٰ� �Ǵ��ϰ� � ���� ���� �ִ��� �˷��ش�.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;      // Ʈ���ſ� �ö�� �ִٰ� üũ
            quizEnum.currentTriggerType = QuizEnum.TriggerType.O;       // O Ʈ���ſ� �ö�� �ִٰ� üũ
        }
    }

    private void OnTriggerExit(Collider other)      // ���ǿ��� ������ ���� ���� �Ǻ��� ���� �ʴ´�.
    {
        quizEnum.onTrigger = false;     // Ʈ���ſ��� �����Դٰ� üũ
        quizEnum.currentTriggerType = QuizEnum.TriggerType.None;       // �ƹ� Ʈ���ſ��� �ö�� ���� �ʴٰ� üũ
    }
}
