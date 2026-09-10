using Zenject;
using UnityEngine;

public class ProjectInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
    // スコア保存サービスをシングルトンとしてバインド
    Container.BindInterfacesTo<PlayerPrefsScoreRepository>().AsSingle();

    //オーディオサービスを ProjectContext 配下の GameObject として常駐バインド
    Container.BindInterfacesTo<AudioService>().FromNewComponentOnNewGameObject().WithGameObjectName("AudioService").AsSingle();


   Debug.Log("<color=cyan>[ProjectInstaller]</color> 基盤サービスのバインド完了");
  }
}
