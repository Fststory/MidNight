using UnityEngine;

public class MyAnswer : MonoBehaviour
{
    // ���� O�� ���ִ��� X�� ���ִ��� �Ǵ��ϴ� Ŭ����

    public QuizManagerKYH quizManagerKYH;       // ���� ������ ���ϱ� ���� ����

    public ShopManager shopManager;     // ShopManager�� �޾ƿͼ� ������ ���߸� ����Ʈ�� ������Ų��.

    public bool checkTime = false;

    public enum MyAnswerType
    {
        None,
        O,
        X
    }

    public MyAnswerType myAnswerType = MyAnswer.MyAnswerType.None;

    public bool isOn = false;   // Ʈ���ſ� �ö�� �ִ���


    private void Update()
    {
        if (checkTime)
        {
            AnswerCheck();
        }
    }

    public void AnswerCheck()
    {
        checkTime = false;

        if (isOn)   // ���� Ʈ���ſ� �ö�� �ִٸ�..
        {
            if (quizManagerKYH.resData.answer == true)    // ������ ������ O ���..
            {
                if (myAnswerType == MyAnswer.MyAnswerType.O)    // ���� ������ ���� O ���..
                {
                    quizManagerKYH.countReq.correct++;      // ���� ���� +1 �Ѵ�.
                    shopManager.playerPoints += 50;         // ��ȭ 50 ȹ��!
                    print("������ O �̰� ������ϴ�!");
                }
            }
            else if (quizManagerKYH.resData.answer == false)  // ������ ������ X ���..
            {
                if (myAnswerType == MyAnswer.MyAnswerType.X)    // ���� ������ ���� X ���..
                {
                    quizManagerKYH.countReq.correct++;      // ���� ���� +1 �Ѵ�.
                    shopManager.playerPoints += 50;         // ��ȭ 50 ȹ��!
                    print("������ X �̰� ������ϴ�!");
                }
            }
        }
        quizManagerKYH.PutCount();      // ä�� �� ���� DB�� Put Count �Ѵ�.
        Invoke("NextQuiz", 5.0f);
    }

    void NextQuiz()
    {
        quizManagerKYH.GetQuiz();       // �̾ ���� ������ �޾ƿ´�.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerO"))
        {
            myAnswerType = MyAnswer.MyAnswerType.O;
            isOn = true;      // Ʈ���ſ� �ö�� �ִٰ� üũ
        }
        else if (other.CompareTag("TriggerX"))
        {
            myAnswerType = MyAnswer.MyAnswerType.X;
            isOn = true;      // Ʈ���ſ� �ö�� �ִٰ� üũ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerO") || other.CompareTag("TriggerX"))
        {
            myAnswerType = MyAnswer.MyAnswerType.None;
            isOn = false;
        }
    }
}
