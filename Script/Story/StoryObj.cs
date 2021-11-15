using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Zenject;

/// <summary>
/// ストーリー開放用
/// </summary>
public class StoryObj : MonoBehaviour
{ 
    //プレイヤー
    GameObject Player;
    //アクションが起こる距離
    float Distance = 5;
    //変更させるオブジェクトのレンダラー
    [SerializeField]Renderer RenderMaterial;
    //アニメーター
    [SerializeField]Animator DefaultAnimator;

    //もともとのマテリアルのカラー
    Color32 DefaultColor;
    //非アクテイブ状態のカラー
    Color UnActiveColor = Color.black;
    //解放したか否かのフラグ
    bool Check = false;

    IStoryData _StoryData = null;

    [Inject]
    public void Data_Construct(IStoryData inject)
    {
        _StoryData = inject;
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        UnActivate();

        this.ObserveEveryValueChanged(x => Vector3.Distance(Player.transform.position, transform.position))
            .Where(x => x <= Distance)
            .Where(x => !Check)
            .Subscribe(x =>
            {
                LightUp();
                _StoryData.Open();
            });

    }

    

    /// <summary>
    /// 初期化
    /// </summary>
    void UnActivate()
    {
        DefaultColor = RenderMaterial.material.color;
        //変更パラメータを指定
        RenderMaterial.material.EnableKeyword("_EMISSION");
        //Emissionを切っておく
        RenderMaterial.material.SetColor("_EmissionColor", UnActiveColor);
        //アニメーションを切る
        DefaultAnimator.enabled = false;
        //カラーを非アクティブにする
        RenderMaterial.material.color = UnActiveColor;
    }

    /// <summary>
    /// 点灯させる
    /// </summary>
    void LightUp()
    {
        Check = true;
        DefaultAnimator.enabled = true;
        RenderMaterial.material.DOColor(DefaultColor, 2);

    }

}
