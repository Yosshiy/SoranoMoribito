using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ストーリーの閲覧
/// </summary>
public class StoryScribe : MonoBehaviour
{
    //ストーリー閲覧オブジェクト
    [SerializeField] GameObject Master;
    //ストーリーの進行度
    int StoryCount = 0;
    //左右にどのくらい動くのか(1920は画面サイズ)
    readonly Vector3 Origin = new Vector3(1920,0,0);
    //閲覧モードのフラグ
    bool ViewMode;

    
    private void Start()
    {

        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.D))
            .Where(x => ViewMode)
            .DistinctUntilChanged()
            .Subscribe(x => RightMove())
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.A))
            .Where(x => ViewMode)
            .DistinctUntilChanged()
            .Subscribe(x => LeftMove())
            .AddTo(this);
    }

    /// <summary>
    /// 画面を右にスライド
    /// </summary>
    public void RightMove()
    {
        if(StoryCount == Master.transform.childCount - 1)
        {
            return;
        }
        Master.transform.position -= Origin;
        StoryCount += 1;
    }

    /// <summary>
    /// 画面を左にスライド
    /// </summary>
    public void LeftMove()
    {
        if(StoryCount == 0)
        {
            return;
        }
        Master.transform.position += Origin;
        StoryCount += -1;
    }

    /// <summary>
    /// ストーリー閲覧画面を開く
    /// </summary>
    public void OpenUIWindow()
    {
        ViewMode = true;
        Master.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// ストーリー閲覧画面を閉じる
    /// </summary>
    public void CloseUIWindow()
    {
        ViewMode = false;
        Master.SetActive(false);
        Time.timeScale = 1;
    }


    /// <summary>
    /// ※※※文化祭展示用にタイトルに戻れるように
    /// 文化祭終わり次第削除すること※※※
    /// </summary>
    public void Title()
    {
        MainManager.Instance.M_Scene.LoadScene(_SceneManager.SceneName.Title.ToString());
    }


}
