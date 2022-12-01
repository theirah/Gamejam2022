using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthDisplay : MonoBehaviour
{
    [SerializeField]
    HealthComponent trackedHealth;

    [SerializeField]
    RectMask2D mask;

    float originalMaskWidth;

    // Start is called before the first frame update
    void Start()
    {
        originalMaskWidth = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedHealth)
        {
            float percentageHealth = (float)trackedHealth.CurrHealth / (float)trackedHealth.MaxHealth;
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  percentageHealth* originalMaskWidth);
        }
    }
}
