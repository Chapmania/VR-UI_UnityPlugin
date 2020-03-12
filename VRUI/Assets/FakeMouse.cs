using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FakeMouse : MonoBehaviour
{
    private Canvas canvas;
    public EventSystem es;
    private RectTransform rect;
    public UnityEngine.UI.Selectable[] entered, pointerUp; //over
    private bool clicking;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
    }

    public void Click()
    {
        clicking = true;
        var toPointerUpNextUpdate = new List<UnityEngine.UI.Selectable>();
        foreach (var obj in entered)
        {
            obj.OnPointerDown(new PointerEventData(EventSystem.current));
            toPointerUpNextUpdate.Add(obj);
        }

        //foreach (var obj in over)
        //{
        //    obj.OnPointerDown(new PointerEventData(EventSystem.current));
        //    toPointerUpNextUpdate.Add(obj);
        //}

        pointerUp = toPointerUpNextUpdate.ToArray();
    }

    public void EndClick() => clicking = false;

    // Update is called once per frame
    void Update()
    {
        //Removed mouse over properties for streamlining

        var enteredThisUpdate = new List<UnityEngine.UI.Selectable>();
        //var mouseOver = new List<UnityEngine.UI.Selectable>();

        var overlapping = new List<UnityEngine.UI.Selectable>();
        var noOverlap = new List<UnityEngine.UI.Selectable>();

        foreach (var obj in canvas.GetComponentsInChildren<UnityEngine.UI.Selectable>())
        {        
            if (Overlapping(rect, obj.GetComponent<RectTransform>()))
            {
                overlapping.Add(obj);
                obj.OnPointerEnter(new PointerEventData(EventSystem.current));
                enteredThisUpdate.Add(obj);
            }
            else
            {
                noOverlap.Add(obj);
            }
        }

        //foreach (var obj in overlapping)
        //{
        //    if(entered.Contains(obj) || mouseOver.Contains(obj))
        //    {
        //        mouseOver.Add(obj);
        //        //Mouse Over Event?
        //     }
        //    else
        //    {                
        //        enteredThisUpdate.Add(obj);
        //        obj.OnPointerEnter(new PointerEventData(EventSystem.current));
        //    }
        //}

        foreach (var obj in noOverlap)
        {
            if (entered.Contains(obj)) //|| over.Contains(obj)
            {
                if(clicking && pointerUp.Contains(obj))
                {
                    var ptrUp = pointerUp.ToList();
                    ptrUp.Remove(obj);
                    pointerUp = ptrUp.ToArray();
                }
                obj.OnPointerExit(new PointerEventData(EventSystem.current));
            }            
        }

        if(clicking == false)
        {
            foreach (var obj in pointerUp)
            {
                obj.OnPointerUp(new PointerEventData(EventSystem.current));
            }
        }
        

        entered = enteredThisUpdate.ToArray();
        //over = mouseOver.ToArray();
    }

    private bool Overlapping(RectTransform check, RectTransform overlap)
    {
        if(check.localPosition.y <= overlap.localPosition.y + overlap.rect.yMax && 
           check.localPosition.y >= overlap.localPosition.y + overlap.rect.yMin &&
           check.localPosition.x <= overlap.localPosition.x + overlap.rect.xMax &&
           check.localPosition.x >= overlap.localPosition.x + overlap.rect.xMin)
        {
            return true;
        }
        return false;
    }
}