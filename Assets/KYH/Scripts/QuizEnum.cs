using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnum : MonoBehaviour
{
    // 발판에 올라가 있는지 & 어떤 발판에 올라가 있는지 판단
    // 퀴즈 정답 체크했는지도 판단

    public enum TriggerType
    {
        None,
        O,
        X
    }

    public TriggerType currentTriggerType = QuizEnum.TriggerType.None;

    public bool onTrigger = false;      // 초기 상태는 발판 위에 올라가 있지 않다고 판단

    public bool answerCheck = false;
}
