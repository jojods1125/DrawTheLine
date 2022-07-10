using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private IEnumerator _coroutine;

    [HideInInspector]
    public NetworkManager NetworkManager;

    public List<ResponseData> ResponseDatasUnranked = new List<ResponseData>(); // Collection of responses from players
    public Dictionary<int, ResponseData[]> ResponseDatasRanked = new Dictionary<int, ResponseData[]>(); // Collection of rankings from players
    public Dictionary<string, int> ResponsesRanked = new(); // The combined ranking of the responses based on user rankings
    public string[] RankedSpectrum;
    public List<ResponseData> AverageResponseDatas = new List<ResponseData>();
    public ResponseData[] RankedSpectrumData;

    public Dictionary<int, string> PlayerNames = new();

    public bool isHost = true;

    public float TimerDuration = 3f;

    public static GameManager Instance { get; private set; }

    //SOUND STUFF
    sndManager soundmanager;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        soundmanager = FindObjectOfType<sndManager>().GetComponent<sndManager>();
        isHost = true;

        soundmanager.PlayMusic(sndManager.MUS.Lobby);
        //GameManager.Instance.StartCinematic(2f, "Lobby Cinematic");
    }

    public void StartCinematic(float waitTime, string cineTitle)
    {
        switch (cineTitle)
        {
            //case "Lobby Cinematic":
            //    soundmanager.PlayMusic(sndManager.MUS.Lobby);
            //    waitTime = 0f;
            //    break;
            case "Current Scores Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.Host);
                switch (GameFSM.Instance.CurrentRound)
                {
                    case 1:
                        soundmanager.PlayVO(sndManager.VO.Scores1b);
                        waitTime = 5f;
                        break;
                    case 2:
                        soundmanager.PlayVO(sndManager.VO.Scores2b);
                        waitTime = 5.5f;
                        break;
                    case 3:
                        //do nothing
                        break;
                }
                break;
            case "Final Scores Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.FinalScores);
                soundmanager.PlayVO(sndManager.VO.Scores3b);
                waitTime = 11f;
                break;
            case "Intro Cinematic":
                soundmanager.StopMusic(); //stop music on the clients now that isHost is updated
                soundmanager.PlayMusic(sndManager.MUS.Lobby);
                soundmanager.PlayVO(sndManager.VO.IntroNotPlayed);
                waitTime = 60f + 53f;
                break;
            case "Post Answers Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.Host);
                soundmanager.PlayVO(sndManager.VO.Part1Finish);
                waitTime = 9.5f;
                break;
            case "Prompt Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.Host);
                switch (GameFSM.Instance.CurrentRound)
                {
                    case 1:
                        soundmanager.PlayVO(sndManager.VO.Part1Start1);
                        waitTime = 5f;
                        break;
                    case 2:
                        soundmanager.PlayVO(sndManager.VO.Part1Start2);
                        waitTime = 4f;
                        break;
                    case 3:
                        soundmanager.PlayVO(sndManager.VO.Part1Start3);
                        waitTime = 5f;
                        break;
                }
                break;
            case "Results Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.Host);
                soundmanager.PlayVO(sndManager.VO.Part3Finish);
                waitTime = 13f;
                break;
            case "Spectrum Cinematic":
                soundmanager.PlayMusic(sndManager.MUS.Host);
                soundmanager.PlayVO(sndManager.VO.Part2Finish);
                waitTime = 9.5f;
                break;
        }

        _coroutine = CinematicDelay(waitTime, cineTitle);
        StartCoroutine(_coroutine);

    }

    private IEnumerator CinematicDelay(float waitTime, string cineTitle)
    {
        yield return new WaitForSeconds(waitTime);
        print("Cinematic Delayed: " + cineTitle);

        //theres no delay after the cinematic ends
        switch (cineTitle)
        {
            case "Current Scores Cinematic":
                switch (GameFSM.Instance.CurrentRound)
                {
                    case 1:
                        soundmanager.PlayVO(sndManager.VO.Scores1c);
                        waitTime = 5f;
                        break;
                    case 2:
                        soundmanager.PlayVO(sndManager.VO.Scores2c);
                        waitTime = 4.5f;
                        break;
                    case 3:
                        //do nothing
                        break;
                }
                break;
            case "Final Scores Cinematic":
                soundmanager.PlayVO(sndManager.VO.PostGame);
                waitTime = 0f; //go directly to next state
                break;
            case "Intro Cinematic":
                //do nothing when the intro ends
                break;
            case "Post Answers Cinematic":
                soundmanager.PlayVO(sndManager.VO.Part2Start);
                waitTime = 6f;
                break;
            case "Prompt Cinematic":
                soundmanager.PlayVO(sndManager.VO.Part1After);
                waitTime = 4.5f;
                break;
            case "Results Cinematic":
                switch (GameFSM.Instance.CurrentRound)
                {
                    case 1:
                        soundmanager.PlayVO(sndManager.VO.Scores1a);
                        waitTime = 4f;
                        break;
                    case 2:
                        soundmanager.PlayVO(sndManager.VO.Scores2a);
                        waitTime = 5f;
                        break;
                    case 3:
                        soundmanager.PlayVO(sndManager.VO.Scores3a);
                        waitTime = 7.5f;
                        break;
                }
                break;
            case "Spectrum Cinematic":
                soundmanager.PlayVO(sndManager.VO.Part3Start);
                waitTime = 10f;
                break;
        }

        _coroutine = CinematicEnd(waitTime, cineTitle);
        StartCoroutine(_coroutine);
    }

    private IEnumerator CinematicEnd(float waitTime, string cineTitle)
    {
        yield return new WaitForSeconds(waitTime);
        print("Cinematic Ended: " + cineTitle);
        GameFSM.Instance.DBG_CineEnd();
    }

    public void StartTimer(float waitTime)
    {
        _coroutine = TimerDelay(waitTime);
        StartCoroutine(_coroutine);

        soundmanager.PlayMusic(sndManager.MUS.Waiting);
    }

    public void CancelTimer()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator TimerDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("Timer Activated");
        GameFSM.Instance.DBG_TimerEnd();

        soundmanager.PlaySFX(sndManager.SFX.TimerEnd);
    }

    public string GeneratePrompt()
    {
        return "Prompt";
    }

    public string[] RetrieveResponses()
    {
        string[] responses = new string[GameFSM.Instance.NumPlayers * 2];
        int index = 0;

        foreach (ResponseData data in ResponseDatasUnranked)
        {
            responses[index] = data.Response;
            index++;
        }
        
        return responses;
    }

    public int[] RetrieveResponseIds()
    {
        int[] ids = new int[GameFSM.Instance.NumPlayers * 2];
        int index = 0;

        foreach (ResponseData data in ResponseDatasUnranked)
        {
            ids[index] = data.CreatorPlayerId;
            index++;
        }

        return ids;
    }

    public void CreateResponseData(string response, int creatorPlayerId)
    {
        ResponseData newData = new ResponseData { CreatorPlayerId = creatorPlayerId, CreatorNickname = LookUpPlayerName(creatorPlayerId), Response = response };
        ResponseDatasUnranked.Add(newData);
    }

    public string LookUpPlayerName(int creatorPlayerId)
    {
        return "Namey Name";
    }

    public void AddPlayerRanking(string[] ranking, int creatorPlayerId)
    {

    }

    public void ClearRoundData()
    {
        NetworkManager.aggregateLinePos.Clear();
        NetworkManager.aggregateRankings.Clear();
        ResponseDatasUnranked.Clear();
        ResponseDatasRanked.Clear();
        ResponsesRanked.Clear();
        Array.Clear(RankedSpectrum, 0, RankedSpectrum.Length);
        AverageResponseDatas.Clear();
        Array.Clear(RankedSpectrumData, 0, RankedSpectrumData.Length);
    }

    public void CollectRanking(string[] ranking)
    {
        int index = 0;
        foreach (string response in ranking)
        {
            if (ResponsesRanked.ContainsKey(response))
            {
                ResponsesRanked[response] += index;
            }
            else
            {
                ResponsesRanked.Add(response, index);
            }

            Debug.Log("COLLECT RANKING: " + response + " " + ResponsesRanked[response]);

            index++;
        }
    }

    public void CreateRankedSpectrum()
    {
        RankedSpectrum = ResponsesRanked.OrderBy(x => x.Value).Select(x => x.Key).ToArray();

        CreateRankedSpectrumData();
    }

    public void CreateRankedSpectrumData()
    {
        RankedSpectrumData = new ResponseData[RankedSpectrum.Length];
        int index = 0;
        foreach (string response in RankedSpectrum)
        {
            ResponseData newData = new ResponseData { Response = response, LineDrawnAfter = false };
            RankedSpectrumData[index] = newData;

            Debug.Log("CREATE RANKED SPEC DATA: " + response);

            index++;
        }
    }

}

public struct ResponseData
{
    public int CreatorPlayerId;
    public string CreatorNickname;
    public string Response;
    public int Ranking;
    public bool LineDrawnAfter;
}
