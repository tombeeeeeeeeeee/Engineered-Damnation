using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SystemManager : MonoBehaviour
{
    public bool OnBreak = false;
    [SerializeField] AnimationCurve ExpectedDemonCount;
    [SerializeField] DemonListSpawner demonListSpawner;
    [SerializeField] int TotalDemons = 100;
    [SerializeField] FPSController player;
    public DemonTypeInfo[] DemonTypes;
    public PotionTypeInfo[] LiquidTypes;
    [SerializeField] float CompletetionPercentageForWin = 0.5f;
    [SerializeField] Clock clock;

    [SerializeField] AudioClip[] musics;
    private int musicIndex = 0;
    private AudioSource aS;

    public int DemonsSummoned = 0;
    [HideInInspector]
    public List<uint> AwaitingSummon;

    [SerializeField] AudioMixerGroup[] mixers;

    //Outside effects
    [SerializeField] ParticleSystem outsideFireParticle;
    [SerializeField] float fireParticleHeight = 8;
    [SerializeField] Material outsideFirePlane;
    [SerializeField] float firePlaneHeight = 3;
    [SerializeField] Material skybox;
    [SerializeField] Color startSkyColour;
    [SerializeField] Color endSkyColour;

    private void Start()
    {
        aS = GetComponent<AudioSource>();

        aS.loop = true;
        aS.clip = musics[0];
        aS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnBreak)
        {
            int DemonsExpected = (int)(TotalDemons * ExpectedDemonCount.Evaluate(clock.playthroughPercentage));

            if (DemonsSummoned + AwaitingSummon.Count < DemonsExpected)
            {
                string newDemonDescription;
                uint newDemon = GetDemonKey(out newDemonDescription);
                demonListSpawner.AddToList(newDemon, newDemonDescription);
                AwaitingSummon.Add(newDemon);
            }
        }


        //Music
        if (clock.playthroughPercentage > (float)(musicIndex  + 1) / (musics.Length - 1))
        {
            musicIndex++;
            float musicStartTime = aS.time;
            aS.Stop();
            aS.clip = musics[musicIndex];
            aS.Play();
            aS.time = musicStartTime;
        }

        mixers[0].audioMixer.SetFloat("master", Mathf.Log10(Gameplay.masterVolume) * 20);
        mixers[1].audioMixer.SetFloat("sfx", Mathf.Log10(Gameplay.sfxVolume) * 20);
        mixers[2].audioMixer.SetFloat("music", Mathf.Log10(Gameplay.musicVolume) * 20);
        mixers[3].audioMixer.SetFloat("intercom", Mathf.Log10(Gameplay.dialogueVolume) * 20);


        //Outside world effects
        if (clock.playthroughPercentage < 1)
        {
            float currentSummoningRate = DemonsSummoned * clock.playthroughPercentage / (TotalDemons * ExpectedDemonCount.Evaluate(clock.playthroughPercentage));
            outsideFireParticle.startLifetime = currentSummoningRate * fireParticleHeight;
            outsideFirePlane.SetFloat("FireHeight", currentSummoningRate * firePlaneHeight);
            skybox.color = Color.Lerp(startSkyColour, endSkyColour, currentSummoningRate);
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
            if (DemonTypes[index].TimePercentageUnlocked <= clock.playthroughPercentage)
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
            if (LiquidTypes[index].TimePercentageUnlocked <= clock.playthroughPercentage)
            {
                demonKey += LiquidTypes[index].KeyIndex;
                DemonDescription = LiquidTypes[index].PotionDescription + "\n" + DemonDescription; 
                DemonBlood = true;
            }
            else LiquidTypeLengthIndex = index;

        } while (!DemonBlood);

        return demonKey;
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

    public bool winState
    {
        get { return CompletetionPercentageForWin * TotalDemons < DemonsSummoned; }
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
    public Color shaderColor;
    public float TimePercentageUnlocked;
    public string PotionDescription;
}