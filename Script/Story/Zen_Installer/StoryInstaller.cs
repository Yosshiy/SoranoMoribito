using Zenject;

/// <summary>
/// ストーリーのInstorller
/// </summary>
public class StoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IStoryData>()
            .To<StoryData>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        

    }
}

