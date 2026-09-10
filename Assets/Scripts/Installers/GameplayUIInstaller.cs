using UnityEngine;
using Zenject;

public class GameplayUIInstaller : MonoInstaller
{
    [SerializeField] private GameplayUIPresenter _uiPresenter;
    public override void InstallBindings()
    {
       // シーン上の UI Presenter をバインド
        Container.Bind<GameplayUIPresenter>()
            .FromInstance(_uiPresenter)
            .AsSingle();
    
    }
}
