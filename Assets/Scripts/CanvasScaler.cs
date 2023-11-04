using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{


        void Awake()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width,Screen.height,0);
}
}
