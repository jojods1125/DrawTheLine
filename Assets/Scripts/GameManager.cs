using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public Dictionary<int, string> PlayerNames = new();

    public bool isHost;

    public float TimerDuration = 3f;

    public static GameManager Instance { get; private set; }

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

    public void StartCinematic(float waitTime, string cineTitle)
    {
        _coroutine = CinematicDelay(waitTime, cineTitle);
        StartCoroutine(_coroutine);
    }

    private IEnumerator CinematicDelay(float waitTime, string cineTitle)
    {
        yield return new WaitForSeconds(waitTime);
        print("Cinematic Ended: " + cineTitle);
        GameFSM.Instance.DBG_CineEnd();
    }

    public void StartTimer(float waitTime)
    {
        _coroutine = TimerDelay(waitTime);
        StartCoroutine(_coroutine);
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
        }
    }

    public void CreateRankedSpectrum()
    {
        ResponsesRanked.OrderBy(x => x.Value).Select(x => x.Key);
        RankedSpectrum = ResponsesRanked.Keys.ToArray();
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
