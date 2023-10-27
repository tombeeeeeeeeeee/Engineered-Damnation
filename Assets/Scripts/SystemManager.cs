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
            float DemonsExpected = TotalDemons * ExpectedDemonCount.Evaluate(gameplayTimeMinutes * 60 / Time.time);
            if (DemonsSummoned + AwaitingSummon.Count < DemonsExpected)
            {
                uint newDemon = GetDemonKey();
                //demonListSpawner.AddToList(newDemon);
                //AwaitingSummon.add(newDemon);
            }
        }
    }

    public uint GetDemonKey()
    {
        uint demonKey = 0;
        int DemonTypeLengthIndex = DemonTypes.Length;
        int LiquidTypeLengthIndex = LiquidTypes.Length; 

        bool DemonType = false; do 
        {
            int index = Random.Range(0, DemonTypeLengthIndex);
            if (DemonTypes[index].TimePercentage < gameplayTimeMinutes * 60 / Time.time)
            {
                demonKey += DemonTypes[index].KeyIndex * 10;
                DemonType = true;
            }
            else DemonTypeLengthIndex = index;

        } while (!DemonType);

        bool DemonBlood = false; do
        {
            int index = Random.Range(0, LiquidTypeLengthIndex);
            if (LiquidTypes[index].TimePercentage < gameplayTimeMinutes * 60 / Time.time)
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
        return DemonsSummoned / (TotalDemons * ExpectedDemonCount.Evaluate((gameplayTimeMinutes * 60 / Time.time)));
    }
}

public struct TimeUnlocked
{
    public uint KeyIndex;
    public float TimePercentage;
}