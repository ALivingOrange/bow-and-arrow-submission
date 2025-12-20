using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("Objects")]
    public GameObject managerObject;

    private TextMeshProUGUI textMesh;
    private ITargetManager trackedManager;

    void Awake()
    {
        if(!TryGetComponent<TextMeshProUGUI>(out textMesh)) Debug.LogError($"{this} is missing text mesh object");
        if(!managerObject.TryGetComponent<ITargetManager>(out trackedManager)) Debug.LogError($"{this}'s assigned manager lacks ITargetManager");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = $"Score: {trackedManager.GetScore()}";
    }
}
