using System.Collections;
using System.Collections.Generic;
using System;
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
    private int misses;
    private bool gameActive = false;

    public event Action OnGameEnd;

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
        misses = 0;
        score = 0;
        StartCoroutine(TargetSpawnLoop(gameTime));
        Invoke("EndGame", gameTime);
    }

    IEnumerator TargetSpawnLoop(int gameTime)
    {
        for(int i = (int)(gameTime/targetFrequency); i > 0; i--) // As many runs as fits in the game time
        {
            ITarget curTarget = SpawnTarget();
            curTarget.SetManager(this.GetComponent<ITargetManager>());

            if (activeTargets.Count == maxTargets) // if targets full, remove earliest (miss)
            {
                misses++;
                activeTargets[0].Die();
                TargetDestroyed(-1);
            }
            curTarget.SetIndexInManager(activeTargets.Count);
            activeTargets.Add(curTarget);

            yield return new WaitForSeconds(targetFrequency);
        }

        ITarget SpawnTarget()
        {
            GameObject target = Instantiate(targetPrefab);
            Vector3 newPosition = this.transform.position;


            // Spawn position in a dome shape
            newPosition.x += UnityEngine.Random.Range(-5.0f, 5.0f);
            newPosition.z += UnityEngine.Random.Range(-5.0f, 5.0f);

            newPosition.y = UnityEngine.Mathf.Sqrt(50 - UnityEngine.Mathf.Pow(newPosition.x, 2) - UnityEngine.Mathf.Pow(newPosition.z, 2));

            target.transform.position = newPosition;
            target.transform.LookAt(player.transform.position);
            target.transform.Rotate(85, 0, 0);
            return target.GetComponent<ITarget>();
        }
    }

    void EndGame()
    {
        misses = ClearAllTargets();
        OnGameEnd?.Invoke();
    }

    int ClearAllTargets()
    {
        int erases = 0;
        foreach (ITarget target in activeTargets)
        {
            target.Die();
            erases++;
        }
        activeTargets.Clear();
        return erases;
    }

    public void TargetDestroyed(int index)
    {
        if (index < -1 || index >= activeTargets.Count) return;

        // index of -1 indicates this is a miss

        if (index == -1) index = 0;
        else score++;

        activeTargets.RemoveAt(index); // RemoveAt shifts further elements back one, like a queue

        // We only need to update stored indices from the index where the removal happened
        for (int i = index; i < activeTargets.Count; i++)
        {
            activeTargets[i].SetIndexInManager(i);
        }
    }

    public int GetScore() { return score; }
    public int GetMisses() { return misses; }
}
