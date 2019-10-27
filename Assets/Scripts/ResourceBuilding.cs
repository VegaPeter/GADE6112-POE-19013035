using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    //Constructor that catches all the variables to use in their own class
    public ResourceBuilding(int x, int y, int h, int f, bool des)
    {
        xPos = x;
        yPos = y;
        health = h;
        faction = f;
    }

    //Additional variables to be used in the class
    private string resourceType = "Grog";
    private int resourcesGenerated = 0;
    private int resourcesGeneratedPerRound;
    private int resourcePool = 100;

    //Fields that the class requires access to 
    public int XPos
    {
        get { return base.xPos; }
        set { base.xPos = value; }
    }

    public int YPos
    {
        get { return base.yPos; }
        set { base.yPos = value; }
    }

    public int Health
    {
        get { return base.health; }
        set { base.health = value; }
    }

    public int MaxHealth
    {
        get { return base.health; }
    }

    public int Faction
    {
        get { return base.faction; }
        set { base.faction = value; }
    }
    public bool IsDestroyed { get; set; }

    public override bool Destruction()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Method that handles the round to round changes for the building
    public int ResourceTick()
    {
        resourceType = "Grog"; //Resource name
        resourcesGeneratedPerRound = 10; //Resource per tick
        resourcePool -= resourcesGeneratedPerRound; //Leftover resources
        resourcesGenerated += resourcesGeneratedPerRound; //Total resources in stockpile

        return resourcesGenerated;
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
