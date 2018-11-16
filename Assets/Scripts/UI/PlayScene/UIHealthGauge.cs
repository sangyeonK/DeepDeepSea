using UnityEngine;
using UnityEngine.UI;
public class UIHealthGauge : MonoBehaviour
{
    [SerializeField]
    Texture[] headStatusTextureList = null;
    [SerializeField]
    Image healthArc = null;
    [SerializeField]
    RawImage headStatusImage = null;

    const float MAXIMUM_HEALTH_ON_UI = 100f;

    public void UpdateHealth(float health)
    {
        healthArc.fillAmount = CalcHealthArcAmount(health);
        headStatusImage.texture = GetHeadStatusTexture(health);
    }

    private void Start()
    {
        healthArc.fillAmount = CalcHealthArcAmount(Mathf.Infinity);
        headStatusImage.texture = GetHeadStatusTexture(Mathf.Infinity);
    }

    private float CalcHealthArcAmount(float health)
    {
        if(health >= MAXIMUM_HEALTH_ON_UI)
        {
            return 0.5f;
        }

        return 0.5f * (health / MAXIMUM_HEALTH_ON_UI);
    }

    private Texture GetHeadStatusTexture(float health)
    {
        if(health > MAXIMUM_HEALTH_ON_UI / 2)
        {
            return headStatusTextureList[0];
        }
        else if(health > MAXIMUM_HEALTH_ON_UI / 4)
        {
            return headStatusTextureList[1];
        }
        else
        {
            return headStatusTextureList[2];
        }
    }
}
