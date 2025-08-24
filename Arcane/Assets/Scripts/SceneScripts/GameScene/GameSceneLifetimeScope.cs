using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameSceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    DescriptionPanelViewObject descriptionPanel;

    [SerializeField]
    PlayerClickHandleManager playerClickHandlerManager;

    protected override void Configure(IContainerBuilder builder)
    {
        // エントリーポイントの登録
        builder.RegisterEntryPoint<GameEntryPoint>();

        // クリック取得システムの登録
        builder.RegisterComponent(playerClickHandlerManager);
        builder.Register<PlayerClickHandleController>(Lifetime.Singleton);

        // UIの登録
        builder.RegisterComponent(descriptionPanel);
        builder.Register<DescriptionPanelController>(Lifetime.Singleton);
    }
}
