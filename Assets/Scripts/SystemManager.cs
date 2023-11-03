using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SystemManager : MonoBehaviour
{
    public bool OnBreak = false;
    public float gameplayTimeMinutes;
    [SerializeField] AnimationCurve ExpectedDemonCount;
    [SerializeField] DemonListSpawner demonListSpawner;
    [SerializeField] int TotalDemons = 100;
    [SerializeField] TimeUnlocked[] DemonTypes;
    [SerializeField] TimeUnlocked[] LiquidTypes;

    [HideInInspector]
    public List<uint> AwaitingSummon;
    public int DemonsSummoned = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnBreak)
        {
            float DemonsExpected = TotalDemons * ExpectedDemonCount.Evaluate(Time.time / (gameplayTimeMinutes * 60));

            if (DemonsSummoned + AwaitingSummon.Count < DemonsExpected)
            {
                Debug.Log("Printing Ticket");
                uint newDemon = GetDemonKey();
                demonListSpawner.AddToList(newDemon);
                AwaitingSummon.Add(newDemon);
            }
        }
    }

    public uint GetDemonKey()
    {
        uint demonKey = 0;
        int DemonTypeLengthIndex = DemonTypes.Length;
        int LiquidTypeLengthIndex = LiquidTypes.Length; 

        //Get a demontype for this part of the game.
        bool DemonType = false; do 
        {
            int index = UnityEngine.Random.Range(0, DemonTypeLengthIndex);
            if (DemonTypes[index].TimePercentage < Time.time / (gameplayTimeMinutes * 60))
            {
                demonKey += DemonTypes[index].KeyIndex * 10;
                DemonType = true;
            }
            else DemonTypeLengthIndex = index;

        } while (!DemonType);

        //Get a demon adjective for this part of the game
        bool DemonBlood = false; do
        {
            int index = UnityEngine.Random.Range(0, LiquidTypeLengthIndex);
            if (LiquidTypes[index].TimePercentage < Time.time / (gameplayTimeMinutes * 60))
            {
                demonKey += LiquidTypes[index].KeyIndex;
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

    public void SummonedDemon(uint demonKey)
    {
        foreach(uint demon in AwaitingSummon)
        {
            if(demon == demonKey)
            {
                AwaitingSummon.Remove(demon);
                demonListSpawner.CheckOffDemon(demon);
                DemonsSummoned++;
                break;
            }
        }
    }
}

[Serializable]
public struct TimeUnlocked
{
    public uint KeyIndex;
    public float TimePercentage;
}