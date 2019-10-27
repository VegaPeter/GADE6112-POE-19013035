using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Building
{

    //Declarations of variables
    protected string unitType;
    protected int productionSpeed;
    protected int spawnPoint;

    //Constructor for FactoryBuilding class
    public FactoryBuilding(int x, int y, int h, int f, bool des, string unT)
    {
        xPos = x;
        yPos = y;
        health = h;
        faction = f;
        unitType = unT;
    }

    //Fields
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


    public int ProductionSpeed
    {
        get { return productionSpeed; }
    }

    //Method for the buildings to generate units 
    public Unit BuildUnit(int factions)
    {
        if (faction == 1)
        {
            if (unitType == "Melee")
            {
                MeleeUnit m = new MeleeUnit(xPos,
                                            yPos--,
                                            100,
                                            1,
                                            20,
                                            1);
                return m;
            }
            else if (unitType == "Ranged")
            {
                RangedUnit ru = new RangedUnit(xPos,
                                            yPos--,
                                            100,
                                            1,
                                            20,
                                            5,
                                            1);
                return ru;
            }
        }
        else if (faction == 0)
        {
            if (unitType == "Melee")
            {
                MeleeUnit m = new MeleeUnit(xPos,
                                            yPos--,
                                            100,
                                            1,
                                            20,
                                            0);
                return m;
            }
            else if (unitType == "Ranged")
            {
                RangedUnit ru = new RangedUnit(xPos,
                                            yPos--,
                                            100,
                                            1,
                                            20,
                                            5,
                                            0);
                return ru;
            }
        }

        RangedUnit rb = new RangedUnit(xPos,
                                            yPos--,
                                            100,
                                            1,
                                            20,
                                            5,
                                            0);
        return rb;
    }

    //Method to destroy buildings
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
