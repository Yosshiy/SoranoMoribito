using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSet : MonoBehaviour
{
    [SerializeField] AudioClip BGM;

    private void Start()
    {
        MainManager.Instance.M_Sound.BGMSet(BGM);
        MainManager.Instance.M_Sound.BGMPlay();
    }
}
