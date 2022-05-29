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
 

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
     
        if (musicEnabled == false)
        {

            AudioListener.pause = true;
            
            
        }
    }

 
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
        AudioListener.pause = true;
        musicEnabled = false;
    }
        public void EnableMusic()
    {
        AudioListener.pause = false;
        FindObjectOfType<AudioListener>().enabled = true;

    }
      
  

}
