using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnum : MonoBehaviour
{
    // 발판에 올라가 있는지 & 어떤 발판에 올라가 있는지 판단
    // 퀴즈 정답 체크했는지도 판단

    // 퀴즈 타이머 퀴즈 시간마다 정답을 체크 후 체크했는지를 초기화
    float currentTime = 0;
    public float quizTime;

    public enum TriggerType
    {
        None,
        O,
        X
    }

    public TriggerType currentTriggerType = QuizEnum.TriggerType.None;

    public bool onTrigger = false;      // 초기 상태는 발판 위에 올라가 있지 않다고 판단

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
