using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    //BuildSetting内のScene
    public enum SceneName
    {
        Main,
        Loading,
        Title,
        StaffRoll,
        Master,
        ZUKANtest
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// SceneをSingleでロード
    /// </summary>
    /// <param name="name">_SceneManagerのSceneName</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// SceneをAddtiveでロード
    /// </summary>
    public void AddtiveScene(string name)
    {
        SceneManager.LoadScene(name,LoadSceneMode.Additive);
    }

    public void RemoveScene(string scenename)
    {
        SceneManager.UnloadSceneAsync(scenename);
    }

    /// <summary>
    /// Sceneがロードされたときにフェードの処理を行う
    /// </summary>
    void SceneLoaded(Scene nextscene,LoadSceneMode mode)
    {
        MainManager.Instance.FadeOut(3);
        VolumeLiset();
    }

    /// <summary>
    /// AudioSourseのVolumeを初期化
    /// </summary>
    void VolumeLiset()
    {
        MainManager.Instance.M_Sound.BGMVolume(1);
        MainManager.Instance.M_Sound.SEVolume(1);
    }
}
