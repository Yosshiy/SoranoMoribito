using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UniRx;

public class BirdChange : MonoBehaviour
{
    ReactiveProperty<bool> IsEnabled = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> EnabledRP => IsEnabled;


    public Transform GetTrans()
    {
        return this.transform;
    }

    public void SetTrans(Transform trans)
    {
        transform.position = trans.position;
        transform.rotation = trans.rotation;
    }

    public void ChangeColor(Material aftercolor, List<Material> objcolor, float duration = 0.5f)
    {
        foreach (var list in objcolor)
        {
            list.DOColor(aftercolor.color, duration).OnComplete(() => IsEnabled.Value = false);
        }
    }

    public void ChangeColorEnd(Material aftercolor, List<Material> objcolor, List<Color> defaultcolor, float duration = 2f)
    {
        foreach (var list in objcolor)
        {
            list.color = aftercolor.color;
        }

        for (int i = 0; i < defaultcolor.Count; i++)
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
