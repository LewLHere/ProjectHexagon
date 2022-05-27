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
    [SerializeField] TextMeshProUGUI[] yellowCostText;
    [SerializeField] TextMeshProUGUI[] greenCostText;
    [SerializeField] TextMeshProUGUI[] blueCostText;
    [SerializeField] TextMeshProUGUI[] redCostText;
    [SerializeField] Building harvesterPrefab;
    [SerializeField] Building towerPrefab;
    [SerializeField] Building forcefieldPrefab;
    [SerializeField] Building pulsePrefab;
    BuildManager bm;
    void Start()
    { manageScene = FindObjectOfType<ManageScene>();
        bm = FindObjectOfType<BuildManager>();
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

        if ((rssWhite) < harvesterPrefab.costWhite[0])
        { yellowCostText[0].color = new Color(1, 0, 0); }
        else yellowCostText[0].color = new Color(1, 1, 1);

        if ((rssWhite) < towerPrefab.costWhite[0])
        { yellowCostText[1].color = new Color(1, 0, 0); }
        else yellowCostText[1].color = new Color(1, 1, 1);

        if ((rssWhite) < forcefieldPrefab.costWhite[0])
        {yellowCostText[2].color = new Color(1, 0, 0); }
        else yellowCostText[2].color = new Color(1, 1, 1);

        if ((rssWhite) < pulsePrefab.costWhite[0])
        { yellowCostText[3].color = new Color(1, 0,0);  }
        else yellowCostText[3].color = new Color(1, 1, 1);


        if ((rssGreen) < harvesterPrefab.costGreen[0])
        { greenCostText[0].color = new Color(1, 0, 0); }
        else greenCostText[0].color = new Color(1, 1, 1);

        if ((rssWhite) < towerPrefab.costGreen[0])
        {greenCostText[1].color = new Color(1, 0, 0); }
        else greenCostText[1].color = new Color(1, 1, 1);

        if ((rssGreen) < forcefieldPrefab.costGreen[0])
        { greenCostText[2].color = new Color(1, 0, 0); }
        else greenCostText[2].color = new Color(1, 1, 1);

        if ((rssGreen) < pulsePrefab.costGreen[0])
        { greenCostText[3].color = new Color(1, 0, 0); }
        else greenCostText[3].color = new Color(1, 1, 1);


        if ((rssRed) < harvesterPrefab.costRed[0])
        { redCostText[0].color = new Color(1,0, 0); }
        else redCostText[0].color = new Color(1, 1, 1);

        if ((rssRed) < towerPrefab.costRed[0])
        { redCostText[1].color = new Color(1, 0, 0); }
        else redCostText[1].color = new Color(1, 1, 1);

        if ((rssRed) < forcefieldPrefab.costRed[0])
        { redCostText[2].color = new Color(1,0, 0); }
        else redCostText[2].color = new Color(1, 1, 1);

        if ((rssRed) < pulsePrefab.costRed[0])
        { redCostText[3].color = new Color(1, 0, 0); }
        else redCostText[3].color = new Color(1, 1, 1);


        if ((rssBlue) < harvesterPrefab.costBlue[0])
        {blueCostText[0].color = new Color(1, 0, 0); }
        else blueCostText[0].color = new Color(1, 1, 1);

        if ((rssBlue) < towerPrefab.costBlue[0])
        { blueCostText[1].color = new Color(1, 0, 0); }
        else blueCostText[1].color = new Color(1, 1, 1);

        if ((rssBlue) < forcefieldPrefab.costBlue[0])
        { blueCostText[2].color = new Color(1, 0, 0); }
        else blueCostText[2].color = new Color(1, 1, 1);

        if ((rssBlue) < pulsePrefab.costBlue[0])
        { blueCostText[3].color = new Color(1, 0, 0); }
        else blueCostText[3].color = new Color(1, 1, 1);

        

    }

   
}
