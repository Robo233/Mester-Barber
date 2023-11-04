using UnityEngine;
using System.Collections;
 
public class AudioFade: MonoBehaviour {
 
     public IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Pause ();
        audioSource.volume = startVolume;
    }

     public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;
 
        audioSource.volume = 0;
        audioSource.Play();
 
        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime * FadeTime;
 
            yield return null;
        }
 
        audioSource.volume = 1f;
    }
    
 
}
