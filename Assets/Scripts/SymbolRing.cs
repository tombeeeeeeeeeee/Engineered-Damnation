using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SymbolRing : MonoBehaviour
{
    public Material[] symbols;

    [HideInInspector]
    public float anglePerSymbol = 360f;
    public int symbolIndex = 1;

    private AudioSource aS;
    [SerializeField] AudioClip[] dialTurnSound;

    public float duration = 0.2f;

    Quaternion targetRotation;
    Quaternion fromRotation;

    float elapsed;
    bool moving = false;

    private void Start()
    {
        anglePerSymbol = 360 / symbols.Length;
        aS = transform.parent.GetComponent<AudioSource>();
    }

    public Material symbol { get { return symbols[symbolIndex - 1]; } }

    public void TurnDial(int direction)
    {
        if (!moving)
        {
            aS.Stop();
            aS.PlayOneShot(dialTurnSound[Random.Range(0, dialTurnSound.Length)]);
            direction = (int)Mathf.Sign(direction); // argument should be either -1 or 1, but just to make sure

            Quaternion initialRotation = transform.rotation;
            transform.Rotate(Vector3.up, direction * anglePerSymbol);
            targetRotation = transform.rotation;
            transform.rotation = initialRotation;

            fromRotation = initialRotation;
            moving = true;
            elapsed = 0;

            symbolIndex -= direction;


            if (symbolIndex > symbols.Length)
                symbolIndex = 1;
            else if (symbolIndex < 1)
                symbolIndex = symbols.Length;

        }
    }

    private void Update()
    {
        if (moving)
        {
            float interpolationRatio = elapsed / duration;

            Quaternion interpolatedRotation = Quaternion.Lerp(fromRotation, targetRotation, interpolationRatio);

            transform.rotation = interpolatedRotation;


            if (elapsed < duration)
                elapsed += Gameplay.deltaTime;
            else moving = false;
        }
    }
}
