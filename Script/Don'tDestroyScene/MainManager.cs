using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  class MainManager : MonoBehaviour
{
    #region シングルトン
    /// <summary>
    /// シングルトン
    /// </summary>
    private static MainManager instance;

    public static MainManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (MainManager)FindObjectOfType(typeof(MainManager));
                if (instance = null)
                {
                    Debug.LogError(typeof(MainManager) + "が存在しません");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] Image FadeImage;
    public SoundManager M_Sound;
    public _SceneManager M_Scene;

    #region フェード処理
    /// <summary>
    /// フェードに入る
    /// </summary>
    /// <param name="Speed">
    /// フェードのスピード
    /// </param>
    public void FadeIn(float Speed)
    {
        FadeImage.DOFade(1,Speed);
    }

    /// <summary>
    /// フェードから抜ける
    /// </summary>
    /// <param name="Speed">
    /// フェードのスピード
    /// </param>
    public void FadeOut(float Speed)
    {
        FadeImage.DOFade(0, Speed);
    }

    /// <summary>
    /// DOTweenをKill
    /// </summary>
    public void FadeKill()
    {
        FadeImage.DOKill();
    }

    public DG.Tweening.Core.TweenerCore<Color,Color,DG.Tweening.Plugins.Options.ColorOptions> FadeDetailed(int endValue,int duration)
    {
        return FadeImage.DOFade(endValue,duration);
    }
    #endregion

    #region 時間の管理
    /// <summary>
    /// 時間を止める
    /// </summary>
    public void TimeStop()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 時間を進める
    /// </summary>
    public void TimeStart()
    {
        Time.timeScale = 1;
    }
    #endregion


}
