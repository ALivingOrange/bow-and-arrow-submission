using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
    private ITargetManager manager = null;
    private int indexInManager = -1;

    public float disappearAfter = 5f; // Seconds to disappear after being hit
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetManager(ITargetManager newManager)
    {
        manager = newManager;

    }

    public void SetIndexInManager(int index)
    {
        indexInManager = index;
    }

    public void OnHit()
    {
        // Change color to indicate hit
        if (TryGetComponent<Renderer>(out Renderer objectRenderer)) objectRenderer.material.color = Color.blue;
        else Debug.LogWarning($"Target '{this}' renderer not found ");

        // Inform Manager of doom, then destroy self
        if ((manager != null) && (indexInManager != -1)) manager.TargetDestroyed(indexInManager);
        indexInManager = -1;
        Invoke("Die", disappearAfter);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
