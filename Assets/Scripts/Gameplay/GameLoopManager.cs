using System;
using UnityEngine;
using Zenject;
public class GameLoopManager : MonoBehaviour
{
    [SerializeField] private float _gameDuration = 30f;

    [Inject] private IScoreRepository _scoreRepository;

    public int CurrentScore{get; private set;}
    public float RemainingTime{get; private set;}
    public bool IsPlaying{get; private set;}

    //UI連携用のイベント
    public event Action<int> OnScoreChanged;
    public event Action<float> OnTimeChanged;
    public event Action OnGameOver;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        CurrentScore = 0;
        RemainingTime = _gameDuration;
        IsPlaying = true;
        OnScoreChanged?.Invoke(CurrentScore);
        OnTimeChanged?.Invoke(RemainingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsPlaying) return;

        RemainingTime -= Time.deltaTime;
        if(RemainingTime <= 0f)
        {
            RemainingTime = 0f;
            EndGame();
        }
        OnTimeChanged?.Invoke(RemainingTime);
    }

    public void AddScore(int amount)
    {
        if(!IsPlaying) return;
        CurrentScore += amount;
        OnScoreChanged?.Invoke(CurrentScore);
        Debug.Log($"<color=yellow>[Score]</color> {CurrentScore} (+{amount})");
    }

    private void EndGame()
    {
        IsPlaying = false;
        OnGameOver?.Invoke();

        //IScoreRepository をここで呼び出して保存！
        _scoreRepository.SaveHighScore(CurrentScore);
        Debug.Log($"<color=red>[GameOver]</color> 最終スコア: {CurrentScore} (ハイスコア: {_scoreRepository.GetHighScore()})");
    }
}
