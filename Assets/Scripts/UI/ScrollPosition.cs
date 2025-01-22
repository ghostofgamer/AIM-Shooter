using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPosition : MonoBehaviour
{
    public ScrollRect scrollRect;

    private void OnEnable()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }

}
