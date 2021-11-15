using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Player
{

    /// <summary>
    /// RigidBodyClassのラッパー
    /// </summary>
    public class PlayerRigidRapper : MonoBehaviour
    {

        Rigidbody Rigid;

        private void Start()
        {
            Rigid = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// ジャンプ
        /// </summary>
        public void Jump()
        {
            Rigid.AddForce(transform.TransformDirection(Vector3.up) * Rigid.mass * 9, ForceMode.Acceleration);
        }

        public void Accel()
        {
            Rigid.AddForce(transform.TransformDirection(Vector3.forward) * Rigid.mass * 10, ForceMode.Acceleration);
            Rigid.AddForce(transform.TransformDirection(Vector3.up) * Rigid.mass / 7, ForceMode.Acceleration);
        }

        /// <summary>
        /// 前進後退
        /// </summary>
        /// <param name="vert">Input.GetAxis</param>
        /// <param name="speed">速度</param>
        public void Move(float vert, float speed)
        {
            Rigid.velocity = new Vector3(transform.forward.x * speed * vert * Time.deltaTime, Rigid.velocity.y, transform.forward.z * speed * vert * Time.deltaTime);
        }

        /// <summary>
        /// 回転
        /// </summary>
        /// <param name="hori"></param>
        /// <param name="speed"></param>
        public void Rotate(float hori, float speed)
        {
            transform.Rotate(transform.up * Time.deltaTime * speed * hori);
            Rigid.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * hori * speed);
        }

        /// <summary>
        /// 設置判定
        /// </summary>
        /// <returns></returns>
        public bool CheckGrounded()
        {

            //放つ光線の初期位置と姿勢
            var ray = new Ray(this.transform.position, Vector3.down);
            //Raycastがhitするかどうかで判定
            var result = Physics.Raycast(ray, 1.5f);

            return result;
        }

    }
}
