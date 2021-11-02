using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private SoundPrefab soundPrefab;

    public void TouchSfx()
    {
        PlaySFX(SFX.clickSFX, 0.2f);
    }

    public void PlaySFX(SFX name, float vol = 1, float time = 0)
    {

        PlaySFX(soundPrefab.GetClip(name), vol, time);
    }

    public void PlaySFX(AudioClip clip, float vol, float time = 0)
    {
        if (clip == null) return;

        StartCoroutine(DoPlayDelay(clip, vol, time));
    }

    private IEnumerator DoPlayDelay(AudioClip clip, float vol, float time = 0)
    {
        yield return new WaitForSeconds(time);
        sfxPlayer.PlayOneShot(clip, vol + 0.15f);
    }
}
