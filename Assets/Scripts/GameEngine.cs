using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    //Declarations for the GameEngine class
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
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        else if (a is RangedUnit && b is MeleeUnit)
        {
            RangedUnit start = (RangedUnit)a;
            MeleeUnit end = (MeleeUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        else if (a is RangedUnit && b is RangedUnit)
        {
            RangedUnit start = (RangedUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        else if (a is MeleeUnit && b is RangedUnit)
        {
            MeleeUnit start = (MeleeUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        //Add wizards
        else if (a is WizardUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            WizardUnit end = (WizardUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        else if (a is MeleeUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            MeleeUnit end = (MeleeUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }
        else if (a is RangedUnit && b is WizardUnit)
        {
            WizardUnit start = (WizardUnit)a;
            RangedUnit end = (RangedUnit)b;
            distance = Mathf.Abs(start.XPos - end.XPos) + Mathf.Abs(start.YPos - end.YPos);
        }

        return distance;
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new Map(36, 2, 20, 20);
        map.Generate();
        Display();

        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp == 30)
        {
            GameLogic();
            temp = 0;
        }
        else
        {
            temp++;
        }
    }

    public void GameLogic()
    {
        CheckDeath();
        for (int i = 0; i < map.Units.Count; i++)
        {
            if (map.Units[i] is MeleeUnit)
            {
                MeleeUnit mu = (MeleeUnit)map.Units[i];
                if (mu.Health <= mu.MaxHealth * 0.25) // Running Away
                {
                    mu.Move(Random.Range(0, 4));
                }
                else
                {
                    (Unit closest, int distanceTo) = mu.Closest(map.Units);
                    (Building buildingclosest, int distanceToBuilding) = mu.BuildingClosest(map.Buildings);

                    if (distanceTo > distanceToBuilding)
                    {
                        //Check In Range
                        if (distanceTo <= mu.AttackRange)
                        {
                            mu.IsAttacking = true;
                            mu.Combat(closest);
                        }
                        else //Move Towards
                        {
                            if (closest is MeleeUnit)
                            {
                                MeleeUnit closestMu = (MeleeUnit)closest;
                                if (mu.XPos > closestMu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestMu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestMu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestMu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                            else if (closest is RangedUnit)
                            {
                                RangedUnit closestRu = (RangedUnit)closest;
                                if (mu.XPos > closestRu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestRu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestRu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestRu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Check In Range
                        if (distanceTo <= mu.AttackRange)
                        {
                            mu.IsAttacking = true;
                            mu.Combat(closest);
                        }
                        else //Move Towards
                        {
                            if (buildingclosest is FactoryBuilding)
                            {
                                FactoryBuilding closestMu = (FactoryBuilding)buildingclosest;
                                if (mu.XPos > closestMu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestMu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestMu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestMu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                            else if (buildingclosest is ResourceBuilding)
                            {
                                ResourceBuilding closestRu = (ResourceBuilding)buildingclosest;
                                if (mu.XPos > closestRu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestRu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestRu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestRu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                        }
                    }

                }
            }
            else if (map.Units[i] is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)map.Units[i];
                if (ru.Health == 0)
                {
                    ru.Move(-1);
                    
                }
                else if (ru.Health <= ru.MaxHealth * 0.25)
                {
                    ru.Move(Random.Range(0, 4));
                }
                else
                {
                    (Unit closest, int distanceTo) = ru.Closest(map.Units);
                    (Building buildingclosest, int distanceToBuilding) = ru.BuildingClosest(map.Buildings);

                    if (distanceTo > distanceToBuilding)
                    {
                        //Check In Range
                        if (distanceTo <= ru.AttackRange)
                        {
                            ru.IsAttacking = true;
                            ru.Combat(closest);
                        }
                        else //Move Towards
                        {
                            if (closest is MeleeUnit)
                            {
                                MeleeUnit closestMu = (MeleeUnit)closest;
                                if (ru.XPos > closestMu.XPos) //North
                                {
                                    ru.Move(0);
                                }
                                else if (ru.XPos < closestMu.XPos) //South
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > closestMu.YPos) //West
                                {
                                    ru.Move(3);
                                }
                                else if (ru.YPos < closestMu.YPos) //East
                                {
                                    ru.Move(1);
                                }
                            }
                            else if (closest is RangedUnit)
                            {
                                RangedUnit closestRu = (RangedUnit)closest;
                                if (ru.XPos > closestRu.XPos) //North
                                {
                                    ru.Move(0);
                                }
                                else if (ru.XPos < closestRu.XPos) //South
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > closestRu.YPos) //West
                                {
                                    ru.Move(3);
                                }
                                else if (ru.YPos < closestRu.YPos) //East
                                {
                                    ru.Move(1);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Check In Range
                        if (distanceTo <= ru.AttackRange)
                        {
                            ru.IsAttacking = true;
                            ru.Combat(closest);
                        }
                        else //Move Towards
                        {
                            if (buildingclosest is FactoryBuilding)
                            {
                                FactoryBuilding closestMu = (FactoryBuilding)buildingclosest;
                                if (ru.XPos > closestMu.XPos) //North
                                {
                                    ru.Move(0);
                                }
                                else if (ru.XPos < closestMu.XPos) //South
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > closestMu.YPos) //West
                                {
                                    ru.Move(3);
                                }
                                else if (ru.YPos < closestMu.YPos) //East
                                {
                                    ru.Move(1);
                                }
                            }
                            else if (buildingclosest is ResourceBuilding)
                            {
                                ResourceBuilding closestRu = (ResourceBuilding)buildingclosest;
                                if (ru.XPos > closestRu.XPos) //North
                                {
                                    ru.Move(0);
                                }
                                else if (ru.XPos < closestRu.XPos) //South
                                {
                                    ru.Move(2);
                                }
                                else if (ru.YPos > closestRu.YPos) //West
                                {
                                    ru.Move(3);
                                }
                                else if (ru.YPos < closestRu.YPos) //East
                                {
                                    ru.Move(1);
                                }
                            }
                        }
                    }

                }
            }
            else if (map.Units[i] is WizardUnit)
            {
                WizardUnit wu = (WizardUnit)map.Units[i];
                if (wu.Health == 0)
                {
                    wu.Move(-1);
                }
                else if (wu.Health <= wu.MaxHealth * 0.5) // Running Away
                {
                    wu.Move(Random.Range(0, 4));
                }
                else
                {
                    (Unit closest, int distanceTo) = wu.Closest(map.Units);

                    //Check In Range
                    if (distanceTo <= wu.AttackRange)
                    {
                        wu.IsAttacking = true;
                        wu.Combat(closest);
                    }
                    else //Move Towards
                    {
                        if (closest is MeleeUnit)
                        {
                            MeleeUnit closestMu = (MeleeUnit)closest;
                            if (wu.XPos > closestMu.XPos) //North
                            {
                                wu.Move(0);
                            }
                            else if (wu.XPos < closestMu.XPos) //South
                            {
                                wu.Move(2);
                            }
                            else if (wu.YPos > closestMu.YPos) //West
                            {
                                wu.Move(3);
                            }
                            else if (wu.YPos < closestMu.YPos) //East
                            {
                                wu.Move(1);
                            }
                        }
                        else if (closest is RangedUnit)
                        {
                            RangedUnit closestRu = (RangedUnit)closest;
                            if (wu.XPos > closestRu.XPos) //North
                            {
                                wu.Move(0);
                            }
                            else if (wu.XPos < closestRu.XPos) //South
                            {
                                wu.Move(2);
                            }
                            else if (wu.YPos > closestRu.YPos) //West
                            {
                                wu.Move(3);
                            }
                            else if (wu.YPos < closestRu.YPos) //East
                            {
                                wu.Move(1);
                            }
                        }
                    }

                }
            }
        }

        Display();
        round++;
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
                if (mu.Faction == 0)
                {
                    Instantiate(redMelee, new Vector3(mu.XPos, 0, mu.YPos), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueMelee, new Vector3(mu.XPos, 0, mu.YPos), Quaternion.identity);
                }
            }
            else if (u is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)u;
                if (ru.Faction == 0)
                {
                    Instantiate(redRanged, new Vector3(ru.XPos, 0, ru.YPos), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueRanged, new Vector3(ru.XPos, 0, ru.YPos), Quaternion.identity);
                }
            }
            else
            {
                WizardUnit wu = (WizardUnit)u;
                Instantiate(wizard, new Vector3(wu.XPos, 0, wu.YPos), Quaternion.identity);
            }

        }

        //Adding Buildings
        foreach (Building bud in map.buildings)
        {
            GameObject gb = new GameObject();

            if (bud is ResourceBuilding)
            {
                ResourceBuilding rb = (ResourceBuilding)bud;

                if (rb.Faction == 0)
                {
                    Instantiate(redResourceFactory, new Vector3(rb.XPos, 0, rb.YPos), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueResourceFactory, new Vector3(rb.XPos, 0, rb.YPos), Quaternion.identity);
                }
            }
            else
            {
                FactoryBuilding fb = (FactoryBuilding)bud;

                if (fb.Faction == 0)
                {
                    Instantiate(redUnitFactory, new Vector3(fb.XPos, 0, fb.YPos), Quaternion.identity);
                }
                else
                {
                    Instantiate(blueUnitFactory, new Vector3(fb.XPos, 0, fb.YPos), Quaternion.identity);
                }

            }
        }
    }   
}
