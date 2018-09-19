using UnityEngine;
using UnityEngine.UI;

public class UILastSecondAlert : MonoBehaviour
{
    public Texture[] numbers = new Texture[NUMBERS_SIZE];

    private const int NUMBERS_SIZE = 10;
    private RawImage image;

    private void Start()
    {
        image = GetComponent<RawImage>();

        InjectEventListeners();
    }

    private void OnGamePlaySecondTick(int remainSecond)
    {
        if (remainSecond < NUMBERS_SIZE)
        {
            image.texture = numbers[remainSecond];
            image.SetNativeSize();
        }
    }

    private void InjectEventListeners()
    {
        GameManager.Instance.AddGamePlaySecondTickListener(OnGamePlaySecondTick);
    }
    private void OnDestroy()
    {
        //removeEventListeners
        GameManager.Instance.RemoveGamePlaySecondTickListener(OnGamePlaySecondTick);

    }
}
