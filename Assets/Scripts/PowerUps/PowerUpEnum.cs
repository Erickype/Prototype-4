using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEnum : MonoBehaviour
{
    public enum PowerUp { Normal, HomingRocket }

    public PowerUp powerUpSelector;

    // Start is called before the first frame update
    void Start()
    {
        switch (powerUpSelector)
        {
            case PowerUp.Normal:
                break;
            case PowerUp.HomingRocket:
                break;
        }
    }

}
