using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ManageScene : MonoBehaviour
{
    int diffituly;
    [SerializeField] GameObject boardCanvas;
    [SerializeField] GameObject difficultyCanvas;
    [SerializeField] GameObject musicText;
    [SerializeField] GameObject soundText;
    bool soundEnabled = true;
        bool musicEnabled = true;
    [SerializeField] AudioSource musicSource;
   

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
      if(soundEnabled == false)
        {

        }
        if (musicEnabled == false)
        {
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
        }
    }

 
    public void GameOver()
    { SceneManager.LoadScene(0); }

    public void LoadBoard(int boardNumber)
    {
        SceneManager.LoadScene(boardNumber);
    }

     public int GetDifficulty()
    { return diffituly; }

    public void SetDifficulty(int difficultyNumber)
    {
             diffituly = difficultyNumber;
            difficultyCanvas.SetActive(false);
            boardCanvas.SetActive(true);
    }

    public bool GetSoundEnabled()
    { return soundEnabled; }
    public void EnableSound()
    {
        if (soundEnabled)
        {
            soundText.GetComponent<Text>().text = "Sound currently Disabled";
            soundEnabled = false;
            return;
        }
        if(!soundEnabled)
        { soundEnabled = true;
            soundText.GetComponent<Text>().text = "Sound currently Enabled";
        }
    }

    public void EnableMusic()
    {
        if (musicEnabled)
        {
            musicText.GetComponent<Text>().text = "Music currently Disabled";
            musicEnabled = false;
            musicSource.Stop();
            return;
        }
        if (!musicEnabled)
        {
            musicText.GetComponent<Text>().text = "Music currently Enabled";
            musicEnabled = true;
            musicSource.Play();
        }
    }


}
