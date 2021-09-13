using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private GameObject _sheepStory;
    [SerializeField] private AudioClip _sheepStoryClip;
    [SerializeField] private GameObject _wolfStory;
    [SerializeField] private AudioClip _wolfStoryClip;
    [SerializeField] private GameObject _rhymeStory;
    [SerializeField] private AudioClip _rhymeStoryClip;

    private AudioSource _storyAudioSource;

    private void Awake()
    {
        _storyAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartStory(_index);
    }

    private void Update()
    {
        if (_storyAudioSource.isPlaying) return;
        _index++;
        StartStory(_index);
    }

    private void StartStory(int index)
    {
        if(index == 0)
        {
            EnableStoryArt(_sheepStory);
            EnableStoryAudio(_sheepStoryClip);
        }
        else if (index == 1)
        {
            EnableStoryArt(_wolfStory);
            EnableStoryAudio(_wolfStoryClip);
        }
        else if(index == 2)
        {
            EnableStoryArt(_rhymeStory);
            EnableStoryAudio(_rhymeStoryClip);
        }
        else
        {
            SceneController.LoadScene("Game");
        }
    }

    private void EnableStoryArt(GameObject storyArt)
    {
        _sheepStory.SetActive(false);
        _wolfStory.SetActive(false);
        _rhymeStory.SetActive(false);
        storyArt.SetActive(true);
    }
    private void EnableStoryAudio(AudioClip storyClip)
    {
        _storyAudioSource.PlayOneShot(storyClip);
    }
}
