using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ManageScene : MonoBehaviour
{
    int diffituly;
    
   
   
        bool musicEnabled = true;
    [SerializeField] AudioSource musicSource;
   

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
     
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
       
    }

        public void DisableMusic()
    {
        musicEnabled = false;
        musicSource.Stop();
    }
        public void EnableMusic()
    {

        musicEnabled = true;
        musicSource.Play();

    }
      
  

}
