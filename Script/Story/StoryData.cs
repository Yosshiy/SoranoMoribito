using System.Collections;
using System.Collections.Generic;
using UniRx;



public interface IStoryData
{
    IReadOnlyReactiveProperty<int> StoryUnlockRP { get; }
    void Open();
}

/// <summary>
/// ModelClass
/// </summary>
public class StoryData : IStoryData
{
    //ゲーム開始時のストーリー進度
    const int FirstStory = -1;
    //ストーリーの進捗
    private IntReactiveProperty StoryProperty = new IntReactiveProperty(FirstStory);
    //公開用プロパティ
    public IReadOnlyReactiveProperty<int> StoryUnlockRP => StoryProperty;

    
    /// <summary>
    /// ストーリー開放
    /// </summary>
    public void Open()
    {
        StoryProperty.Value++;
    }

    
}
