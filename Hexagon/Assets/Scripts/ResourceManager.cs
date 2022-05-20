using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    [SerializeField] int rssWhite = 3;
    [SerializeField] int rssGreen = 0;
    [SerializeField] int rssBlue = 0;
    [SerializeField] int rssRed = 0;
    [SerializeField] Text textWhite, textGreen, textBlue, textRed;
    
    void Start()
    {
        
    }

 
  public void UpdateRss(int amount, string colour)
    {
        if(colour == "White")
        {
            rssWhite += amount;
            textWhite.text = (""+rssWhite);
        }
        else if(colour == "Green")
        {
            rssGreen += amount;
            textGreen.text = ("" + rssGreen);
        }
        else if(colour == "Blue")
        {
            rssBlue += amount;
            textBlue.text = ("" + rssBlue);
        }
        else if(colour == "Red")
        {
            rssRed += amount;
            textRed.text = ("" + rssRed);
        }
        else {
            Debug.Log("Wrong Material!");
        }
    }

   
}
