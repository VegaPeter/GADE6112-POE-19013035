using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public int PosX
    {
        get { return base.posX; }
        set { base.posX = value; }
    }

    public int PosY
    {
        get { return base.posY; }
        set { base.posY = value; }
    }

    public int Health
    {
        get { return base.health; }
        set { base.health = value; }
    }

    public string Symbol
    {
        get { return base.symbol; }
    }

    public int FactionType
    {
        get { return base.factionType; }
    }

    public int ResresourcesGenerated
    {
        get { return resourcesGenerated; }
    }

    public int ResresourcesLeft
    {
        get { return resourcesRemaining; }
    }
    private int resourcesGenerated = 0;
    private int resourcesPerRound;
    private int resourcesRemaining = 1000;


    public ResourceBuilding(int x, int y, int hp, int faction, int resPerRound)
        : base(x, y, hp, faction)
    {
        resourcesPerRound = resPerRound;
    }

    //Mines the resources adding them and then removing them from the resources left
    public int GenerateResource()
    {
        resourcesGenerated = 0;

        if (resourcesRemaining > 10)
        {
            resourcesGenerated += resourcesPerRound;
            resourcesRemaining -= resourcesPerRound;
        }
        else if (resourcesRemaining > 0)
        {
            resourcesGenerated += resourcesRemaining;
            resourcesRemaining = 0;
        }

        return resourcesGenerated;
    }

    //Returns if the building has more than 0 health of not
    public override bool Death()
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
