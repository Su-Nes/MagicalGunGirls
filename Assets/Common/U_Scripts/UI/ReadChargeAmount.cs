using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReadChargeAmount : MonoBehaviour
{
    public CooldownManager _CooldownManager;
    private Image cooldownImage;

    private void Start()
    {
        cooldownImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (_CooldownManager != null)
            cooldownImage.fillAmount = _CooldownManager.FillAmount();
    }
}
