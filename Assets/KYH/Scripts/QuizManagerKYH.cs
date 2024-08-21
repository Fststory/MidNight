using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http ����� ���� ���� �����̽�
using UnityEngine.UI;

// Request: ���� �ִ� Json / Response: ���� �޾� ���� Json

#region Get / List - OX���� ��� ����

[System.Serializable]
public struct ListRes   // �ҷ�����
{
    public int id;
    public string quiz;
    public bool answer;
    public string comment;
    public int correctCount;
    public int totalCount;

    public ListRes(int id, string quiz, bool answer, string comment, int correctCount, int totalCount)
    {
        this.id = id;
        this.quiz = quiz;
        this.answer = answer;
        this.comment = comment;
        this.correctCount = correctCount;
        this.totalCount = totalCount;
    }
}

// Json �迭
[System.Serializable]
public struct ListResDataList
{
    public List<ListRes> ListResDatas;
}

#endregion

#region POST / Add - ������ DB�� �߰�
public struct AddReq    // ����
{
    public string quiz;
    public bool answer;
    public string comment;
    public AddReq(string quiz, bool answer, string comment)
    {
        this.quiz = quiz;
        this.answer = answer;
        this.comment = comment;
    }
}

public struct AddRes    // �ҷ�����
{
    public bool ok;
    public AddRes(bool ok)
    {
        this.ok = ok;
    }
}
#endregion

#region POST / Ai - �ؼ� �ۼ��� �� AI ��ý���Ʈ�� �ʿ�
public struct AiReq     // �ҷ�����
{
    public string comment;
    public AiReq(string comment)
    {
        this.comment = comment;
    }
}

public struct AiRes     // ����
{
    public string message;
    public AiRes(string message)
    {
        this.message = message;
    }
}
#endregion

#region GET / Quiz - ����Ƽ���� ���� ��� �޾ƿ��� ���Ʈ
// Param: ?time=<unix timestamp>
//public struct QuizRes
//{
//    public int id;
//    public string quiz;
//    public bool answer;
//    public string comment;
//    public QuizRes(int id, string quiz, bool answer, string comment)
//    {
//        this.id = id;
//        this.quiz = quiz;
//        this.answer = answer;
//        this.comment = comment;
//    }
//}

//public struct QuizRes33
//{
//    public int httpStatus;
//    public string message;
//    public sy result;
//    public QuizRes33(int httpStatus, string message, QuizRes result)
//    {
//        this.httpStatus = httpStatus;
//        this.message = message;
//        this.result = result;
//    }
//}

[System.Serializable]
public class QuizRes
{
    public int id;
    public string quiz;
    public bool answer;
    public string comment;
}

[System.Serializable]
public class QuizRes222
{
    public QuizRes quiz;
}

[System.Serializable]
public class QuizRes33
{
    public int httpStatus;
    public string message;
    public QuizRes222 result;
}
#endregion

#region PUT / Count - ��� ���� ī��Ʈ�� ������Ʈ
public struct CountReq
{
    public int id;
    public int correct;
    public int players;
    public CountReq(int id, int correct, int players)
    {
        this.id = id;
        this.correct = correct;
        this.players = players;
    }
}
#endregion

public class QuizManagerKYH : MonoBehaviour
{
    // �������� ������ Get �ϰ�, �÷��̾ ������ Ǯ��, �׿� ���� ������ ������ Put �ϴ� Ŭ����

    public string getUrl;
    public string putUrl;
    public QuizRes resData;
    public CountReq countReq = new CountReq();      // ������ Request�� �ν��Ͻ� ����

    public MyAnswer myAns;

    public FindPlayers players;

    public Text quiz, answer, comment, answerTitle, commentTitle, timer;

    bool timerStart = false;
    public float currentTime = 5.0f;

    void Start()
    {
        GetQuiz();
    }

    private void Update()
    {
        if (timerStart)
        {
            currentTime -= Time.deltaTime;
            timer.text = Mathf.Max(Mathf.FloorToInt(currentTime) + 1, 0).ToString();

            if (currentTime <= 0.0f)
            {
                timerStart = false;
                currentTime = 5.0f;
            }
        }
    }

    #region GetList() ����
    //// �������� OX���� ����� GET�ϴ� �Լ�
    //public Text text_result;
    //public ListRes listResData;

    //public void GetList()
    //{
    //    //btn_getJson.interactable = false;
    //    StartCoroutine(GetQuizList(url));       // url���� ���� ��� Get ��û�� �Ѵ�.
    //}

    //IEnumerator GetQuizList(string url)
    //{
    //    // 1. url�κ��� Get���� ��û�� �غ��Ѵ�.
    //    UnityWebRequest request = UnityWebRequest.Get(url);

    //    // 2. �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
    //    yield return request.SendWebRequest();

    //    // 3. ����, ������ �����̶��...
    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        // 4. �ؽ�Ʈ�� �޴´�. ListRes ������ Json(string) ���·�
    //        string result = request.downloadHandler.text;
    //        //string result = "{\"id\":1,\"quiz\":\"������ �¾� ������ ����.\",\"answer\":true,\"comment\":\"�ؼ�\"}";
    //        // print(result); <= Json ���� �״�� ��µ�

