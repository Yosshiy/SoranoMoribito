using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerのModelClass
    /// </summary>
    public class PlayerCore : MonoBehaviour
    {
        //速度
        public readonly float Speed = 300;
        //回転力
        public readonly float TorqueSpeed = 100;
        //ジャンプするときの力
        public readonly float JumpPower = 10;


        //SkinnedMeshRenderの中のMaterialsを格納するList
        List<Material> Render;
        //RenderのGetter
        public List<Material> RenderList => Render;
        //Birdの色を格納するList
        List<Color> DefaultColor;
        //DefaultColorのGetter
        public List<Color> DefaultColorList => DefaultColor;


        private void Awake()
        {
            var render = transform.GetComponentInChildren<SkinnedMeshRenderer>();
            Render = render.materials.ToList();
            DefaultColor = render.materials.Select(x => x.color).ToList();

        }

    }
}
