using System.Collections;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面
/// </summary>
public class Title : MonoBehaviour
{
    //選択肢
    [SerializeField] List<Text> SelectText;
    //選択肢
    [SerializeField] AudioClip Clip;
    //カーソル
    [SerializeField] Image Cursor;
    //どの位置にカーソルがあるか
    int CursorNum = 0;
    //メソッドを処理しているか
    bool PlayMethod;

    private void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Where(_ => !PlayMethod)
            .Subscribe(_ =>Select())
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.S))
            .Subscribe(_ => MoveCursor(1))
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.W))
            .Subscribe(_ => MoveCursor(-1))
            .AddTo(this);
    }

    /// <summary>
    /// MainGameへ
    /// </summary>
    private void Select()
    {
        PlayMethod = true;
        var timespan = 2;

        MainManager.Instance.M_Sound.SEPlayOneShot(Clip);
        MainManager.Instance.FadeIn(timespan);
        MainManager.Instance.M_Sound.BGMFadeIn(timespan);

        Observable.Timer(TimeSpan.FromSeconds(timespan))
            .Subscribe(_ => MainManager.Instance.M_Scene.LoadScene(_SceneManager.SceneName.Main.ToString()));
    }

    /// <summary>
    /// カーソルを動かす
    /// </summary>
    /// <param name="num">上下どちらに移動するか</param>
    private void MoveCursor(int num)
    {
        CursorNum += num;

        if(CursorNum ==-1)
        {
            CursorNum = SelectText.Count - 1;
        }
        else if(CursorNum > SelectText.Count - 1)
        {
            CursorNum = 0;
        }

        Cursor.rectTransform.anchoredPosition = new Vector2(Cursor.rectTransform.anchoredPosition.x,SelectText[CursorNum].rectTransform.anchoredPosition.y);
    }
}
