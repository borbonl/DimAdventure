using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public PlayerController PC;

    void OnMouseDown()
    {
        PC.shoot();
    }

}
