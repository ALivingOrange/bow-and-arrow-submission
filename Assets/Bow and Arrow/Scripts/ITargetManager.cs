using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetManager
{
    void StartGame();
    void TargetDestroyed(int index);
    int GetScore();
}