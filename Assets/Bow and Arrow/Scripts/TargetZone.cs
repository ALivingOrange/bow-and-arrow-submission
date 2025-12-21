using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour, ITargetManager
{
    [Header("Prefabs")]
    public GameObject targetPrefab; // Prefab containing spawned targets

    [Header("Objects")]
    public GameObject player;

    [Header("Settings")]
    public float targetFrequency; // How often targets should spawn
    public int maxTargets; // When there are this many targets active, no more will be spawned


    private List<ITarget> activeTargets = new List<ITarget>();
    private int score;
    private bool gameActive = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(int gameTime)
    {
        if (gameActive)
        {
            Debug.LogError("Tried to start game while already active");
            return;
        }
        activeTargets.Clear();
        StartCoroutine(TargetSpawnLoop(gameTime));
        Invoke("EndGame", gameTime);
    }

    IEnumerator TargetSpawnLoop(int gameTime)
    {
        for(int i = (int)(gameTime/targetFrequency); i > 0; i--) // As many runs as fits in the game time
        {
            if (activeTargets.Count < maxTargets)
            {
                ITarget curTarget = SpawnTarget();

                curTarget.SetIndexInManager(activeTargets.Count);
                curTarget.SetManager(this.GetComponent<ITargetManager>());
                activeTargets.Add(curTarget);
            }
            yield return new WaitForSeconds(targetFrequency);
        }

        ITarget SpawnTarget()
        {
            GameObject target = Instantiate(targetPrefab);
            Vector3 newPosition = this.transform.position;


            // Spawn position in a dome shape
            newPosition.x += Random.Range(-5.0f, 5.0f);
            newPosition.z += Random.Range(-5.0f, 5.0f);

            newPosition.y = UnityEngine.Mathf.Sqrt(50 - UnityEngine.Mathf.Pow(newPosition.x, 2) - UnityEngine.Mathf.Pow(newPosition.z, 2));

            target.transform.position = newPosition;
            target.transform.LookAt(player.transform.position);
            target.transform.Rotate(85, 0, 0);
            return target.GetComponent<ITarget>();
        }
    }

    void EndGame()
    {
        score = 0;
        ClearAllTargets();
    }

    void ClearAllTargets()
    {
        foreach (ITarget target in activeTargets)
        {
            target.Die();
        }
        activeTargets.Clear();
    }

    public void TargetDestroyed(int index)
    {
        if (index < 0) return;

        // increment score 
        score++;

        // Swap to-be-destroyed index with furthest index
        int lastIndex = activeTargets.Count - 1;
        activeTargets[index] = activeTargets[lastIndex];

        // Let last index know it just got swapped
        activeTargets[index].SetIndexInManager(index);

        activeTargets.RemoveAt(lastIndex);

    }

    public int GetScore() { return score; }
}
