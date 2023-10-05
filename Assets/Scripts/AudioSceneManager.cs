using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneManager : MonoBehaviour
{
    [Header("Music Audio Source")]
    [SerializeField] AudioSource musicSource;

    /*[Header("Music Audio Source")]
    [SerializeField] AudioSource ambienceSource;

    [Header("Audio Clip")]
    public AudioClip menuMusic;

    [Header("Ambient Sound")]
    public AudioClip ambientSound;*/

    public static AudioSceneManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // musicSource.clip = menuMusic;
        // ambienceSource.clip = ambientSound;
        musicSource.Play();
        // ambienceSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "EasyLevel" || Application.loadedLevelName == "MediumLevel" || Application.loadedLevelName == "HardLevel")
        {
            Destroy(this.gameObject);
        }
    }
}
