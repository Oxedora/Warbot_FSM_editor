using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLayout : MonoBehaviour {

    private static MainLayout actual;

    public static MainLayout Actual { get { return actual; } }

    private List<uiObject> widgets;

    public GameObject MainCanvas;

    // Use this for initialization
    void Start () {
        MainLayout.actual = this;
        widgets = new List<uiObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddWidget(uiObject obj)
    {
        widgets.Add(obj);
    }

    public void RemoveWidget(uiObject obj)
    {
        obj.OnDestroy();
        widgets.Remove(obj);
    }

    public void OnDraw()
    {
        foreach (uiObject o in widgets)
        {
            o.OnDraw();
        }
    }

    public void OnKeyEvent(int button, bool state, int x, int y)
    {
        foreach (uiObject o in widgets)
        {
            o.OnKeyEvent(button, state, x, y);
        }
    }

    public void OnMouseEvent(int keycode, bool pressed)
    {
        foreach (uiObject o in widgets)
        {
            o.OnMouseEvent(keycode, pressed);
        }
    }

    public void OnMotionEvent(int x, int y)
    {
        foreach (uiObject o in widgets)
        {
            o.OnMotionEvent(x, y);
        }
    }

    public void OnScrollEvent(int delta)
    {
        foreach (uiObject o in widgets)
        {
            o.OnScrollEvent(delta);
        }
    }

}
