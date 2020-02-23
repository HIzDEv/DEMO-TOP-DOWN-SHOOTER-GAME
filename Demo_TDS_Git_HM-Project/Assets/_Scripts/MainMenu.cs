using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioMixer Audio;

	public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }


    //Chager la valeur du volume
    public void setVolume(float volume)
    {
        Audio.SetFloat("volume",volume);
    }
}
