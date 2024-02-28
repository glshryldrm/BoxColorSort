using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class SoundManager
{
    public static void PlaySound()
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GameAssets.Instance.audioClip);
        GameObject.Destroy(soundObj, GameAssets.Instance.audioClip.length +1f);
    }
}
