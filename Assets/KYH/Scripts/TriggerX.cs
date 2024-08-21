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
        if (quizEnum.onTrigger)   // ����, Ʈ���ſ� �ö� �ִٸ�
        {
            if (isOnX == !quizManagerKYH.resData.answer)     // ������ ������ false�̰� ���� X ���� ���� �ִٸ�
            {
                quizManagerKYH.countReq.correct++;
                print("������ X �̰� ������ϴ�!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)     // ���� ���� �ö󰡸� ���� ���� �ִٰ� �Ǵ��ϰ� � ���� ���� �ִ��� �˷��ش�.
    {
        if (other.CompareTag("Player"))
        {
            quizEnum.onTrigger = true;
            triO.isOnO = false;
            isOnX = true;
        }
    }

    private void OnTriggerExit(Collider other)      // ���ǿ��� ������ ���� ���� �Ǻ��� ���� �ʴ´�.
    {
        quizEnum.onTrigger = false;
    }
}
