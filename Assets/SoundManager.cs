using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

}