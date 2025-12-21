using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetManager
{
    void StartGame(int gameTime);
    void TargetDestroyed(int index);
    int GetScore();
}