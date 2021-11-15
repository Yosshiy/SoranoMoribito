using Player;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Cinemachine;

/// <summary>
/// 操作キャラを切り替える処理
/// </summary>
public class PlayerChangeManager : MonoBehaviour
{
    //Player
    [SerializeField] PlayerCore _PlayerCore;
    //Bird
    [SerializeField] BirdCore _BirdCore;
    //PlayerChange
    [SerializeField] PlayerChange _PlayerChange;
    //BirdChange
    [SerializeField] BirdChange _BirdChange;
    //変更後のマテリアル
    [SerializeField] Material AfterColor;

    private void Start()
    {
        //トリ形態へ
        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.Space))
            .Where(x => _PlayerChange.ChangePossibleCheck())
            .Subscribe(x => _PlayerChange.ChangeColorStart(AfterColor,_PlayerCore.RenderList));

        //トリ形態へ
        _PlayerChange.EnabledRP
            .Skip(1)
            .Where(x => x == false)
            .Subscribe(x => 
            {
                _BirdChange.SetTrans(_PlayerChange.GetTrans());
                _PlayerChange.gameObject.SetActive(false);
                _BirdChange.gameObject.SetActive(true);
                _BirdChange.ChangeColorEnd(AfterColor, _BirdCore.RenderList, _BirdCore.DefaultColorList);
                
            });

        //ヒト形態へ
        _BirdChange.OnCollisionEnterAsObservable()
            .Subscribe(x =>
            {
                _PlayerChange.SetTrans(_BirdChange.GetTrans());
                _PlayerChange.gameObject.SetActive(true);
                _BirdChange.gameObject.SetActive(false);
                _PlayerChange.ChangeColorEnd(AfterColor,_PlayerCore.RenderList, _PlayerCore.DefaultColorList);

            });



    }
}
