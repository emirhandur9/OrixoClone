using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class EmptyNode : MonoBehaviour
{

    public bool dontChange;

    [SerializeField]private bool isFull;
    Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        transform.parent.GetComponent<RectTransform>().DOShakeAnchorPos(1f, 10);
    }
    public bool IsFull
    {
        get { return isFull; }
        set
        {
            if(!dontChange)
                isFull = value;
        }
    }

    public void ChangeColor(Color color)
    {
        if (!dontChange)
        {
            img.color = color;
        }
    }

}
