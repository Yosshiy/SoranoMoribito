using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UniRx;

namespace Player
{
    /// <summary>
    /// Playerの切り替え時の処理
    /// </summary>
    public class PlayerChange : MonoBehaviour
    {
        //Active状態を通知
        ReactiveProperty<bool> IsEnabled = new ReactiveProperty<bool>(true);
        public IReadOnlyReactiveProperty<bool> EnabledRP => IsEnabled;
        //プレイヤーの変身を通知
        ReactiveProperty<bool> Change = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> ChangeRP => Change;

        /// <summary>
        /// Transformを取得
        /// </summary>
        /// <returns></returns>
        public Transform GetTrans()
        {
            return this.transform;
        }

        /// <summary>
        /// Transformを切り替え
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(Transform trans)
        {
            transform.position = trans.position;
            transform.rotation = trans.rotation;
            transform.DORotateQuaternion(Quaternion.Euler(0, trans.transform.eulerAngles.y, 0), 0.1f);
        }

        /// <summary>
        /// 体の色を変える(デフォルトから変身色に)
        /// </summary>
        /// <param name="aftercolor">変え終わったらこの色になる</param>
        /// <param name="objcolor">このMaterialを変える</param>
        /// <param name="duration">この速度で色の変化が終わる</param>
        public void ChangeColorStart(Material aftercolor,List<Material> objcolor,float duration = 1.5f)
        {
            Change.Value = true;

            foreach(var list in objcolor)
            {
                
                list.DOColor(aftercolor.color, duration)
                    .OnComplete(() => 
                    {
                        IsEnabled.Value = false;
                        Change.Value = false;
                        
                    });
            }
        }

        /// <summary>
        /// 色を変える(変身色からデフォルトに)
        /// </summary>
        /// <param name="aftercolor">変身色</param>
        /// <param name="objcolor">このMaterialの色を変える</param>
        /// <param name="defaultcolor">元の色</param>
        /// <param name="duration">この速度で色が変わる</param>
        public void ChangeColorEnd(Material aftercolor,List<Material> objcolor,List<Color> defaultcolor,float duration = 2f)
        {
            foreach(var list in objcolor)
            {
                list.color = aftercolor.color;
            }

            for(int i = 0;i < defaultcolor.Count;i++)
            {
                objcolor[i].DOColor(defaultcolor[i], duration).OnComplete(() => IsEnabled.Value = true);
            }
        }

        


        /// <summary>
        /// 代われるかどうか確認
        /// </summary>
        public bool ChangePossibleCheck(float distance = 2)
        {
            var Obj = Physics.OverlapSphere(transform.position, distance)
                             .Where(X => !X.CompareTag("Player"));

            if (Obj.Count() > 0)
            {
                return false;
            }

            return true;
        }

    }
}
