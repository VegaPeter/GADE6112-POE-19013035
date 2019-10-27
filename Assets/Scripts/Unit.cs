using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
        //Protected variables for other classes to inherit
        protected int xPos,
                      yPos,
                      health,
                      maxHealth,
                      speed,
                      attack,
                      attackRange,
                      faction;
        protected bool isAttacking;
        protected string name;

        //Abstract methods 
        public abstract void Move(int dir);
        public abstract void Combat(Unit attacker);
        public abstract bool InRange(Unit other, Building otherino);
        public abstract (Unit, int) Closest(List<Unit> units);
        public abstract bool Death();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
