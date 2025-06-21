using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{

    readonly IObjectResolver resolver;
    public GameEntryPoint(IObjectResolver resolver)
    {
        this.resolver = resolver;
    }

    public void Start()
    {
        resolver.Resolve<PlayerClickHandleController>();
    }
}
