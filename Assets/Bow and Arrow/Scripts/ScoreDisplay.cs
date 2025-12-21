using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Objects")]
    public GameObject targetManager;
    public GameObject rangedWeapon;

    private ITargetManager managerInterface;
    private IRangedWeapon weaponInterface;
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<TextMeshProUGUI>(out textMesh)) Debug.LogError($"{this} is missing text mesh object");
        if (!targetManager.TryGetComponent<ITargetManager>(out managerInterface)) Debug.LogError($"{this}'s assigned target manager lacks ITargetManager");
        if (!rangedWeapon.TryGetComponent<IRangedWeapon>(out weaponInterface)) Debug.LogError($"{this}'s assigned ranged weapon lacks IRangedWeapon");

        if (managerInterface != null) managerInterface.OnGameEnd += ShowScores;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowScores()
    {
        int score = managerInterface.GetScore();
        int missedTargets = managerInterface.GetMisses();
        int shots = weaponInterface.GetShotCount();

        float accuracy = (float)score / shots;
        float clearRate = (float)score / (score + missedTargets);

        textMesh.text = $"\n\n\n\n\n\n\nShots Fired: {shots}\nTargets Hit: {score}\nTargets Missed: {missedTargets}\n\nAccuracy: {accuracy:P2}\nClear Rate: {clearRate:P2}";

    }
}
