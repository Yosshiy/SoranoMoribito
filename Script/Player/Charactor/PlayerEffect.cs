using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem HokoriEffectR;
    [SerializeField] ParticleSystem HokoriEffectL;

    /// <summary>
    /// AnimationKey_右足
    /// </summary>
    void Hokori_R()
    {
        HokoriEffectR.Play();
    }

    /// <summary>
    /// AnimationKey_左足
    /// </summary>
    void Hokori_L()
    {
        HokoriEffectL.Play();
    }
}
