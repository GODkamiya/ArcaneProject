using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameSceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    DescriptionPanelViewObject descriptionPanel;

    [SerializeField]
    PlayerClickHandleManager playerClickHandlerManager;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    BoardManager boardManager;

    protected override void Configure(IContainerBuilder builder)
    {
        // エントリーポイントの登録
        // builder.RegisterEntryPoint<GameEntryPoint>();
        builder.RegisterComponent(gameManager);
        builder.RegisterComponent(boardManager);

        // クリック取得システムの登録
        builder.RegisterComponent(playerClickHandlerManager); // Inject呼び出し元を登録
        builder.Register<PlayerClickHandleController>(Lifetime.Singleton); // LifetimeScopeで生成する
        
        // ターンアクション管理システムの登録
        builder.Register<TurnActionManager>(Lifetime.Singleton);

        // UIの登録
        builder.RegisterComponent(descriptionPanel);
        builder.Register<DescriptionPanelController>(Lifetime.Singleton);
    }
}
