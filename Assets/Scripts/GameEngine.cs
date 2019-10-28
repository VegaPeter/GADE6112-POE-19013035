using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour
{
    //Declarations for the GameEngine class
    bool runGame = false;
    private Map map;
    private int temp = 0;
    private int round;
    public GameObject redMelee,
                      redRanged,
                      redUnitFactory,
                      redResourceFactory,
                      wizard,
                      floor,
                      blueMelee,
                      blueRanged,
                      blueUnitFactory,
                      blueResourceFactory;

    //All texts for the UI
    public Text txtPausePlay;
    public Text txtRound;
    //Radient texts
    public Text txtBlueResourcesLeft;
    public Text txtBlueResourcesGathered;
    public Text txtBlueUnitsLeft;
    //Dire texts
    public Text txtRedResourcesLeft;
    public Text txtRedResourcesGathered;
    public Text txtRedUnitsLeft;

    public Text txtWinText;

    //Variables that can be adjusted by the user to change the map size
    const int mapHeight = 20;
    const int mapWidth = 20;

    //Variables to indacate how many resources each team has
    int redResources = 0;
    int redResourcesLeft;

    int blueResources = 0;
    int blueResourcesLeft;

    //Variables to adjust how many units and buildings will spawn
    //static int unitNum = 8;
    //static int buildingNum = 6;

    int red = 0;
    int blue = 0;

    //Fields
    public int Round
    {
        get { return round; }
    }

    //Determines the distance between two units
    public int DistanceTo(Unit a, Unit b)
    {
        int distance = 0;

        if (a is MeleeUnit && b is MeleeUnit)
        {
            MeleeUnit start = (MeleeUnit)a;
            MeleeUnit end = (MeleeUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        else if (a is RangedUnit && b is MeleeUnit)
        {
            RangedUnit start = (RangedUnit)a;
            MeleeUnit end = (MeleeUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        else if (a is RangedUnit && b is RangedUnit)
        {
            RangedUnit start = (RangedUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        else if (a is MeleeUnit && b is RangedUnit)
        {
            MeleeUnit start = (MeleeUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        //Add wizards
        else if (a is WizardUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            WizardUnit end = (WizardUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        else if (a is MeleeUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            MeleeUnit end = (MeleeUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }
        else if (a is RangedUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.PosX - end.PosX) + Mathf.Abs(start.PosY - end.PosY);
        }

        return distance;
    }
    public void ResourceDisplay()
    {
        txtRound.text = "Round: " + round;
        txtBlueUnitsLeft.text = "Blue Units Left: " + blue;
        txtRedUnitsLeft.text = "Red Units Left: " + red;

        txtBlueResourcesLeft.text = "Blue Resources Left: " + blueResourcesLeft;
        txtRedResourcesLeft.text = "Red Resources Left: " + redResourcesLeft;

        txtBlueResourcesGathered.text = "Blue Resources Gathered: " + blueResources;
        txtRedResourcesGathered.text = "Red Resources Gathered: " + redResources;
    }

     public void PlayPause()
     {
        if (runGame == false)
        {
            runGame = true;
            txtPausePlay.text = "Pause";
        }
        else
        {
            runGame = false;
            txtPausePlay.text = "Resume";
        }
     }

    // Start is called before the first frame update
    void Start()
    {
        map = new Map(16, 6, 15, 15);
        map.Generate();

            foreach (Building B in map.buildings)
            {
                if (B is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)B;
                    if (RB.FactionType == 0)
                    {
                        redResourcesLeft += RB.ResresourcesLeft;
                    }
                    else if (RB.FactionType == 1)
                    {
                        blueResourcesLeft += RB.ResresourcesLeft;
                    }
                }

            }
         Display();
         round = 0;
         ResourceDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        if (runGame == true)
        {
            if (temp == 20)
            {
                GameLogic();
                temp = 0;
            }
            else
            {
                temp++;
            }
        }

    }

    private void GameLogic()
    {
        ResourceDisplay();
        //Working out if both teams are alive
        red = 0;
        blue = 0;

        foreach (Building B in map.buildings)
        {
            if (B is ResourceBuilding)
            {
                ResourceBuilding RB = (ResourceBuilding)B;
                if (RB.FactionType == 0)
                {
                    red++;
                }
                else
                {
                    blue++;
                }
            }
            else
            {
                FactoryBuilding FB = (FactoryBuilding)B;
                if (FB.FactionType == 0)
                {
                    red++;
                }
                else
                {
                    blue++;
                }
            }
        }

        foreach (Unit u in map.Units)
        {
            if (u is MeleeUnit)
            {
                MeleeUnit mu = (MeleeUnit)u;
                if(mu.FactionType == 0)
                {
                    red++;
                }
                else
                {
                    blue++;
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                if (ru.FactionType == 0)
                {
                    red++;
                }
                else
                {
                    blue++;
                }
            }
            else
            {
            }
        }

        if (red > 0 && blue > 0)//Checks to see if both teams are still alive
        {
            //Reset resource values
            redResourcesLeft = 0;
            blueResourcesLeft = 0;

            foreach (Building b in map.buildings)
            {
                if (b is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)b;
                    if (RB.FactionType == 0)
                    {
                        redResources += RB.GenerateResource();
                        redResourcesLeft += RB.ResresourcesLeft;
                    }
                    else if (RB.FactionType == 1)
                    {
                        blueResources += RB.GenerateResource();
                        blueResourcesLeft += RB.ResresourcesLeft;
                    }
                }
                else
                {
                    FactoryBuilding FB = (FactoryBuilding)b;
                    Unit u = FB.SpawnUnit();

                    if (FB.FactionType == 0 && redResources > FB.SpawnCost)
                    {
                        if (round % FB.SpawnSpeed == 0)
                        {
                            map.units.Add(u);

                            if (u is MeleeUnit)
                            {
                                MeleeUnit M = (MeleeUnit)u;

                                M.MapHeight = mapHeight;
                                M.MapWidth = mapWidth;
                                map.Units.Add(M);
                            }
                            else if (u is RangedUnit)
                            {
                                RangedUnit R = (RangedUnit)u;

                                R.MapHeight = mapHeight;
                                R.MapWidth = mapWidth;
                                map.Units.Add(R);
                            }
                            redResources -= FB.SpawnCost;

                        }
                    }
                    else if (FB.FactionType == 1 && blueResources > FB.SpawnCost)
                    {
                        if (round % FB.SpawnSpeed == 0)
                        {

                            if (u is MeleeUnit)
                            {
                                MeleeUnit M = (MeleeUnit)u;

                                map.Units.Add(M);
                            }
                            else if (u is RangedUnit)
                            {
                                RangedUnit R = (RangedUnit)u;

                                map.Units.Add(R);
                            }
                            blueResources -= FB.SpawnCost;
                        }
                    }
                }
            }
            foreach (Unit u in map.units)
            {
                u.CheckAttackRange(map.units, map.buildings);
            }

            round++;
            CheckDeath();
            Display();
        }
        else
        {
            Display();
            runGame = false;

            if (red > blue)
            {
                txtWinText.text = "Red Wins!";
            }
            else
            {
                txtWinText.text = "Blue Wins!";
            }
        }
    }

    public void CheckDeath()
    {
        for(int i = 0; i < map.Units.Count; i++)
        {
            if(map.Units[i].Death() == true)
            {
                map.Units.RemoveAt(i);
            }
        }

        for(int k = 0; k < map.Buildings.Count; k++)
        {
            if(map.Buildings[k].Death() == true)
            {
                map.Buildings.RemoveAt(k);
            }
        }
    }

    //Displays the units onto the form
    public void Display()
    {
        for(int k = 0; k < 15; k++)
        {
            for (int l = 0; l < 15; l++)
            {
                Instantiate(floor, new Vector3(k, 0, l), Quaternion.identity);
            }
        }
        //Clears map
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("floor");

        foreach (GameObject u in tiles)
        {
            GameObject.Destroy(u);
        }

        //Adding Units
        foreach (Unit u in map.units)
        {
            GameObject gb = new GameObject();
            if (u is MeleeUnit)
            {
                MeleeUnit mu = (MeleeUnit)u;
                if (mu.FactionType == 0)
                {
                    Instantiate(redMelee, new Vector3(mu.PosX, 0, mu.PosY), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueMelee, new Vector3(mu.PosX, 0, mu.PosY), Quaternion.identity);
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                if (ru.FactionType == 0)
                {
                    Instantiate(redRanged, new Vector3(ru.PosX, 0, ru.PosY), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueRanged, new Vector3(ru.PosX, 0, ru.PosY), Quaternion.identity);
                }
            }
            else
            {
                WizardUnit wu = (WizardUnit)u;
                Instantiate(wizard, new Vector3(wu.PosX, 0, wu.PosY), Quaternion.identity);
            }

        }

        //Adding Buildings
        foreach (Building bud in map.buildings)
        {
            GameObject gb = new GameObject();

            if (bud is ResourceBuilding)
            {
                ResourceBuilding rb = (ResourceBuilding)bud;

                if (rb.FactionType == 0)
                {
                    Instantiate(redResourceFactory, new Vector3(rb.PosX, 0, rb.PosY), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueResourceFactory, new Vector3(rb.PosX, 0, rb.PosY), Quaternion.identity);
                }
            }
            else
            {
                FactoryBuilding fb = (FactoryBuilding)bud;

                if (fb.FactionType == 0)
                {
                    Instantiate(redUnitFactory, new Vector3(fb.PosX, 0, fb.PosY), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueUnitFactory, new Vector3(fb.PosX, 0, fb.PosY), Quaternion.identity);
                }

            }
        }
    }   
}
