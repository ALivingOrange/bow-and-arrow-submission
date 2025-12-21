using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    void SetManager(ITargetManager newManager); // If it needs to be counted
    void SetIndexInManager(int index); // For information
    void OnHit(); // To be activated when hit by a projectile
    void Die();
}