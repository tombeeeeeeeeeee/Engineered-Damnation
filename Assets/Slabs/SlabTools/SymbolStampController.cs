using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class SymbolStampController : Focusable
{
    [SerializeField] SymbolRing[] rings;
    [SerializeField] MeshRenderer[] planes;
    [SerializeField] InteractionController playerPickUpScript;
    [SerializeField] GameObject outerUI;
    private AudioSource aS;
    [SerializeField] AudioClip[] slabBurnInSounds;
    [SerializeField] AudioClip burningSound;
    [SerializeField] AudioClip calmBurning;
    [SerializeField] SnapSlab slabSnap;
    [SerializeField] float lengthOfBurnIn;
    private float burnInTime = 0;

    int currentRing = 0; // 0=outer 1=inner

    //   cycle input : turn left and right
    //    exit input : exit
    // action1 input : switch ring
    // action2 input : stamp

    private void Start()
    {
        planes[0].material = rings[0].symbol;
        planes[1].material = rings[1].symbol;
        outerUI.SetActive(false);
        ui.SetActive(false);

        aS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!player.controls.Focused.Action2.IsPressed() && aS.isPlaying && aS.clip == burningSound)
        {
            aS.Stop();
            aS.clip = null;
        }

        burnInTime += player.controls.Focused.Action2.IsPressed() ? Gameplay.deltaTime : -Gameplay.deltaTime;
        burnInTime = burnInTime < 0 ? 0 : burnInTime;

        planes[0].material.SetFloat("_Burnin", burnInTime / lengthOfBurnIn);
        planes[1].material.SetFloat("_Burnin", burnInTime / lengthOfBurnIn);
        
        if(slabSnap.ExpectedObject != null && player.controls.Focused.Action2.IsPressed())
            slabSnap.ExpectedObject.GetComponent<SlabManager>().AddToFlames(Gameplay.deltaTime * 2);


        if (burnInTime > lengthOfBurnIn)
        {
            PressStamp();
            Exit(new InputAction.CallbackContext());
            burnInTime = 0;
        }
    }

    public override void Right()
    {
        rings[currentRing].TurnDial(-1);
        planes[currentRing].material = rings[currentRing].symbol;
    }

    public override void Left()
    {
        rings[currentRing].TurnDial(1);
        planes[currentRing].material = rings[currentRing].symbol;
    }

    public override void UpDown(float userInput)
    {
        currentRing = (int)(userInput + 1) / (int) 2;

        if (currentRing == 0)
        {
            outerUI.SetActive(false);
            ui.SetActive(true);
        }
        else
        {
            outerUI.SetActive(true);
            ui.SetActive(false);
        }
    }

    public override void Action2(InputAction.CallbackContext context)
    {
        if (slabSnap.ExpectedObject != null)
        {
            aS.Stop();
            aS.clip = burningSound;
            aS.loop = true;
            aS.Play();
        }
        else Exit(new InputAction.CallbackContext());
    }


    public override void Exit(InputAction.CallbackContext context)
    {
        outerUI.SetActive(false);
        base.Exit(context);
    }

    public void PressStamp()
    {
        SlabManager slab = slabSnap.ExpectedObject.GetComponent<SlabManager>();
        if (slab != null)
        {
            aS.Stop();
            aS.clip = null;
            aS.PlayOneShot(slabBurnInSounds[Random.Range(0,slabBurnInSounds.Length)]);

            slab.ChangeInner(rings[0].symbol, (uint)rings[0].symbolIndex);

            slab.ChangeOuter(rings[1].symbol, (uint)rings[1].symbolIndex);

            //Give a faint imprint of the press onto the objectToDestroy
            slab.ChangeLiquid(new Color(0, 0, 0, 50), 0);

            playerPickUpScript.PickupObject(slab.gameObject);
        }
    }
}
