using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Birdのデータを持つClass
/// </summary>
public class BirdCore : MonoBehaviour
{
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
