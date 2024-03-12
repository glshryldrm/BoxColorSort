using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class SoundManager
{
    public static void PlayBubbleSound()
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GameAssets.Instance.bubbleAudioClip);
        GameObject.Destroy(soundObj, GameAssets.Instance.bubbleAudioClip.length +1f);
    }
    public static void PlayCorrectSound()
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GameAssets.Instance.correctAudioClip);
        GameObject.Destroy(soundObj, GameAssets.Instance.correctAudioClip.length + 1f);
    }
}
