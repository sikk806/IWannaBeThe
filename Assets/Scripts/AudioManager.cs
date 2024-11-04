using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> audioClips;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.loop = true;
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSound(int index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
