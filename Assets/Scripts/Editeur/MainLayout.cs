using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainLayout : MonoBehaviour
{

    private static MainLayout actual;

    public static MainLayout Actual { get { return actual; } }

    private List<Widget> widgets;

    public GameObject MainCanvas;

    private Vector2 mousePos;

    // Use this for initialization
    void Start()
    {
        MainLayout.actual = this;
        widgets = new List<Widget>();
        mousePos = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public void AddWidget(Widget obj)
    {
        widgets.Add(obj);
    }

    public void RemoveWidget(Widget obj)
    {
        obj.OnDestroy();
        widgets.Remove(obj);
    }

    public void OnUpdate()
    {
        foreach (Widget o in widgets)
        {
            o.OnUpdate();
        }
    }

    public void OnKeyEvent(KeyCode keycode, bool state)
    {
        print("[Key]: " + keycode + " = " + state);
        foreach (Widget o in widgets)
        {
            o.OnKeyEvent(keycode, state);
        }
    }

    public void OnMouseEvent(int button, bool pressed, int x, int y)
    {
        print("[Mouse]: " + button + " = " + pressed + " -> " + x + " , " + y);
        foreach (Widget o in widgets)
        {
            o.OnMouseEvent(button, pressed, x, y);
        }
    }

    public void OnMotionEvent(int x, int y)
    {
        print("[Motion]: " + x + " , " + y);
        foreach (Widget o in widgets)
        {
            o.OnMotionEvent(x, y);
        }
    }

    public void OnScrollEvent(int delta)
    {
        print("[Scroll]: " + delta);
        foreach (Widget o in widgets)
        {
            o.OnScrollEvent(delta);
        }
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (mousePos != e.mousePosition)
        {
            mousePos = e.mousePosition;
            OnMotionEvent((int)e.mousePosition.x, (int)e.mousePosition.y);
        }
        switch (e.type)
        {
            case EventType.MouseDown:
                OnMouseEvent(e.button, true, (int)e.mousePosition.x, (int)e.mousePosition.y);
                break;
            case EventType.MouseUp:
                OnMouseEvent(e.button, false, (int)e.mousePosition.x, (int)e.mousePosition.y);
                break;
            case EventType.MouseMove:
                OnMotionEvent((int)e.mousePosition.x, (int)e.mousePosition.y);
                break;
            case EventType.ScrollWheel:
                OnScrollEvent((int)e.delta.y);
                break;
            case EventType.KeyDown:
                OnKeyEvent(e.keyCode, true);
                break;
            case EventType.KeyUp:
                OnKeyEvent(e.keyCode, false);
                break;
        }
    }
}
