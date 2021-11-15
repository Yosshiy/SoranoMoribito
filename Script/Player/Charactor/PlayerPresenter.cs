using System.Collections;
using System.Collections.Generic;
using System;
using Player;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// Playerのプレゼンター
/// </summary>
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCore))]
[RequireComponent(typeof(PlayerRigidRapper))]
[RequireComponent(typeof(PlayerChange))]
public class PlayerPresenter : MonoBehaviour
{
    PlayerCore _PlayerCore;
    PlayerMover _PlayerMover;
    PlayerAnimation _PlayerAnimation;
    PlayerRigidRapper _PlayerRigidRapper;
    PlayerChange _PlayerChange;

    private void Start()
    {
        _PlayerCore = GetComponent<PlayerCore>();
        _PlayerMover = GetComponent<PlayerMover>();
        _PlayerRigidRapper = GetComponent<PlayerRigidRapper>();
        _PlayerAnimation = GetComponent<PlayerAnimation>();
        _PlayerChange = GetComponent<PlayerChange>();

        AnimationSetUp();
        MoveSetUp();
        JudgeSetUp();
    }

    /// <summary>
    /// Animationのセットアップ
    /// </summary>
    void AnimationSetUp()
    {
        this.OnEnableAsObservable()
            .Where(x => Input.GetAxis("Vertical") != 0)
            .Subscribe(x => _PlayerAnimation.IsRun = true).AddTo(this);

        _PlayerMover.RunRP
            .DistinctUntilChanged()
            .Subscribe(x => _PlayerAnimation.IsRun = x).AddTo(this);

        _PlayerMover.JumpRP
            .DistinctUntilChanged()
            .Subscribe(x => _PlayerAnimation.IsJump = x).AddTo(this);

        _PlayerChange.ChangeRP
            .DistinctUntilChanged()
            .Subscribe(x => _PlayerAnimation.IsChange = x).AddTo(this);

        _PlayerMover.IsgroundRP
            .DistinctUntilChanged()
            .Subscribe(x => _PlayerAnimation.IsGround = x).AddTo(this);

        //変身途中はAccel()を回す
        this.FixedUpdateAsObservable()
            .Where(x => _PlayerChange.ChangeRP.Value == true)
            .Subscribe(x => _PlayerRigidRapper.Accel()).AddTo(this);

    }

    /// <summary>
    /// 動きに関するセットアップ
    /// </summary>
    void MoveSetUp()
    {
        
        this.UpdateAsObservable()
            .Where(x => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(x =>
            {
                if (_PlayerMover.IsgroundRP.Value)
                {
                    _PlayerMover.SetJump = true;

                    _PlayerRigidRapper.Jump();
                }

            }).AddTo(this);

        this.FixedUpdateAsObservable()
            .Select(x => Input.GetAxis("Horizontal"))
            .Subscribe(x => _PlayerRigidRapper.Rotate(x, _PlayerCore.TorqueSpeed))
            .AddTo(this);

        this.FixedUpdateAsObservable()
            .Select(x => Input.GetAxis("Vertical"))
            .Subscribe(x => _PlayerRigidRapper.Move(x, _PlayerCore.Speed))
            .AddTo(this);

        this.UpdateAsObservable()
            .Select(x => Input.GetAxis("Vertical"))
            .Subscribe(x =>
            {

                if(x != 0 && _PlayerMover.IsgroundRP.Value)
                {
                    _PlayerMover.SetRun = true;
                }
                else if(x == 0)
                {
                    _PlayerMover.SetRun = false;
                }
            }).AddTo(this);
    }

    /// <summary>
    /// 判定のセットアップ
    /// </summary>
    void JudgeSetUp()
    {
        _PlayerMover.OnCollisionStayAsObservable()
            .Where(x=>!_PlayerMover.IsgroundRP.Value)
            .Subscribe(x => {
                _PlayerMover.SetGround = true;
                _PlayerMover.SetJump = false;

            }).AddTo(this);

        _PlayerMover.OnCollisionExitAsObservable()
            .Subscribe(x => _PlayerMover.SetGround = false).AddTo(this);

    }

   
}
