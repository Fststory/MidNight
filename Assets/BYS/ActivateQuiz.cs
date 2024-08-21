using UnityEngine;

public class ActivateQuiz : MonoBehaviour
{
    public GameObject quizPanel;  // 퀴즈 창 Panel

    public void ShowQuizPanel()  // 함수가 클래스 내부에 선언되어야 합니다.
    {
        // quizPanel이 null이 아닌지 확인
        if (quizPanel != null)
        {
            quizPanel.SetActive(true);  // 퀴즈 창을 활성화
        }
        else
        {
            Debug.LogError("Quiz Panel이 할당되지 않았습니다!");
        }
    }
}