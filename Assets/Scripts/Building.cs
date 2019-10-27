using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    protected int posX, posY;
    protected int health, maxHealth;
    protected int factionType;
    protected string symbol;

    public Building(int x, int y, int hp, int faction)
    {
        posX = x;
        posY = y;
        health = hp;

        factionType = faction;

        maxHealth = hp;
    }

    public abstract bool Death();
}
