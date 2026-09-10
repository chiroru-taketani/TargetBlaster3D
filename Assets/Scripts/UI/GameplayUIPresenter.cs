using TMPro;
using UnityEngine;
using Zenject;

public class GameplayUIPresenter : MonoBehaviour
{
    // ① インスペクターで画面部品（View）をセットする欄
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;

     [Header("Game Over UI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

 // ② ★★★ ここが奇跡の1行（別シーンからの注入） ★★★
    [Inject] private GameLoopManager _gameLoopManager;
  
    void Start()
    {
        // ゲーム開始時は「GAME OVER パネル」を隠しておく
        if(_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }

         // ③ GameLoopManager の「通知ベル（イベント）」に耳を傾ける（購読）
        //GameLoopManager のイベントを購読
        _gameLoopManager.OnScoreChanged += UpdateScore;
        _gameLoopManager.OnTimeChanged += UpdateTimer;
        _gameLoopManager.OnGameOver += ShowGameOver;

    // ④ 起動した瞬間の初期値を画面に反映
        //初期値の反映
        UpdateScore(_gameLoopManager.CurrentScore);
        UpdateTimer(_gameLoopManager.RemainingTime);
        
    }

// ⑤ ★★★ 現場で超重要：片付け処理 ★★★
  private void OnDestroy()
  {
    // メモリリーク防止のため購読解除
    if(_gameLoopManager != null)
        {
            // シーン終了時に通知ベルのリスナーを解除（メモリリーク防止！）
             _gameLoopManager.OnScoreChanged -= UpdateScore;
            _gameLoopManager.OnTimeChanged -= UpdateTimer;
            _gameLoopManager.OnGameOver -= ShowGameOver;
        }
  }

// ⑥ スコア通知が来たら呼ばれる処理
  private void UpdateScore(int score)
    {
        if(_scoreText != null)
        {
            _scoreText.text = $"Score:{score:D5}";
        }
    }

// ⑦ タイマー通知が来たら呼ばれる処理   
     private void UpdateTimer(float time)
    {
        if (_timerText != null)
        {
            _timerText.text = $"Time: {time:F1}s";
        }
    }

// ⑧ ゲームオーバー通知が来たら呼ばれる処理
     private void ShowGameOver()
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }
        if (_finalScoreText != null)
        {
            _finalScoreText.text = $"Final Score: {_gameLoopManager.CurrentScore}";
        }
    }

}
