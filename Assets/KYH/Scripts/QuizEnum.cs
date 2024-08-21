using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnum : MonoBehaviour
{
    // ���ǿ� �ö� �ִ��� & � ���ǿ� �ö� �ִ��� �Ǵ�
    // ���� ���� üũ�ߴ����� �Ǵ�

    public enum TriggerType
    {
        None,
        O,
        X
    }

    public TriggerType currentTriggerType = QuizEnum.TriggerType.None;

    public bool onTrigger = false;      // �ʱ� ���´� ���� ���� �ö� ���� �ʴٰ� �Ǵ�

    public bool answerCheck = false;
}
