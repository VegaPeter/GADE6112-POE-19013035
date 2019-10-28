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
    public Text txtRadientResourseLeft;
    public Text txtRadientResourseGathered;
    public Text txtRadientUnits;
    //Dire texts
    public Text txtDireResourseLeft;
    public Text txtDireResourseGathered;
    public Text txtDireUnits;

    public Text txtWinText;

    //Variables that can be adjusted by the user to change the map size
    const int mapHeight = 20;
    const int mapWidth = 20;

    //Variables to indacate how many resources each team has
    int direResources = 0;
    int direResourcesLeft;

    int radientResources = 0;
    int radientResourcesLeft;

    //Variables to adjust how many units and buildings will spawn
    static int unitNum = 8;
    static int buildingNum = 6;

    int dire = 0;
    int radiant = 0;

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
        map = new Map(36, 2, 20, 20);
        map.Generate();
            foreach (Building B in map.buildings)
            {
                ResourceBuilding RB = (ResourceBuilding)B;
                if (RB.FactionType == 0)
                {
                    direResourcesLeft += RB.ResresourcesLeft;
                }
                else if (RB.FactionType == 1)
                {
                    radientResourcesLeft += RB.ResresourcesLeft;
                }
            }
         Display();
         round = 1;
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
        Display();

        //Working out if both teams are alive
        dire = 0;
        radiant = 0;

        foreach (Building B in map.buildings)
        {
            if (B is ResourceBuilding)
            {
                ResourceBuilding RB = (ResourceBuilding)B;
                if (RB.FactionType == 0)
                {
                    dire++;
                }
                else
                {
                    radiant++;
                }
            }
            else
            {
                FactoryBuilding FB = (FactoryBuilding)B;
                if (FB.FactionType == 0)
                {
                    dire++;
                }
                else
                {
                    radiant++;
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
                    dire++;
                }
                else
                {
                    radiant++;
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                if (ru.FactionType == 0)
                {
                    dire++;
                }
                else
                {
                    radiant++;
                }
            }
            else
            {
            }
        }

        if (dire > 0 && radiant > 0)//Checks to see if both teams are still alive
        {
            //Reset resource values
            direResourcesLeft = 0;
            radientResourcesLeft = 0;

            foreach (Building b in map.buildings)
            {
                if (b is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)b;
                    if (RB.FactionType == 0)
                    {
                        direResources += RB.GenerateResource();
                        direResourcesLeft += RB.ResresourcesLeft;
                    }
                    else if (RB.FactionType == 1)
                    {
                        radientResources += RB.GenerateResource();
                        radientResourcesLeft += RB.ResresourcesLeft;
                    }
                }
                else
                {
                    FactoryBuilding FB = (FactoryBuilding)b;
                    Unit u = FB.SpawnUnit();

                    if (FB.FactionType == 0 && direResources > FB.SpawnCost)
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
                            direResources -= FB.SpawnCost;

                        }
                    }
                    else if (FB.FactionType == 1 && radientResources > FB.SpawnCost)
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
                            radientResources -= FB.SpawnCost;
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

            if (dire > radiant)
            {
                txtWinText.text = "Dire Wins!";
            }
            else
            {
                txtWinText.text = "Radient Wins!";
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
    }

    //Displays the units onto the form
    public void Display(/*Have gameMap or whatevs*/)
    {
        for(int k = 0; k < 20; k++)
        {
            for (int l = 0; l < 20; l++)
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
