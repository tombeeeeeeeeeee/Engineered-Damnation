using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SymbolStampController : Focusable
{
    [SerializeField] SymbolRing[] rings;
    [SerializeField] Transform raycastPos;
    [SerializeField] MeshRenderer[] planes;
    [SerializeField] InteractionController playerPickUpScript;
    [SerializeField] GameObject outerUI;
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
    }

    public override void Right()
    {
        rings[currentRing].TurnDial(1);
        planes[currentRing].material = rings[currentRing].symbol;
    }

    public override void Left()
    {
        rings[currentRing].TurnDial(-1);
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
        // stamp
        Exit(context);
    }

    public override void Exit(InputAction.CallbackContext context)
    {
        outerUI.SetActive(false);
        PressStamp();
        base.Exit(context);
    }

    public void PressStamp()
    {
        SlabManager slab = null;

        RaycastHit[] hits = Physics.RaycastAll(raycastPos.position, -transform.up, 1);

        foreach (RaycastHit hit in hits)
        {
            slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
            if (slab != null)
                break;
        }

        if (slab != null)
        {

            slab.ChangeInner(rings[0].symbol, (uint)rings[0].symbolIndex);

            slab.ChangeOuter(rings[1].symbol, (uint)rings[1].symbolIndex);

            //Give a faint imprint of the press onto the slab
            slab.ChangeLiquid(new Color(0, 0, 0, 50), 0);

            playerPickUpScript.PickupObject(slab.gameObject);
        }
    }
}
