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

    [SerializeField]
    GameConfig gameConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        // エントリーポイントの登録
        // builder.RegisterEntryPoint<GameEntryPoint>();
        builder.RegisterComponent(gameManager);
        builder.RegisterComponent(boardManager);

        // クリック取得システムの登録
        builder.RegisterComponent(playerClickHandlerManager); // Inject呼び出し元を登録
        builder.Register<PlayerClickHandleController>(Lifetime.Singleton); // LifetimeScopeで生成する

        // UIの登録
        builder.RegisterComponent(descriptionPanel);
        builder.Register<DescriptionPanelController>(Lifetime.Singleton);

        // ゲーム設定の登録
        builder.RegisterInstance(gameConfig);
    }
}
