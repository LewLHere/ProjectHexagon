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
