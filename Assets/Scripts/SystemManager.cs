using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SystemManager : MonoBehaviour
{
    public bool OnBreak = false;
    public float gameplayTimeMinutes;
    [SerializeField] AnimationCurve ExpectedDemonCount;
    [SerializeField] DemonListSpawner demonListSpawner;
    [SerializeField] int TotalDemons = 100;
    [SerializeField] FPSController player;
    private Controls controls;
    public DemonTypeInfo[] DemonTypes;
    public PotionTypeInfo[] LiquidTypes;
    [SerializeField] float CompletetionPercentageForWin = 0.5f;
    [SerializeField] string mainMenuScene;

    [HideInInspector]
    public List<uint> AwaitingSummon;
    public int DemonsSummoned = 0;

    [SerializeField] Canvas FinishUI;

    private float FinishTime;

    private void Start()
    {
        controls = player.controls;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnBreak)
        {
            float DemonsExpected = TotalDemons * ExpectedDemonCount.Evaluate(Time.time / (gameplayTimeMinutes * 60));

            if(Time.time / (gameplayTimeMinutes * 60) >= 1 && !FinishUI.gameObject.activeSelf)
            {
                controls.Player.Disable();
                string winOrLose = ((float)DemonsSummoned / (float)TotalDemons) > CompletetionPercentageForWin ? "you win" : "you lose";
                FinishUI.gameObject.SetActive(true);
                FinishUI.GetComponentInChildren<TextMeshProUGUI>().text = winOrLose;
                FinishTime = Time.time + 5;
            }

            else if (DemonsSummoned + AwaitingSummon.Count < DemonsExpected)
            {
                string newDemonDescription = "";
                uint newDemon = GetDemonKey(out newDemonDescription);
                demonListSpawner.AddToList(newDemon, newDemonDescription);
                AwaitingSummon.Add(newDemon);
            }

            else if(FinishUI.gameObject.activeSelf && FinishTime < Time.time)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }

    public uint GetDemonKey(out string DemonDescription)
    {
        uint demonKey = 0;
        DemonDescription = "";
        int DemonTypeLengthIndex = DemonTypes.Length;
        int LiquidTypeLengthIndex = LiquidTypes.Length; 

        //Get a demontype for this part of the game.
        bool DemonType = false; do 
        {
            int index = UnityEngine.Random.Range(1, DemonTypeLengthIndex);
            if (DemonTypes[index].TimePercentageUnlocked < Time.time / (gameplayTimeMinutes * 60))
            {
                demonKey += DemonTypes[index].KeyIndex * 10;
                DemonDescription += DemonTypes[index].DemonDescription;
                DemonType = true;
            }
            else DemonTypeLengthIndex = index;

        } while (!DemonType);

        //Get a demon adjective for this part of the game
        bool DemonBlood = false; do
        {
            int index = UnityEngine.Random.Range(0, LiquidTypeLengthIndex);
            if (LiquidTypes[index].TimePercentageUnlocked < Time.time / (gameplayTimeMinutes * 60))
            {
                demonKey += LiquidTypes[index].KeyIndex;
                DemonDescription = LiquidTypes[index].PotionDescription + " " + DemonDescription; 
                DemonBlood = true;
            }
            else LiquidTypeLengthIndex = index;

        } while (!DemonBlood);

        return demonKey;
    }

    public float ExpectedCompletionPercentage()
    {
        return DemonsSummoned / (TotalDemons * ExpectedDemonCount.Evaluate(Time.time / (gameplayTimeMinutes * 60)));
    }

    public bool SummonedDemon(uint demonKey)
    {
        foreach(uint demon in AwaitingSummon)
        {
            if(demon == demonKey)
            {
                AwaitingSummon.Remove(demon);
                demonListSpawner.CheckOffDemon(demon);
                DemonsSummoned++;
                return true;
            }
        }

        return false;
    }
}

[Serializable]
public struct DemonTypeInfo
{
    public uint KeyIndex;
    public GameObject Demon;
    public float TimePercentageUnlocked;
    public string DemonDescription;
}

[Serializable]
public struct PotionTypeInfo
{
    public uint KeyIndex;
    public Color color;
    public float TimePercentageUnlocked;
    public string PotionDescription;
}