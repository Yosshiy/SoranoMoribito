using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

/// <summary>
/// PresenterClass
/// </summary>
public class StoryPresenter : MonoBehaviour
{
    [SerializeField] Story _Story;

    IStoryData _StoryData = null;

    [Inject]
    public void Data_Construct(IStoryData inject)
    {
        _StoryData = inject;
    }

    
    private void Start()
    {
        //Skip(1)...登録時の購読を無視
        //ロックを解除
        _StoryData.StoryUnlockRP
            .Skip(1)
            .Subscribe(x => { _Story.StoryUnLock(x); });

    }

}
