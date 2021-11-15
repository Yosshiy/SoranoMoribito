using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static GameObject[] SceneObj = null;
    const string SceneName = "Master";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void main()
    {
        string ActiveSceneName = SceneManager.GetActiveScene().name;

        if(ActiveSceneName != SceneName)
        {
            SetSceneObjActive(false);
            SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Additive);
        }
    }

    private void Awake()
    {
        StartCoroutine(Awaker());
    }

    IEnumerator Awaker()
    {
        UnityEngine.Object.DontDestroyOnLoad(this.gameObject);

        if(SceneObj != null)
        {
            SetSceneObjActive(true);
            SceneObj = null;
        }

        Scene scene = SceneManager.GetSceneByName(SceneName);

        while(scene.isLoaded == false)
        {
            yield return null;
        }

        yield return SceneManager.UnloadSceneAsync(scene);
    }

    public static void SetSceneObjActive(bool value)
    {
        if(SceneObj == null)
        {
            SceneObj = Object.FindObjectsOfType<Transform>()
                .Select(x => x.root.gameObject)
                .Distinct()
                .Where(x => x.activeInHierarchy)
                .ToArray();
        }

        foreach(GameObject Obj in SceneObj)
        {
            Obj.SetActive(value);
        }
    }
}
