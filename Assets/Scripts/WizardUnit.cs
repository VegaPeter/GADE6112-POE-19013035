using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardUnit : Unit
{
    //Fields that the WizardUnit class needs access to
    public bool IsDead { get; set; }
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
        get { return base.maxHealth; }
    }
    public int Attack
    {
        get { return base.attack; }
        set { base.attack = value; }
    }
    public int AttackRange
    {
        get { return base.attackRange; }
        set { base.attackRange = value; }
    }
    public int Speed
    {
        get { return base.speed; }
        set { base.speed = value; }
    }
    public int Faction
    {
        get { return base.faction; }
    }
    public bool IsAttacking
    {
        get { return base.isAttacking; }
        set { base.isAttacking = value; }
    }
    public string Name
    {
        get { return base.name; }
        set { base.name = value; }
    }

    //WizardUnit Constructor
    public WizardUnit(int x, int y, int h, int s, int a, int ar, int f)
    {
        XPos = x;
        YPos = y;
        Health = h;
        base.maxHealth = h;
        Speed = s;
        Attack = a;
        AttackRange = ar;
        base.faction = f;
        Name = "Mage";
        IsAttacking = false;
        IsDead = false;
    }

    //OVERRIDE METHODS
    //Method to handle a units death
    public override bool Death()
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

    //Method to handle where the unit moves
    public override void Move(int dir)
    {
        if (dir == 0 && YPos > 0)
        {
            YPos--;
        }
        else if (dir == 1 && XPos < 9)
        {
            XPos++;
        }
        else if (dir == 2 && YPos < 9)
        {
            YPos++;
        }
        else if (dir == 3 && XPos > 0)
        {
            XPos--;
        }
    }

    //Method to handle a unit's combat
    public override void Combat(Unit attacker)
    {
        if (attacker is MeleeUnit)
        {
            Health = Health - ((MeleeUnit)attacker).Attack;
        }
        else if (attacker is RangedUnit)
        {
            RangedUnit ru = (RangedUnit)attacker;
            Health = Health - (ru.Attack - ru.AttackRange);
        }
        else if (attacker is WizardUnit)
        {
            //Health = Health - ((WizardUnit)attacker.Attack);
        }

        if (Health <= 0)
        {
            Death(); //Me_IRL
        }
    }

    //Method to determine which unit is closest and therefore should be attacked
    public override bool InRange(Unit other, Building otherino)
    {
        int distance = 0;
        int otherX = 0;
        int otherY = 0;
        if (other is MeleeUnit)
        {
            otherX = ((MeleeUnit)other).XPos;
            otherY = ((MeleeUnit)other).YPos;
        }
        else if (other is RangedUnit)
        {
            otherX = ((RangedUnit)other).XPos;
            otherY = ((RangedUnit)other).YPos;
        }
        else if (otherino is FactoryBuilding)
        {
            otherX = ((FactoryBuilding)otherino).XPos;
            otherY = ((FactoryBuilding)otherino).YPos;
        }
        else if (otherino is ResourceBuilding)
        {
            otherX = ((ResourceBuilding)otherino).XPos;
            otherY = ((ResourceBuilding)otherino).YPos;
        }

        distance = Mathf.Abs(XPos - otherX) + Mathf.Abs(YPos - otherY);
        if (distance <= AttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Handling additional distance calculations
    public override (Unit, int) Closest(List<Unit> units)
    {
        int shortest = 100;
        Unit closest = this;
        //Closest Unit and Distance                    
        foreach (Unit u in units)
        {
            if (u is MeleeUnit)
            {
                MeleeUnit otherMu = (MeleeUnit)u;
                int distance = Mathf.Abs(this.XPos - otherMu.XPos)
                           + Mathf.Abs(this.YPos - otherMu.YPos);
                if (distance < shortest)
                {
                    shortest = distance;
                    closest = otherMu;
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit otherRu = (RangedUnit)u;
                int distance = Mathf.Abs(this.XPos - otherRu.XPos)
                           + Mathf.Abs(this.YPos - otherRu.YPos);
                if (distance < shortest)
                {
                    shortest = distance;
                    closest = otherRu;
                }
            }
        }
        return (closest, shortest);
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
