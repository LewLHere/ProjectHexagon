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
            FindObjectOfType<AudioListener>().enabled = false;
            
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
        FindObjectOfType<AudioListener>().enabled = false;
        musicEnabled = false;
    }
        public void EnableMusic()
    {
        musicEnabled = true;
        FindObjectOfType<AudioListener>().enabled = true;

    }
      
  

}
