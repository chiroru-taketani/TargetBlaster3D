using UnityEngine;

public class AudioService : MonoBehaviour, IAudioService
{
    private AudioSource _seSource;

  private void Awake()
  {
    _seSource = gameObject.AddComponent<AudioSource>();
  }

  public void PlaySE(AudioClip clip)
    {
        if(clip != null) _seSource.PlayOneShot(clip);
    }
}
