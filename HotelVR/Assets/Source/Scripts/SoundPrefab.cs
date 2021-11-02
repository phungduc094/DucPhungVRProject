using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundPrefab : ScriptableObject
{

    public List<Sound> clips;
    public AudioClip[] coinClips;
    public AudioClip[] winClips;
    public AudioClip[] loseClips;
    public AudioClip appearClip;

    public SoundPrefab()
    {
    }
    public AudioClip GetWinClip()
    {
        return winClips[Random.Range(0, winClips.Length)];
    }
    public AudioClip GetLoseClip()
    {
        return loseClips[Random.Range(0, loseClips.Length)];
    }
    public AudioClip GetCoinClip()
    {
        return coinClips[Random.Range(0, coinClips.Length)];
    }
    public AudioClip GetClip(SFX name)
    {

        AudioClip clip = null;
       
        for (int i = 0; i < clips.Count; i++)
        {
            if (name.Equals(clips[i].name))
            {
                clip = clips[i].clip;
            }
        }

        return clip;
    }


}
public enum SFX
{
    clickSFX
}
[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public SFX name;
}