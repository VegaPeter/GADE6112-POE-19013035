using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardUnit : Unit
{
    public string Name
    {
        get { return base.name; }
    }

    public int PosX
    {
        get { return base.posX; }
        set { base.posX = value; }
    }

    public int PosY
    {
        get { return base.posY; }
        set { posY = value; }
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

    public int Speed
    {
        get { return base.speed; }
    }

    public int Attack
    {
        get { return base.attack; }
    }

    public int AttackRange
    {
        get { return base.attackRange; }
    }

    public string Symbol
    {
        get { return base.symbol; }
    }

    public int FactionType
    {
        get { return base.factionType; }
    }

    public bool IsAttacking
    {
        get { return base.isAttacking; }
        set { base.isAttacking = value; }
    }

    private int mapHeight;

    public int MapHeight
    {
        get { return mapHeight; }
        set { mapHeight = value; }
    }

    private int mapWidth;

    public int MapWidth
    {
        get { return mapWidth; }
        set { mapWidth = value; }
    }

    private int speedCounter = 1;
    List<Unit> units = new List<Unit>();
    Unit closestUnit;

    //Constructor that sends all parameters to the unit constructor
    public WizardUnit(string n, int x, int y, int faction, int hp, int sp, int att, int attRange, bool isAtt)
        : base(n, x, y, hp, sp, att, attRange, faction, isAtt)
    {
        factionType = 3;
    }

    //Changes the x and y position towards the closest enemy or to run away
    public override void Move(int type)
    {
        //Moves towards closest enemey
        if (Health > MaxHealth * 0.5)
        {
            if (type == 0)
            {
                if (closestUnit is MeleeUnit)
                {
                    MeleeUnit closestUnitM = (MeleeUnit)closestUnit;

                    if (closestUnitM.PosX > posX && PosX < MapWidth - 1)
                    {
                        posX++;
                    }
                    else if (closestUnitM.PosX < posX && posX > 0)
                    {
                        posX--;
                    }

                    if (closestUnitM.PosY > posY && PosY < MapHeight - 1)
                    {
                        posY++;
                    }
                    else if (closestUnitM.PosY < posY && posY > 0)
                    {
                        posY--;
                    }
                }
                else if (closestUnit is RangedUnit)
                {
                    RangedUnit closestUnitR = (RangedUnit)closestUnit;

                    if (closestUnitR.PosX > posX && PosX < MapWidth - 1)
                    {
                        posX++;
                    }
                    else if (closestUnitR.PosX < posX && posX > 0)
                    {
                        posX--;
                    }

                    if (closestUnitR.PosY > posY && PosY < MapHeight - 1)
                    {
                        posY++;
                    }
                    else if (closestUnitR.PosY < posY && posY > 0)
                    {
                        posY--;
                    }
                }
            }
        }
        else //Moves in random direction to run away
        {
            int direction = Random.Range(0, 4);

            if (direction == 0 && PosX < MapHeight - 1)
            {
                posX++;
            }
            else if (direction == 1 && posX > 0)
            {
                posX--;
            }
            else if (direction == 2 && posY < MapWidth - 1)
            {
                posY++;
            }
            else if (direction == 3 && posY > 0)
            {
                posY--;
            }
        }
    }

    //Deals damage to closest unit if they are in attack range
    public override void Combat(int type)
    {
        foreach (Unit u in units)
        {
            if (u is MeleeUnit)
            {
                MeleeUnit M = (MeleeUnit)u;

                if (M.PosX == PosX - 1 && M.PosY == PosY - 1)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX && M.PosY == PosY - 1)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX + 1 && M.PosY == PosY - 1)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX - 1 && M.PosY == PosY)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX + 1 && M.PosY == PosY)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX - 1 && M.PosY == PosY + 1)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX && M.PosY == PosY + 1)
                {
                    M.Health -= 1;
                }
                else if (M.PosX == PosX + 1 && M.PosY == PosY + 1)
                {
                    M.Health -= 1;
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit R = (RangedUnit)u;

                if (R.PosX == PosX - 1 && R.PosY == PosY - 1)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX && R.PosY == PosY - 1)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX + 1 && R.PosY == PosY - 1)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX - 1 && R.PosY == PosY)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX + 1 && R.PosY == PosY)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX - 1 && R.PosY == PosY + 1)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX && R.PosY == PosY + 1)
                {
                    R.Health -= 1;
                }
                else if (R.PosX == PosX + 1 && R.PosY == PosY + 1)
                {
                    R.Health -= 1;
                }
            }
        }
    }

    //Checks to see if they are below 25% health so they move rather than attacking
    public override void CheckAttackRange(List<Unit> uni, List<Building> build)
    {
        units = uni;

        closestUnit = ClosestEnemy();

        int enemyType = 0;

        int xDis = 0, yDis = 0;

        int uDistance = 10000;
        int distance = 1000;

        if (closestUnit is MeleeUnit)
        {
            MeleeUnit M = (MeleeUnit)closestUnit;
            xDis = Mathf.Abs((PosX - M.PosX) * (PosX - M.PosX));
            yDis = Mathf.Abs((PosY - M.PosY) * (PosY - M.PosY));

            uDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }
        else if (closestUnit is RangedUnit)
        {
            RangedUnit R = (RangedUnit)closestUnit;
            xDis = Mathf.Abs((PosX - R.PosX) * (PosX - R.PosX));
            yDis = Mathf.Abs((PosY - R.PosY) * (PosY - R.PosY));

            uDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }

        if (uDistance < distance)
        {
            distance = uDistance;
            enemyType = 0;
        }

        //Checks to see if they are below 25% health so they move rather than attacking
        if (Health > MaxHealth * 0.5)
        {
            if (distance <= AttackRange)
            {
                IsAttacking = true;
                Combat(enemyType);
            }
            else
            {
                IsAttacking = false;
                Move(enemyType);
            }
        }
        else
        {
            Move(enemyType);
        }
    }

    //finds and returns the closest enemy
    public override Unit ClosestEnemy()
    {
        int xDis = 0, yDis = 0;
        double distance = 1000;
        double temp = 1000;
        Unit target = null;


        foreach (Unit u in units)
        {
            if (u is RangedUnit)
            {
                RangedUnit b = (RangedUnit)u;

                if (FactionType != b.FactionType)
                {
                    xDis = Mathf.Abs((PosX - b.PosX) * (PosX - b.PosX));
                    yDis = Mathf.Abs((PosY - b.PosY) * (PosY - b.PosY));

                    distance = Mathf.Round(Mathf.Sqrt(xDis + yDis));
                }
            }
            else if (u is MeleeUnit)
            {
                MeleeUnit b = (MeleeUnit)u;

                if (FactionType != b.FactionType)
                {
                    xDis = Mathf.Abs((PosX - b.PosX) * (PosX - b.PosX));
                    yDis = Mathf.Abs((PosY - b.PosY) * (PosY - b.PosY));

                    distance = Mathf.Round(Mathf.Sqrt(xDis + yDis));
                }
            }
            else if (u is WizardUnit)
            {
                distance = 1000;
            }


            if (distance < temp)
            {
                temp = distance;
                target = u;
            }
        }

        return target;
    }

    //Checks and returns if the unit is below or at 0 health
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

    //Returns the units information
    public override string ToString()
    {
        return Name + "\nX: " + PosX
            + " Y: " + PosY
            + "\nMax Health: " + MaxHealth
            + "\nHealth: " + Health
            + "\nSpeed: " + Speed
            + "\nAttack Damage " + Attack
            + "\nAttack Range: " + AttackRange
            + "\nFaction: " + FactionType
            + "\nAttacking: " + IsAttacking;
    }
}
