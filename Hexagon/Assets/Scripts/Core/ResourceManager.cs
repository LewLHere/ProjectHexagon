using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{

    [SerializeField] int rssWhite = 0;
    [SerializeField] int rssGreen = 0;
    [SerializeField] int rssBlue = 0;
    [SerializeField] int rssRed = 0;
    [SerializeField] int[] startRssWhite;
    [SerializeField] int[] startRssColour;
    [SerializeField] TextMeshProUGUI textWhite, textGreen, textBlue, textRed;
    ManageScene manageScene;
    
    void Start()
    { manageScene = FindObjectOfType<ManageScene>();

        rssWhite = startRssWhite[manageScene.GetDifficulty()];
        rssGreen = startRssColour[manageScene.GetDifficulty()];
        rssBlue = startRssColour[manageScene.GetDifficulty()];
        rssRed = startRssColour[manageScene.GetDifficulty()];
        UpdateRss(0, "White");
    }

 
    public int GetBlue()
    { return rssBlue; }
    public int GetWhite()
    { return rssWhite; }
    public int GetRed()
    { return rssRed; }
    public int GetGreen()
    { return rssGreen; }
    public void UpdateRss(int amount, string colour)
    {
        if(colour == "White")
        {
            rssWhite += amount;
          
        }
        else if(colour == "Green")
        {
            rssGreen += amount;
         
        }
        else if(colour == "Blue")
        {
            rssBlue += amount;
          
        }
        else if(colour == "Red")
        {
            rssRed += amount;
           
        }
        else {
            Debug.Log("Wrong Material!");
        }

        textWhite.text = ("" + rssWhite);
        textGreen.text = ("" + rssGreen);
        textBlue.text = ("" + rssBlue);
        textRed.text = ("" + rssRed);
    }

   
}
