using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetManager
{
    void TargetDestroyed(int index);
    int GetScore();
}