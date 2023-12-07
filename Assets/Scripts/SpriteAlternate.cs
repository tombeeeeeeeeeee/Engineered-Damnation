using UnityEngine;
using UnityEngine.UI;

public class SpriteAlternate : MonoBehaviour
{
    [SerializeField] float timeBetweenAlternating;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    private float timeSinceAlternate = 0;

    // Update is called once per frame
    void Update()
    {
        timeSinceAlternate += Gameplay.deltaTime;

        if(timeSinceAlternate > timeBetweenAlternating)
        {
            gameObject.GetComponent<Image>().sprite =
                gameObject.GetComponent<Image>().sprite != onSprite ? onSprite : offSprite;
            timeSinceAlternate = 0;
        }
    }
}