    //        // 5. ���� ���� json �����͸� ListRes ����ü ���·� �ν��Ͻ��� �Ľ��Ѵ�.
    //        ListRes resData = JsonUtility.FromJson<ListRes>(result);
    //        // print(resData); <= ListRes �� ��µ�

    //        string resList = JsonUtility.ToJson(resData, true);
    //        // print(resList); <= value�� ��� �ʱⰪ�� ListRes ���°� ��µ�.
    //    }

    //    //    // 6. �ش� �ν��Ͻ��� byte[]�� ��ȯ�Ѵ�.
    //    //    //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
    //    //    byte[] binaries = Convert.FromBase64String(reqImageData.img);

    //    //    if (binaries.Length > 0)
    //    //    {
    //    //        Texture2D texture = new Texture2D(184, 273);

    //    //        // byte �迭�� �� raw �����͸� �ؽ��� ���·� ��ȯ�ؼ� texture2D �ν��Ͻ��� ��ȯ�Ѵ�.
    //    //        texture.LoadImage(binaries);
    //    //        img_response.texture = texture;

    //    //    }
    //    //}
    //    //// �׷��� �ʴٸ�...
    //    //else
    //    //{
    //    //    // ���� ������ text_response�� �����Ѵ�.
    //    //    text_response.text = request.responseCode + ": " + request.error;
    //    //    Debug.LogError(request.responseCode + ": " + request.error);
    //    //}
    //    //btn_getJson.interactable = true;
    //}
    #endregion

    #region GetQuiz()
    // �������� *����*�� ��� �޾ƿ´�. ���� ȭ�鿡 �޾ƿ� ������ ���� �ͱ��� �ؾߵȴ�.
    public void GetQuiz()
    {
        answerTitle.color = new Color(0, 0, 0, 0);      // ������ ������ �� ����� �ؼ��� ������ ������ �ʴ´�.
        commentTitle.color = new Color(0, 0, 0, 0);
        answer.color = new Color(0, 0, 0, 0);
        comment.color = new Color(0, 0, 0, 0);
        StartCoroutine(GetRandomQuiz(getUrl));
    }

    IEnumerator GetRandomQuiz(string url)
    {
        // �������� ���� ���� �ѷ��ֱ�� ����!!
        // ȭ�� ����� UI ĵ������ �ؽ�Ʈ�� ������ ������ �������ָ� �ȴ�.

        // 1. url�κ��� Get���� ��û�� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);     // getUrl = http://172.16.16.81:8080/entity/quizzes/random

        // 2. �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
        yield return request.SendWebRequest();

        // 3. ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. �ؽ�Ʈ�� �޴´�. QuizRes ������ Json(string) ���·�
            string result = request.downloadHandler.text;

            // 5. ���� ���� json �����͸� QuizRes ����ü ���·� �ν��Ͻ��� �Ľ��Ѵ�.
            QuizRes33 resData1 = JsonUtility.FromJson<QuizRes33>(result);
            resData = resData1.result.quiz;

            quiz.text = resData.quiz;
            if (resData.answer)
            {
                answer.text = "O";
            }
            else if (!resData.answer)
            {
                answer.text = "X";
            }
            //answer.text = resData.answer.ToString();
            comment.text = resData.comment;

            timerStart = true;
            Invoke("OpenAnswer", 5.0f);     // 5���� ī��Ʈ �� ����� �ؼ��� �����Ѵ�. **************************************************
        }
    }

    void OpenAnswer()       // ī��Ʈ�� ������ ����� �ؼ��� ����Ѵ�.
    {
        answerTitle.color = new Color(0, 0, 0, 1);
        commentTitle.color = new Color(0, 0, 0, 1);
        answer.color = new Color(0, 0, 0, 1);
        comment.color = new Color(0, 0, 0, 1);
        myAns.checkTime = true;
    }
    #endregion

    #region PutCount()
    // ������ ��������� ���� ���൵?�� ����(������Ʈ) �Ѵ�.
    public void PutCount()
    {
        StartCoroutine(PutQuizCount(putUrl));
    }

    IEnumerator PutQuizCount(string url)
    {
        // 1. ���� ���൵�� Json �����ͷ� ��ȯ�ϱ�

        // CountReq �ν��Ͻ�(countData)�� Put�� ������ ����ؾ� ��
        countReq.id = resData.id;                                       // ������ ��ȣ
        /*countReq.correct =      ; */                                  // ������ ���� ����� �� (MyAnswer.AnswerCheck()���� �Ǵ��ؼ� ������ ��)
        countReq.players = players.playerObjects.Count;                 // ���� �÷��� ���� ����� ��

        string countJsonData = JsonUtility.ToJson(countReq, true);     // �ν��Ͻ��� Json���� �ٲ۴�.

        print(countJsonData);

        // 2. Put�� �ϱ� ���� �غ� �Ѵ�.
        UnityWebRequest request = UnityWebRequest.Put(url + countReq.id, countJsonData);    // putUrl = http://172.16.16.81:8080/entity/quizzes/
        request.SetRequestHeader("Content-Type", "application/json");

        // 3. ������ Put�� �����ϰ� ������ �� ������ ��ٸ���.
        yield return request.SendWebRequest();

        print(request.downloadHandler.text);
    }
    #endregion
}
