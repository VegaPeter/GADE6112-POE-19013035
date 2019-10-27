using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    //Variables for other classes to inherit
    protected int xPos,
                  yPos,
                  health,
                  maxHealth,
                  faction;

    //Abstract method for other buildings to inherit
    public abstract bool Destruction();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
