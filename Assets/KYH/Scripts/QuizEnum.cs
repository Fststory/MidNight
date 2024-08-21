using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnum : MonoBehaviour
{
    // ���ǿ� �ö� �ִ��� & � ���ǿ� �ö� �ִ��� �Ǵ�
    // ���� ���� üũ�ߴ����� �Ǵ�

    public ShopManager shopManager;     // ShopManager�� �޾ƿͼ� ������ ���߸� ����Ʈ�� ������Ų��.

    // ���� Ÿ�̸� ���� �ð����� ������ üũ �� üũ�ߴ����� �ʱ�ȭ
    public float currentTime = 0;
    public float quizTime;

    public enum TriggerType
    {
        None,
        O,
        X
    }

    public TriggerType currentTriggerType = QuizEnum.TriggerType.None;

    public bool onTrigger = false;      // �ʱ� ���´� ���� ���� �ö� ���� �ʴٰ� �Ǵ�

    public bool answerCheck = false;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > quizTime)
        {
            answerCheck = false;
            currentTime = 0;
        }
    }
}
