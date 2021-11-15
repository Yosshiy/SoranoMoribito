using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// バードキャラのModelClass
/// </summary>
public class BirdModel : IBird
{
    //初速
    const float FirstSpeed = 10;
    //終端速度
    const float FinalySpeed = 100;
    //何秒で加速を終えるか
    const float AccelerationTime = 3;
    //何秒間速度を維持するか
    const float FinalySpeedTime = 5;
    //加速度
    const float Acceleration = 20;






    //スピード
    FloatReactiveProperty Speed = new FloatReactiveProperty(FirstSpeed);
    //加速度
    public IReactiveProperty<float> SpeedRP => Speed;

    public float ReducedSpeed()
    {
        var a = Speed.Value / FinalySpeed * 100;
        var b = Acceleration/ AccelerationTime;
        var x = a / b;

        return x;
    }
}

public interface IBird
{
    IReactiveProperty<float> SpeedRP { get; }
}
