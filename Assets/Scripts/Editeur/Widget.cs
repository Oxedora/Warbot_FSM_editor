using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Widget {

    public Widget parent = null;

    public Rect area = new Rect(new Vector2(0, 0), new Vector2(0, 0));

    public virtual void OnDestroy() { }

    public virtual void OnDraw() { }

    public virtual void OnText() { }

    public virtual void OnUpdate() { }

    public virtual void OnKeyEvent(KeyCode keycode, bool state) { }

    public virtual void OnMouseEvent(int button, bool pressed, int x, int y) { }

    public virtual void OnMotionEvent(int x, int y) { }

    public virtual void OnScrollEvent(int delta) { }

}
