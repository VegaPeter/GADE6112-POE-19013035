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
            if (Random.Range(0, 3) == 0) //Generate Melee Unit
            {
                MeleeUnit m = new MeleeUnit(Random.Range(0, width),
                                            Random.Range(0, height),
                                            100,
                                            1,
                                            25,
                                            (i % 2 == 0 ? 1 : 0));
                units.Add(m);
            }
            else if (Random.Range(0, 3) == 1) // Generate Ranged Unit
            {
                RangedUnit ru = new RangedUnit(Random.Range(0, width),
                                            Random.Range(0, height),
                                            75,
                                            2,
                                            20,
                                            5,
                                            (i % 2 == 0 ? 1 : 0));
                units.Add(ru);
            }

        }
        for(int l = 0; l < 6; l++)
        {
           WizardUnit wu = new WizardUnit(Random.Range(0, width),
                                          Random.Range(0, height),
                                          50,
                                          5,
                                          50,
                                          1,
                                          3);
           units.Add(wu);
        }

        for (int k = 0; k < numBuildings; k++)
        {
            if (Random.Range(0, 2) == 0) //Generate Resource Building
            {
                ResourceBuilding rb = new ResourceBuilding(Random.Range(0, width),
                                                           Random.Range(0, height),
                                                           150,
                                                           (k % 2 == 0 ? 1 : 0),
                                                           false);
                buildings.Add(rb);
            }
            else //Generate Unit Building
            {
                FactoryBuilding fb = new FactoryBuilding(Random.Range(0, width),
                                                         Random.Range(0, height),
                                                         200,
                                                         (k % 2 == 0 ? 1 : 0),
                                                         false,
                                                         (Random.Range(0, 2) == 1 ? "Melee" : "Ranged"));

                buildings.Add(fb);
            }
        }
    }

    //Displays the units onto the form
   /* public void Display(/*Have gameMap or whatevs)
    {
        //Clears map
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

        foreach(GameObject u in tiles)
        {
            GameObject.Destroy(u);
        }
        


        //Adding Units
        foreach (Unit u in units)
        {
            GameObject gb = new GameObject();
            if (u is MeleeUnit)
            {
                MeleeUnit mu = (MeleeUnit)u;
                gb.Size = new Size(30, 30);
                gb.Location = new Point(mu.XPos * 30, mu.YPos * 30);
                if (mu.Faction == 0)
                {
                    gb.ForeColor = Color.HotPink;
                }
                else
                {
                    gb.ForeColor = Color.Blue;
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                gb.Size = new Size(30, 30);
                gb.Location = new Point(ru.XPos * 30, ru.YPos * 30);
                if (ru.Faction == 0)
                {
                    gb.ForeColor = Color.HotPink;
                }
                else
                {
                    gb.ForeColor = Color.Blue;
                }
            }
            else
            {
                WizardUnit wu = (WizardUnit)u;
                gb.Size = new Size(30, 30);
                gb.Location = new Point(wu.XPos * 30, wu.YPos * 30);
                gb.ForeColor = Color.Black;
            }

            gb.Click += Unit_Click;
            groupBox.Controls.Add(gb);
        }

        //Adding Buildings
        foreach (Building bud in buildings)
        {
            GameObject gb = new GameObject();

            if (bud is ResourceBuilding)
            {
                ResourceBuilding rb = (ResourceBuilding)bud;

                gb.Size = new Size(30, 30);
                gb.Location = new Point(rb.XPos * 30, rb.YPos * 30);
                gb.Text = rb.Symbol;
                if (rb.Faction == 0)
                {
                   gb.ForeColor = Color.HotPink;
                }
                else
                {
                    gb.ForeColor = Color.Blue;
                }
            }
            else
            {
                FactoryBuilding fb = (FactoryBuilding)bud;

                gb.Size = new Size(30, 30);
                gb.Location = new Point(fb.XPos * 30, fb.YPos * 30);
                if (fb.Faction == 0)
                {
                    gb.ForeColor = Color.HotPink;
                }
                else
                {
                    gb.ForeColor = Color.Blue;
                }

            }

            gb.Click += Building_Click;
            groupBox.Controls.Add(b);
        }
    }

    //Adds a unit's info to the ToString
    public void Unit_Click()
    {
        int x, y;
        GameObject gb = (GameObject)sender;
        x = gb.Location.X / 30;
        y = gb.Location.Y / 30;

        foreach (Unit u in units)
        {
            if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                if (ru.XPos == x && ru.YPos == y)
                {
                    txtInfo.Text = "";
                    txtInfo.Text = ru.ToString();
                }
            }
            else if (u is MeleeUnit)
            {
                MeleeUnit mu = (MeleeUnit)u;
                if (mu.XPos == x && mu.YPos == y)
                {
                    txtInfo.Text = "";
                    txtInfo.Text = mu.ToString();
                }
            }
            else if (u is WizardUnit)
            {
                WizardUnit wu = (WizardUnit)u;
                if (wu.XPos == x && wu.YPos == y)
                {
                    txtInfo.Text = "";
                    txtInfo.Text = wu.ToString();
                }
            }
        }
    }

    //Adds a building's info to the ToString
    public void Building_Click()
    {
        int x, y;

        GameObject gb = (GameObject)sender;

        x = gb.Location.X / 30;
        y = gb.Location.Y / 30;

        foreach (Building bud in buildings)
        {
            if (bud is ResourceBuilding)
            {
                ResourceBuilding rb = (ResourceBuilding)bud;
                if (rb.XPos == x && rb.YPos == y)
                {
                    txtInfo.Text = "";
                    txtInfo.Text = rb.ToString();
                }
            }
            else if (bud is FactoryBuilding)
            {
                FactoryBuilding fb = (FactoryBuilding)bud;
                if (fb.XPos == x && fb.YPos == y)
                {
                    txtInfo.Text = "";
                    txtInfo.Text = fb.ToString();
                }
            }
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
