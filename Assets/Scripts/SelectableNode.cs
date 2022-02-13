using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SelectableNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool pointerEnter;
    public bool playing;

    Vector2 clickPoint;
    Vector2 currentPoint;

    public Color wrongSideColor;
    public Color trueSideColor;
    public Color defaultColor;

    public int Value;
    public bool isCompletedAll;

    Image image;
    [SerializeField] float minClickValue;

    [Space(10)]
    public bool Right;
    public bool Left;
    public bool Top;
    public bool Bot;


    Dir right;
    Dir left;
    Dir top;
    Dir bot;

    [SerializeField] TextMeshProUGUI text;

    List<EmptyNode> colored = new List<EmptyNode>();
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEnter = false;
    }

    private void Start()
    {
        transform.GetComponent<RectTransform>().DOShakeAnchorPos(1f, 10);
        image = GetComponent<Image>();

        right = GetComponent<RightDir>();
        left = GetComponent<LeftDir>();
        bot = GetComponent<BotDir>();
        top = GetComponent<TopDir>();

        text.text = Value.ToString();

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && pointerEnter && !isCompletedAll)
        {
            playing = true;
            clickPoint = Input.mousePosition;

        }

        if(Input.GetMouseButton(0) && playing)
        {
            currentPoint = Input.mousePosition;

            Vector2 dir = currentPoint - clickPoint;

            if(Mathf.Abs(dir.x) > minClickValue || Mathf.Abs(dir.y) > minClickValue)
            {
                if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    if(dir.x > 0)
                    {
                        Select(right);
                    }
                    else
                    {
                        Select(left);
                    }
                }
                else
                {
                    if (dir.y > 0)
                    {
                        Select(top);
                    }
                    else
                    {
                        Select(bot);
                    }
                }
            }
            else   
            {
                UnSelect(right);
                UnSelect(left);
                UnSelect(top);
                UnSelect(bot);
                image.color = defaultColor;
                foreach (var item in colored)
                {
                    
                    item.GetComponent<Image>().color = defaultColor;
                }
            }
        }

        if(Input.GetMouseButtonUp(0) && playing)
        {
            
            playing = false;

            if (isCompletedAll)
            {
                StartCoroutine(CubeAnim());
            }
            else
            {
                foreach (var item in colored)
                {
                    item.GetComponent<EmptyNode>().ChangeColor(defaultColor);
                }
            }
            image.color = Color.white;
        }
    }

    IEnumerator CubeAnim()
    {
        foreach (var item in colored)
        {
            item.GetComponent<RectTransform>().DOScale(0.3f, 0);
            item.GetComponent<RectTransform>().DOScale(1, 1);
            item.GetComponent<Image>().color = Color.white;
            item.GetComponent<EmptyNode>().dontChange = true;
            text.text = "";
        }
        yield return new WaitForSeconds(0.25f);
        
    }
    public void Select(Dir dir)
    {
        UnSelectAllDirExceptThis(dir);

        if(dir == null)
        {
            image.color = wrongSideColor;
            return;
        }
        if (dir.isCompleted) return;
        int selectedCount = 0;
        colored.Clear();
        image.color = trueSideColor;
        Debug.Log("a");
        for (int i = 0; i < dir.Nodes.Length; i++)
        {
            if (selectedCount == Value) break;

            if (dir.Nodes[i].GetComponent<EmptyNode>().IsFull)
            {
                continue;
            }
            else
            {
                dir.Nodes[i].GetComponent<EmptyNode>().ChangeColor(trueSideColor);
                colored.Add(dir.Nodes[i].GetComponent<EmptyNode>());
                selectedCount++;
            }
        }

        if (selectedCount != Value) // OLMAMIÞ.
        {
            foreach (var item in colored)
            {
                image.color = wrongSideColor;
                item.GetComponent<EmptyNode>().ChangeColor(wrongSideColor);
                isCompletedAll = false;
            }
        }
        else // OLMUÞ
        {
            foreach (var item in colored)
            {
                item.IsFull = true;
            }
            isCompletedAll = true;
        }

        dir.isCompleted = true;
        
        
    }

    public void UnSelectAllDirExceptThis(Dir dir)
    {
        if (dir == right)
        {
            UnSelect(bot);
            //UnSelect(right);
            UnSelect(left);
            UnSelect(top);
            return;
        }
        else if (dir == left)
        {
            UnSelect(bot);
            UnSelect(right);
            //UnSelect(left);
            UnSelect(top);

        }
        else if (dir == top)
        {
            UnSelect(bot);
            UnSelect(right);
            UnSelect(left);
            //UnSelect(top);

        }
        else if (dir == bot)
        {
            //UnSelect(bot);
            UnSelect(right);
            UnSelect(left);
            UnSelect(top);
        }
        
        
    }
    public void UnSelect(Dir dir)
    {
        if (dir == null) return;
        for (int i = 0; i < dir.Nodes.Length; i++)
        {
            dir.Nodes[i].GetComponent<EmptyNode>().ChangeColor(defaultColor);
            dir.Nodes[i].GetComponent<EmptyNode>().IsFull = false;
        }
        dir.isCompleted = false;
    }
        


}
