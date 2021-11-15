using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 鳥Class
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bird : MonoBehaviour
{
    //初速
    const float FirstSpeed = 50;
    //終端速度
    const float FinalySpeed = 300;
    //何秒で加速を終えるか
    const float AccelerationTime = 5;
    //何秒間速度を維持するか
    const float FinalySpeedTime = 5;
    //加速度
    const float Acceleration = 100;
    //速度
    float Speed = FirstSpeed;

    Rigidbody Rb;
    //加速フラグ
    bool Accels;

    Animator anim;
    Coroutine Des = null;

    private void Start()
    {
        
        Rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.Mouse0))
            .Where(x => !Accels)
            .Subscribe(x => StartCoroutine(Accel(Acceleration))).AddTo(this);

        
    }

    private void OnDisable()
    {
        Speed = FirstSpeed;
        Accels = false;
        
    }


    void FixedUpdate()
    {
        var hori = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
 
        //自分に対しての向きで、トルクをかける
        Rb.AddRelativeTorque(new Vector3(0, hori, 0) * 3f);
        Rb.AddRelativeTorque(new Vector3(-vert, 0, 0) * 3f); 

        //x軸方向の水平を保つ
        var left = transform.TransformVector(Vector3.left); 
        var horizontal_left = new Vector3(left.x, 0f, left.z).normalized;   
        Rb.AddTorque(Vector3.Cross(left, horizontal_left) * 11f); 

        //z軸方向の水平を保つ。
        var forward = transform.TransformVector(Vector3.forward);
        var horizontal_forward = new Vector3(forward.x, 0f, forward.z).normalized;
        Rb.AddTorque(Vector3.Cross(forward, horizontal_forward) * 11f);

        //前に発進させる
        var force = (Rb.mass * Rb.drag * Speed/ 3.6f) / (1f - Rb.drag * Time.fixedDeltaTime); 
        Rb.AddRelativeForce(new Vector3(0f, 0f, force));

    }

    

    /// <summary>
    /// 加速
    /// </summary>
    IEnumerator Accel(float push)
    {
        if(Des != null)
        StopCoroutine(Des);
        Accels = true;

        yield return new WaitForSeconds(0.4f);

        anim.SetBool("Speed",true);

        float speed = Speed + push;
        Speed = speed;

        if(speed >= FinalySpeed)
        {
            Speed = FinalySpeed;
        }

        yield return new WaitForSeconds(1f);

        anim.SetBool("Speed", false);
        Accels = false;

        yield return new WaitForSeconds(FinalySpeedTime);
        
        if (Speed == speed||Speed == FinalySpeed && this.gameObject.activeSelf)
        {
            Des = StartCoroutine(ReducedSpeed());
        }
    }

    /// <summary>
    /// 減速
    /// </summary>
    IEnumerator ReducedSpeed()
    {
        //経過時間用
        float time = 0;
        //加速開始時の速度
        float speed = Speed;

        var b = Acceleration / AccelerationTime;
        var duration = speed / b;

        while (time < duration)
        {
            time += Time.deltaTime;
            Speed = Mathf.Lerp(speed,FirstSpeed, Mathf.Clamp01(time / duration));

            yield return null;
        }

        
    }



}
