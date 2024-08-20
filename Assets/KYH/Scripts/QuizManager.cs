using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http 통신을 위한 네임 스페이스
using System.Text;      // json, csv같은 문서 형태의 인코딩(UTF-8)을 위한 네임 스페이스
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;
using static UnityEngine.UIElements.UxmlAttributeDescription;

// Request: 내가 주는 Json / Response: 내가 받아 오는 Json

#region Get / List - OX퀴즈 목록 리턴

[System.Serializable]
public struct ListRes   // 불러오기
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

// Json 배열
[System.Serializable]
public struct ListResDataList
{
    public List<ListRes> ListResDatas;
}

#endregion

#region POST / Add - 질문을 DB에 추가
public struct AddReq    // 저장
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

public struct AddRes    // 불러오기
{
    public bool ok;
    public AddRes(bool ok)
    {
        this.ok = ok;
    }
}
#endregion

#region POST / Ai - 해설 작성할 때 AI 어시스턴트에 필요
public struct AiReq     // 불러오기
{
    public string comment;
    public AiReq(string comment)
    {
        this.comment = comment;
    }
}

public struct AiRes     // 저장
{
    public string message;
    public AiRes(string message)
    {
        this.message = message;
    }
}
#endregion

#region GET / Quiz - 유니티에서 랜덤 퀴즈를 받아오는 라우트
// Param: ?time=<unix timestamp>
public struct QuizRes   // 저장
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

#region POST / Count - 퀴즈에 대한 카운트를 업데이트
public struct CountReq      // 불러오기
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

    #region GetList() 연습
    // 서버에서 OX퀴즈 목록을 GET하는 함수
    public Text text_result;
    public ListRes listResData;

    public void GetList()
    {
        //btn_getJson.interactable = false;
        StartCoroutine(GetQuizList(url));       // url에서 퀴즈 목록 Get 요청을 한다.
    }

    IEnumerator GetQuizList(string url)
    {
        // 1. url로부터 Get으로 요청을 준비한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 2. 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
        yield return request.SendWebRequest();

        // 3. 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. 텍스트를 받는다. ListRes 내용을 Json(string) 형태로
            string result = request.downloadHandler.text;
            //string result = "{\"id\":1,\"quiz\":\"지구는 태양 주위를 돈다.\",\"answer\":true,\"comment\":\"해설\"}";
            // print(result); <= Json 형태 그대로 출력됨

            // 5. 응답 받은 json 데이터를 ListRes 구조체 형태로 인스턴스에 파싱한다.
            ListRes resData = JsonUtility.FromJson<ListRes>(result);
            // print(resData); <= ListRes 가 출력됨

            string resList = JsonUtility.ToJson(resData, true);
            // print(resList); <= value가 모두 초기값인 ListRes 형태가 출력됨.
        }

        //    // 6. 해당 인스턴스를 byte[]로 전환한다.
        //    //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
        //    byte[] binaries = Convert.FromBase64String(reqImageData.img);

        //    if (binaries.Length > 0)
        //    {
        //        Texture2D texture = new Texture2D(184, 273);

        //        // byte 배열로 된 raw 데이터를 텍스쳐 형태로 변환해서 texture2D 인스턴스로 변환한다.
        //        texture.LoadImage(binaries);
        //        img_response.texture = texture;

        //    }
        //}
        //// 그렇지 않다면...
        //else
        //{
        //    // 에러 내용을 text_response에 전달한다.
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
        // 1. url로부터 Get으로 요청을 준비한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 2. 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
        yield return request.SendWebRequest();

        // 3. 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. 텍스트를 받는다. QuizRes 내용을 Json(string) 형태로
            string result = request.downloadHandler.text;

            // 5. 응답 받은 json 데이터를 ListRes 구조체 형태로 인스턴스에 파싱한다.
            QuizRes resData = JsonUtility.FromJson<QuizRes>(result);

            print("퀴즈의 번호는 : " + resData.idx);
            print("퀴즈의 내용은 : " + resData.quiz);
            print("퀴즈의 정답은 : " + resData.answer);
            print("퀴즈의 해설은 : " + resData.comment);
        }
    }

    // text 데이터를 파일로 저장하기
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. 파일 스트림을 쓰기 형태로 연다.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);

        // 2. 스트림에 json 데이터를 쓰기로 전달한다.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. 스트림을 닫아준다.
        fs.Close();
    }

    // text 파일을 읽어오기
    public string ReadJsonData(string path, string fileName)
    {
        string readText;
        string fullPath = Path.Combine(path, fileName);

        // 예외 처리 : 해당 경로에 값이 파일이 존재하는지를 먼저 확인한다.
        bool isDirectoryExist = Directory.Exists(path);

        if (isDirectoryExist)
        {
            bool isFileExist = File.Exists(fullPath);

            if (isFileExist)
            {
                // 1. 파일 스트림을 읽기 모드로 연다.
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                // 2. 스트림으로부터 데이터(byte)를 읽어온다.
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                readText = sr.ReadToEnd();
            }
            else
            {
                readText = "그런 파일 없어욧!";
            }
        }
        else
        {
            readText = "그런 경로 없어욧!";
        }

        // 3. 읽은 데이터를 string으로 변환해서 반환한다.
        return readText;
    }
}
