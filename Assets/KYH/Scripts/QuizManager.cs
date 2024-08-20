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
public struct QuizRes   // ����
{
    public int idx;
    public string quiz;
    public bool answer;
    public string comment;
    public QuizRes(int idx, string quiz, bool answer, string comment)
    {
        this.idx = idx;
        this.quiz = quiz;
        this.answer = answer;
        this.comment = comment;
    }
}
#endregion

#region POST / Count - ��� ���� ī��Ʈ�� ������Ʈ
public struct CountReq      // �ҷ�����
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

public class QuizManager : MonoBehaviour
{
    public string url;
    

    void Start()
    {
        //GetList();
        GetQuiz();
    }

    #region GetList() ����
    // �������� OX���� ����� GET�ϴ� �Լ�
    public Text text_result;
    public ListRes listResData;

    public void GetList()
    {
        //btn_getJson.interactable = false;
        StartCoroutine(GetQuizList(url));       // url���� ���� ��� Get ��û�� �Ѵ�.
    }

    IEnumerator GetQuizList(string url)
    {
        // 1. url�κ��� Get���� ��û�� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 2. �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
        yield return request.SendWebRequest();

        // 3. ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. �ؽ�Ʈ�� �޴´�. ListRes ������ Json(string) ���·�
            string result = request.downloadHandler.text;
            //string result = "{\"id\":1,\"quiz\":\"������ �¾� ������ ����.\",\"answer\":true,\"comment\":\"�ؼ�\"}";
            // print(result); <= Json ���� �״�� ��µ�

            // 5. ���� ���� json �����͸� ListRes ����ü ���·� �ν��Ͻ��� �Ľ��Ѵ�.
            ListRes resData = JsonUtility.FromJson<ListRes>(result);
            // print(resData); <= ListRes �� ��µ�

            string resList = JsonUtility.ToJson(resData, true);
            // print(resList); <= value�� ��� �ʱⰪ�� ListRes ���°� ��µ�.
        }

        //    // 6. �ش� �ν��Ͻ��� byte[]�� ��ȯ�Ѵ�.
        //    //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
        //    byte[] binaries = Convert.FromBase64String(reqImageData.img);

        //    if (binaries.Length > 0)
        //    {
        //        Texture2D texture = new Texture2D(184, 273);

        //        // byte �迭�� �� raw �����͸� �ؽ��� ���·� ��ȯ�ؼ� texture2D �ν��Ͻ��� ��ȯ�Ѵ�.
        //        texture.LoadImage(binaries);
        //        img_response.texture = texture;

        //    }
        //}
        //// �׷��� �ʴٸ�...
        //else
        //{
        //    // ���� ������ text_response�� �����Ѵ�.
        //    text_response.text = request.responseCode + ": " + request.error;
        //    Debug.LogError(request.responseCode + ": " + request.error);
        //}
        //btn_getJson.interactable = true;
    }
    #endregion

    void GetQuiz()
    {
        StartCoroutine(GetRandomQuiz(url));
    }

    IEnumerator GetRandomQuiz(string url)
    {
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
            QuizRes resData = JsonUtility.FromJson<QuizRes>(result);

            print("������ ��ȣ�� : " + resData.idx);
            print("������ ������ : " + resData.quiz);
            print("������ ������ : " + resData.answer);
            print("������ �ؼ��� : " + resData.comment);
        }
    }

    // text �����͸� ���Ϸ� �����ϱ�
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. ���� ��Ʈ���� ���� ���·� ����.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);

        // 2. ��Ʈ���� json �����͸� ����� �����Ѵ�.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. ��Ʈ���� �ݾ��ش�.
        fs.Close();
    }

    // text ������ �о����
    public string ReadJsonData(string path, string fileName)
    {
        string readText;
        string fullPath = Path.Combine(path, fileName);

        // ���� ó�� : �ش� ��ο� ���� ������ �����ϴ����� ���� Ȯ���Ѵ�.
        bool isDirectoryExist = Directory.Exists(path);

        if (isDirectoryExist)
        {
            bool isFileExist = File.Exists(fullPath);

            if (isFileExist)
            {
                // 1. ���� ��Ʈ���� �б� ���� ����.
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                // 2. ��Ʈ�����κ��� ������(byte)�� �о�´�.
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                readText = sr.ReadToEnd();
            }
            else
            {
                readText = "�׷� ���� �����!";
            }
        }
        else
        {
            readText = "�׷� ��� �����!";
        }

        // 3. ���� �����͸� string���� ��ȯ�ؼ� ��ȯ�Ѵ�.
        return readText;
    }
}
