using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    int diffituly;
    [SerializeField] GameObject boardCanvas;
    [SerializeField] GameObject difficultyCanvas;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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

}
