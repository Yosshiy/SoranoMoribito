using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerMover : MonoBehaviour
{
    //走る
    BoolReactiveProperty Run = new BoolReactiveProperty(false);
    //ジャンプ
    BoolReactiveProperty Jump = new BoolReactiveProperty(false);
    //設置判定
    BoolReactiveProperty Isground = new BoolReactiveProperty(true);

    #region 公開用Property
    public IReadOnlyReactiveProperty<bool> IsgroundRP => Isground;
    public IReadOnlyReactiveProperty<bool> RunRP => Run;
    public IReadOnlyReactiveProperty<bool> JumpRP => Jump;
    #endregion

    /// <summary>
    /// 走る
    /// </summary>
    public bool SetRun
    {
        set { Run.Value = value; }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public bool SetJump
    {
        set { Jump.Value = value; }
    }

    /// <summary>
    /// 設置判定
    /// </summary>
    public bool SetGround
    {
        set { Isground.Value = value; }
    }
}
