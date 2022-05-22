using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] int startLives;
    [SerializeField] int remainingLives;
    [SerializeField] TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        remainingLives = startLives;
        livesText.text = "Lives Remain: " + remainingLives;
    }

   public void LoseLife(int livesToLose)
    {
        remainingLives -= livesToLose;
        livesText.text = "Lives Remain: " + remainingLives;

        if(remainingLives <= 0)
        {
            //Restart or Game Over or Other Board
        }
    }
}
