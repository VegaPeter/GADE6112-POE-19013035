using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //Declares list to hold the units and buildings
    public List<Unit> units = new List<Unit>();
    public List<Building> buildings = new List<Building>();

    //Declares random class and other useful variables
    public int numUnits = 0;
    public int numBuildings = 0;
    public int height,
               width;

    //Fields 
    public List<Unit> Units
    {
        get { return units; }
        set { units = value; }
    }

    public List<Building> Buildings
    {
        get { return buildings; }
        set { buildings = value; }
    }

    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    //Constructor
    public Map(int n, int noBuilds, int hght, int wdth)
    {
        units = new List<Unit>();
        buildings = new List<Building>();
        numUnits = n;
        numBuildings = noBuilds;
        height = hght;
        width = wdth;
    }

    //Handles generation of the units
    public void Generate()
    {
        //PvP Teams
        for (int i = 0; i < numUnits; i++)
        {
            int holdMe;
            holdMe = Random.Range(0, 2);

            if (holdMe == 0) //Generate Melee Unit
            {
                MeleeUnit m = new MeleeUnit("Barbarian",
                                            Random.Range(0, width),
                                            Random.Range(0, height),
                                            (i % 2 == 0 ? 1 : 0),
                                            10,
                                            1,
                                            5,
                                            1,
                                            false
                                            );
                units.Add(m);
            }
            else if (holdMe == 1) // Generate Ranged Unit
            {
                RangedUnit ru = new RangedUnit("Tank",
                                            Random.Range(0, width),
                                            Random.Range(0, height),
                                            (i % 2 == 0 ? 1 : 0),
                                            7,
                                            1,
                                            3,
                                            2,
                                            false
                                            );
                units.Add(ru);
            }
            else
            {
                WizardUnit wu = new WizardUnit("Lich",
                                 Random.Range(0, width),
                                 Random.Range(0, height),
                                 3,
                                 6,
                                 1,
                                 5,
                                 1,
                                 false
                                 );
                units.Add(wu);
            }
        }

        for(int l = 0; l < 6; l++)
        {
           WizardUnit wu = new WizardUnit("Lich",
                                            Random.Range(0, width),
                                            Random.Range(0, height),
                                            3,
                                            6,
                                            1,
                                            5,
                                            1,
                                            false
                                            );
            units.Add(wu);
        }

        for (int k = 0; k < numBuildings; k++)
        {
            if (Random.Range(0, 2) == 0) //Generate Resource Building
            {
                ResourceBuilding rb = new ResourceBuilding(Random.Range(0, width),
                                                           Random.Range(0, height),
                                                           20,
                                                           (k % 2 == 0 ? 1 : 0),
                                                           3);
                buildings.Add(rb);
            }
            else //Generate Unit Building
            {
                FactoryBuilding fb = new FactoryBuilding(Random.Range(0, width),
                                                         Random.Range(0, height),
                                                         20,
                                                         (k % 2 == 0 ? 1 : 0),
                                                         10,
                                                         (Random.Range(0, 2) == 1 ? "Melee" : "Ranged"),
                                                         100);

                buildings.Add(fb);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
