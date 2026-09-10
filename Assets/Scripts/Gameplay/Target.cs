using UnityEngine;
using Zenject;

public class Target : MonoBehaviour
{
    [SerializeField] private AudioClip _hitClip;

    [Inject] private IAudioService _audioService;
    [Inject] private GameLoopManager _gameLoopManager;

  //クリックされた時の処理
  private void OnMouseDown()
  {
    if(!_gameLoopManager.IsPlaying) return;

    //効果音を鳴らす
    _audioService.PlaySE(_hitClip);

    //スコア加算
    _gameLoopManager.AddScore(100);

    //自身を破壊
    Destroy(gameObject);
  }

  public class Factory : PlaceholderFactory<Target>{}
}
