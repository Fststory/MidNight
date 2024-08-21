using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;  // 퀴즈 UI가 포함된 Panel
    public Button startQuizButton; // 퀴즈 시작 버튼

    void Start()
    {
        // 처음에는 퀴즈 UI를 숨깁니다.
        quizPanel.SetActive(false);

        // 버튼에 클릭 이벤트를 연결합니다.
        startQuizButton.onClick.AddListener(ShowQuizUI);
    }

    void ShowQuizUI()
    {
        // 퀴즈 UI를 활성화하여 표시합니다.
        quizPanel.SetActive(true);
    }
}