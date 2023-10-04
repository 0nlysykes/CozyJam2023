using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public bool muted;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioListener.volume == 0)
        {
            musicOffButton.SetActive(true);
            musicOnButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void unmute()
    {
        AudioListener.volume = 1;
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);
    }


    public void mute()
    {
        AudioListener.volume = 0;
        musicOffButton.SetActive(true);
        musicOnButton.SetActive(false);
    }
}
