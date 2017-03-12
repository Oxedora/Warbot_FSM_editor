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

        this.AddWidget(new SelectorPanel());
        this.AddWidget(new Box(new Rect(700, 300, 100, 100), "Primitive 2", true, null));
        this.AddWidget(new Box(new Rect(500, 200, 100, 100), "Primitive 1", false, (Box)widgets[widgets.Count - 1]));
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

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    public void OnDraw()
    {
        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        GL.LoadOrtho();
        foreach (Widget o in widgets)
        {
            o.OnDraw();
        }
        GL.PopMatrix();
        foreach (Widget o in widgets)
        {
            o.OnText();
        }
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
            case EventType.Repaint:
                OnDraw();
                break;
        }
    }
}
