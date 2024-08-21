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
        if (!quizEnum.answerCheck)      // ����, ���� üũ�� �� �ߴٸ� Ʈ���ſ� �ִ��� �Ǵ��Ѵ�.
        {
            if (quizEnum.onTrigger)   // ����, Ʈ���ſ� �ö� �ִٸ�
            {
                if (quizEnum.currentTriggerType == QuizEnum.TriggerType.X && quizManagerKYH.resData.answer == false)     // ������ ������ true�̰� ���� O ���� ���� �ִٸ�
                {
                    quizManagerKYH.countReq.correct++;      // ���� ���� +1 �Ѵ�.
                    print("������ X �̰� ������ϴ�!");
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
            quizEnum.currentTriggerType = QuizEnum.TriggerType.X;       // O Ʈ���ſ� �ö�� �ִٰ� üũ
        }
    }

    private void OnTriggerExit(Collider other)      // ���ǿ��� ������ ���� ���� �Ǻ��� ���� �ʴ´�.
    {
        quizEnum.onTrigger = false;     // Ʈ���ſ��� �����Դٰ� üũ
        quizEnum.currentTriggerType = QuizEnum.TriggerType.None;       // �ƹ� Ʈ���ſ��� �ö�� ���� �ʴٰ� üũ
    }
}
