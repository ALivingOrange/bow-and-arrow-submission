using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ITargetManager
{
    void StartGame(int gameTime);
    void TargetDestroyed(int index);
    int GetScore();
    int GetMisses();
    event Action OnGameEnd;
}