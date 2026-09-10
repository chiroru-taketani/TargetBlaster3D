using System.Collections;
using UnityEngine;
using Zenject;

public class TargetSpawner : MonoBehaviour
{
    // ① 出現間隔（秒）。インスペクターから自由に変更可能
    [SerializeField] private float _spawnInterval = 0.8f;

 // ② Zenject に届けてもらう2つの道具
    [Inject] private Target.Factory _targetFactory;
    [Inject] private GameLoopManager _gameLoopManager;
    void Start()
    {
        // ③ コルーチン（時間差ループ処理）を開始
        StartCoroutine(SpawnLoop());
    }

//④ 一定時間ごとに繰り返すループ処理
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (_gameLoopManager.IsPlaying)
            {
                SpawnTarget();
            }
            // 指定した秒数（0.8秒）だけ待機して次のループへ
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

 // ⑤ ターゲットを実際に1個生み出して配置するメソッド
    private void SpawnTarget()
    {
        // Zenject のファクトリでターゲットを生成
        Target target = _targetFactory.Create();

        //ランダムな3D座標に配置
        Vector3 randomPos = new Vector3(
            Random.Range(-4f, 4f),
            Random.Range(1f, 4f),
            Random.Range(2f, 8f)
        );
        target.transform.position = randomPos;
    }

   
}
