using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerのAnimation管理Class
    /// </summary>
    public class PlayerAnimation : MonoBehaviour
    {
        Animator _Animator;

        /// <summary>
        /// 走る
        /// </summary>
        public bool IsRun
        {
            set { _Animator.SetBool("Run", value); }
        }


        /// <summary>
        /// ジャンプ
        /// </summary>
        public bool IsJump
        {
            set { _Animator.SetBool("Jump", value); }
        }

        /// <summary>
        /// チェンジ
        /// </summary>
        public bool IsChange
        {
            set { _Animator.SetBool("Change", value); }
        }

        public bool IsGround
        {
            set { _Animator.SetBool("IsGround", value); }
        }

        private void Awake()
        {
            _Animator = GetComponent<Animator>();

        }

    }
}
