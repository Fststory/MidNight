using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http ����� ���� ���� �����̽�
using System.Text;      // json, csv���� ���� ������ ���ڵ�(UTF-8)�� ���� ���� �����̽�
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;
using static UnityEngine.UIElements.UxmlAttributeDescription;

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

#region POST / Count - ��� ���� ī��Ʈ�� ������Ʈ
public struct CountReq
{
    public int number;
    public int correct;
    public int players;
    public CountReq(int number, int correct, int players)
    {
        this.number = number;
        this.correct = correct;
        this.players = players;
    }
}
#endregion

public class QuizManagerKYH : MonoBehaviour
{
    public string url;
    public QuizRes resData;
    public CountReq countReq = new CountReq();      // ������ Request�� �ν��Ͻ� ����

    public FindPlayers players;

    public Text id, quiz, answer, comment;

    void Start()
    {
        //GetList();
        GetQuiz();
        Invoke("PostCount", 10.0f);
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
        StartCoroutine(GetRandomQuiz(url));
    }

    IEnumerator GetRandomQuiz(string url)       
    {
        /* ������ ���� �������� ��� �޾ƿñ�?
            1. �������� �޾ƿ� �������� ����Ʈ�� �ְ� ����Ʈ �ε����� �������� ��� ȭ�鿡 ���
            2. ���ʿ� �������� �������� �̾ƿ� �� �ִٸ�..?


            ȭ�� ����� UI ĵ������ �ؽ�Ʈ�� ������ ������ �������ָ� �ȴ�.
        */

        // 1. url�κ��� Get���� ��û�� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 2. �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
        yield return request.SendWebRequest();

        // 3. ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. �ؽ�Ʈ�� �޴´�. QuizRes ������ Json(string) ���·�
            string result = request.downloadHandler.text;

            // 5. ���� ���� json �����͸� ListRes ����ü ���·� �ν��Ͻ��� �Ľ��Ѵ�.
            QuizRes33 resData1 = JsonUtility.FromJson<QuizRes33>(result);
            resData = resData1.result.quiz;

            print("������ ��ȣ�� : " + resData.id);
            print("������ ������ : " + resData.quiz);
            print("������ ������ : " + resData.answer);
            print("������ �ؼ��� : " + resData.comment);
            id.text = resData.id.ToString();
            quiz.text = resData.quiz;
            answer.text = resData.answer.ToString();
            comment.text = resData.comment;
        }
    }
    #endregion

    #region PostCount()
    // ������ ��������� ���� ���൵?�� ����(������Ʈ) �Ѵ�.
    public void PostCount()
    {
        StartCoroutine(PostQuizCount(url));
    }

    IEnumerator PostQuizCount(string url)
    {
        // 1. ���� ���൵�� Json �����ͷ� ��ȯ�ϱ�

        // CountReq �ν��Ͻ�(countData)�� Post�� ������ ����ؾ� ��
        countReq.number = resData.id;                                       // ������ ��ȣ
        /*countReq.correct =      ; */                                      // ������ ���� ����� �� (Trigger���� �Ǵ��ؼ� ������ ��)
        countReq.players = players.playerObjects.Count;                     // ���� �÷��� ���� ����� ��

        string countJsonData = JsonUtility.ToJson(countReq, true);     // �ν��Ͻ��� Json���� �ٲ۴�.

        // 2. Post�� �ϱ� ���� �غ� �Ѵ�.
        UnityWebRequest request = UnityWebRequest.Post(url, countJsonData, "application/json");

        // 3. ������ Post�� �����ϰ� ������ �� ������ ��ٸ���.
        yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.Success)
        //{
        //    // �ٿ�ε� �ڵ鷯���� �ؽ�Ʈ ���� �޾Ƽ� UI�� ����Ѵ�.
        //    string response = request.downloadHandler.text;
        //    text_response.text = response;
        //    Debug.LogWarning(response);
        //}
        //else
        //{
        //    text_response.text = request.error;
        //    Debug.LogError(request.error);
        //}
    }

    #endregion
}
