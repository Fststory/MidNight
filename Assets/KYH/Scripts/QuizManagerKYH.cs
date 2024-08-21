using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http 통신을 위한 네임 스페이스
using UnityEngine.UI;

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

#region PUT / Count - 퀴즈에 대한 카운트를 업데이트
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
    // 서버에서 문제를 Get 하고, 플레이어가 문제를 풀면, 그에 대한 정보를 서버에 Put 하는 클래스

    public string getUrl;
    public string putUrl;
    public QuizRes resData;
    public CountReq countReq = new CountReq();      // 전달할 Request의 인스턴스 생성

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

    #region GetList() 연습
    //// 서버에서 OX퀴즈 목록을 GET하는 함수
    //public Text text_result;
    //public ListRes listResData;

    //public void GetList()
    //{
    //    //btn_getJson.interactable = false;
    //    StartCoroutine(GetQuizList(url));       // url에서 퀴즈 목록 Get 요청을 한다.
    //}

    //IEnumerator GetQuizList(string url)
    //{
    //    // 1. url로부터 Get으로 요청을 준비한다.
    //    UnityWebRequest request = UnityWebRequest.Get(url);

    //    // 2. 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
    //    yield return request.SendWebRequest();

    //    // 3. 만일, 응답이 성공이라면...
    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        // 4. 텍스트를 받는다. ListRes 내용을 Json(string) 형태로
    //        string result = request.downloadHandler.text;
    //        //string result = "{\"id\":1,\"quiz\":\"지구는 태양 주위를 돈다.\",\"answer\":true,\"comment\":\"해설\"}";
    //        // print(result); <= Json 형태 그대로 출력됨

    //        // 5. 응답 받은 json 데이터를 ListRes 구조체 형태로 인스턴스에 파싱한다.
    //        ListRes resData = JsonUtility.FromJson<ListRes>(result);
    //        // print(resData); <= ListRes 가 출력됨

    //        string resList = JsonUtility.ToJson(resData, true);
    //        // print(resList); <= value가 모두 초기값인 ListRes 형태가 출력됨.
    //    }

    //    //    // 6. 해당 인스턴스를 byte[]로 전환한다.
    //    //    //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
    //    //    byte[] binaries = Convert.FromBase64String(reqImageData.img);

    //    //    if (binaries.Length > 0)
    //    //    {
    //    //        Texture2D texture = new Texture2D(184, 273);

    //    //        // byte 배열로 된 raw 데이터를 텍스쳐 형태로 변환해서 texture2D 인스턴스로 변환한다.
    //    //        texture.LoadImage(binaries);
    //    //        img_response.texture = texture;

    //    //    }
    //    //}
    //    //// 그렇지 않다면...
    //    //else
    //    //{
    //    //    // 에러 내용을 text_response에 전달한다.
    //    //    text_response.text = request.responseCode + ": " + request.error;
    //    //    Debug.LogError(request.responseCode + ": " + request.error);
    //    //}
    //    //btn_getJson.interactable = true;
    //}
    #endregion

    #region GetQuiz()
    // 서버에서 *랜덤*한 퀴즈를 받아온다. 이후 화면에 받아온 문제를 띄우는 것까지 해야된다.
    public void GetQuiz()
    {
        answerTitle.color = new Color(0, 0, 0, 0);      // 문제를 출제할 때 정답과 해설의 포멧은 보이지 않는다.
        commentTitle.color = new Color(0, 0, 0, 0);
        answer.color = new Color(0, 0, 0, 0);
        comment.color = new Color(0, 0, 0, 0);
        StartCoroutine(GetRandomQuiz(getUrl));
    }

    IEnumerator GetRandomQuiz(string url)
    {
        // 서버에서 랜덤 문제 뿌려주기로 했음!!
        // 화면 출력은 UI 캔버스의 텍스트에 각각의 정보를 대입해주면 된다.

        // 1. url로부터 Get으로 요청을 준비한다.
        UnityWebRequest request = UnityWebRequest.Get(url);     // getUrl = http://172.16.16.81:8080/entity/quizzes/random

        // 2. 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
        yield return request.SendWebRequest();

        // 3. 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 4. 텍스트를 받는다. QuizRes 내용을 Json(string) 형태로
            string result = request.downloadHandler.text;

            // 5. 응답 받은 json 데이터를 QuizRes 구조체 형태로 인스턴스에 파싱한다.
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
            Invoke("OpenAnswer", 5.0f);     // 5초의 카운트 후 정답과 해설을 공개한다. **************************************************
        }
    }

    void OpenAnswer()       // 카운트가 끝나면 정답과 해설을 출력한다.
    {
        answerTitle.color = new Color(0, 0, 0, 1);
        commentTitle.color = new Color(0, 0, 0, 1);
        answer.color = new Color(0, 0, 0, 1);
        comment.color = new Color(0, 0, 0, 1);
        myAns.checkTime = true;
    }
    #endregion

    #region PutCount()
    // 서버에 현재까지의 퀴즈 진행도?를 전달(업데이트) 한다.
    public void PutCount()
    {
        StartCoroutine(PutQuizCount(putUrl));
    }

    IEnumerator PutQuizCount(string url)
    {
        // 1. 퀴즈 진행도를 Json 데이터로 변환하기

        // CountReq 인스턴스(countData)에 Put할 내용을 기록해야 됨
        countReq.id = resData.id;                                       // 문제의 번호
        /*countReq.correct =      ; */                                  // 문제를 맞춘 사람의 수 (MyAnswer.AnswerCheck()에서 판단해서 전달해 줌)
        countReq.players = players.playerObjects.Count;                 // 현재 플레이 중인 사람의 수

        string countJsonData = JsonUtility.ToJson(countReq, true);     // 인스턴스를 Json으로 바꾼다.

        print(countJsonData);

        // 2. Put를 하기 위한 준비를 한다.
        UnityWebRequest request = UnityWebRequest.Put(url + countReq.id, countJsonData);    // putUrl = http://172.16.16.81:8080/entity/quizzes/
        request.SetRequestHeader("Content-Type", "application/json");

        // 3. 서버에 Put를 전송하고 응답이 올 때까지 기다린다.
        yield return request.SendWebRequest();

        print(request.downloadHandler.text);
    }
    #endregion
}
