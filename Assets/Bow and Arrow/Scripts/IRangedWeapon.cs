using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon
{
    void StartShotCount(float timer);
    int GetShotCount();
}
