using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    //BGM用AudioSource
    [SerializeField] AudioSource BGM;
    //SE用AudioSource
    [SerializeField] AudioSource SE;


    /// <summary>
    /// BGMのAudioSourceを返す
    /// </summary>
    public AudioSource GetBGM()
    {
        return BGM;
    }

    /// <summary>
    /// SEのAudioSourceを返す
    /// </summary>
    public AudioSource GetSE()
    {
        return SE;
    }

    /// <summary>
    /// SEに引数のAudioClipを割り当てる
    /// </summary>
    public void SESet(AudioClip clip)
    {
        SE.clip = clip;
    }

    /// <summary>
    /// BGMに引数のAudioClipを割り当てる
    /// </summary>
    public void BGMSet(AudioClip clip)
    {
        BGM.clip = clip;
    }

    /// <summary>
    /// 引数のAudioClipをSEとして再生する
    /// </summary>
    public void SEPlayOneShot(AudioClip clip)
    {
        SE.PlayOneShot(clip);
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    public void SEPlay()
    {
        SE.Play();
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    public void BGMPlay()
    {
        BGM.Play();
    }

    /// <summary>
    /// ボリュームを引数の速度でフェードさせて再生する
    /// 最初は小さい音で徐々に大きくなる
    /// </summary>
    public void BGMFadePlay(float speed)
    {
        BGM.volume = 0;
        BGM.Play();
        BGMFadeOut(speed);
    }

    /// <summary>
    /// BGMの再生を止める
    /// </summary>
    public void BGMStop()
    {
        BGM.Stop();
    }

    /// <summary>
    /// SEの再生を止める
    /// </summary>
    public void SEStop()
    {
        SE.Stop();
    }

    /// <summary>
    /// BGM : 引数のスピードでフェードイン
    /// </summary>
    public void BGMFadeIn(float speed)
    {
        BGM.DOFade(0,speed).SetEase(Ease.Linear); ;
    }

    /// <summary>
    /// BGM : 引数のスピードでフェードアウト
    /// </summary>
    public void BGMFadeOut(float speed)
    {
        BGM.DOFade(1, speed).SetEase(Ease.Linear);
    }

    /// <summary>
    /// SE : 引数のスピードでフェードイン
    /// </summary>
    public void SEFadeIn(float speed)
    {
        SE.DOFade(0, speed).SetEase(Ease.Linear); ;
    }

    /// <summary>
    /// SE : 引数のスピードでフェードアウト
    /// </summary>
    public void SEFadeOut(float speed)
    {
        SE.DOFade(1, speed).SetEase(Ease.Linear); ;
    }

    /// <summary>
    /// BGMのボリューム調整
    /// </summary>
    public void BGMVolume(float volume)
    {
        BGM.volume = volume;
    }

    /// <summary>
    /// SEのボリューム調整
    /// </summary>
    public void SEVolume(float volume)
    {
        SE.volume = volume;
    }

}
