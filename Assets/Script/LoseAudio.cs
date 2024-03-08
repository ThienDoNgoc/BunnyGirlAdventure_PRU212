using UnityEngine;



public class LoseAudio : MonoBehaviour
{
   
    [SerializeField]  AudioSource musicSource;
    [SerializeField]  AudioSource SFXSource;

    public AudioClip background;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }
}
