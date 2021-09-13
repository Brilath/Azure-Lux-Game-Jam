using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
