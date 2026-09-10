using UnityEngine;
using Zenject;// ① Zenject を使うための宣言

// ② MonoInstaller を継承（シーン上の GameObject に貼れるインストーラー）
public class GameplayInstaller : MonoInstaller
{
    // ③ Unity のインスペクターから登録してもらう 4 つの材料
    [SerializeField] private GameObject _targetPrefab;// 生成するプレハブ本体
    [SerializeField] private Transform _spawnRoot;// 生成した的をまとめる親オブジェクト
    [SerializeField] private GameLoopManager _gameLoopManager;// シーン上のゲーム審判役

    [SerializeField] private TargetSpawner _targetSpawner;// シーン上の現場監督役


// ④ シーン起動時に Zenject が自動で実行する「配線作業」
  public override void InstallBindings()
  {
    // -------------------------------------------------------------
        // 配線①：ターゲット製造工場（Factory）の開設
        // -------------------------------------------------------------
    // 1. Target のプレハブファクトリをバインド（生成時に自動注入）
    Container.BindFactory<Target, Target.Factory>().FromComponentInNewPrefab(_targetPrefab).UnderTransform(_spawnRoot);

// -------------------------------------------------------------
        // 配線②：ゲームの審判役（GameLoopManager）の登録
        // -------------------------------------------------------------
    // 2. シーン上の GameLoopManager をバインド
    Container.Bind<GameLoopManager>().FromInstance(_gameLoopManager).AsSingle();


// -------------------------------------------------------------
        // 配線③：現場監督（TargetSpawner）の登録
        // -------------------------------------------------------------
    // 3. シーン上の TargetSpawner をバインド
    Container.Bind<TargetSpawner>().FromInstance(_targetSpawner).AsSingle();
  }
}
