using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// StoryViewClass
/// </summary>
public class Story : MonoBehaviour
{
    //ストーリー画面
    [SerializeField] List<Image> StoryImage;
    //ロック画面
    [SerializeField] List<Image> StoryLockImage;

    public  List<Image> GetStoryUI
    {
        get { return StoryImage; }
    }


    /// <summary>
    /// ストーリーの数を返す
    /// </summary>
    public int GetMaxValue()
    {
        //ロック画面の数とストーリーの数がかみ合わない場合はエラーを吐く
        if(StoryImage.Count != StoryLockImage.Count)
        {
            Debug.LogError("ストーリーの数とストーリーのロック画面の数が不一致です。");
        }

        return StoryImage.Count;
    }

    /// <summary>
    /// ストーリーを開放
    /// </summary>
    public void StoryUnLock(int storynum)
    {
        Debug.Log("a");
        StoryLockImage[storynum].gameObject.SetActive(false);
    }

    
    
}
