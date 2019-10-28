using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public Text Health;

    public void DisplayHealth(int hp)
    {
        Health.text = hp.ToString();
    }
}
