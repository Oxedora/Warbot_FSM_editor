using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Widget {

    public virtual void OnDestroy() { }

    public virtual void OnUpdate() { }

    public virtual void OnKeyEvent(KeyCode keycode, bool state) { }

    public virtual void OnMouseEvent(int button, bool pressed, int x, int y) { }

    public virtual void OnMotionEvent(int x, int y) { }

    public virtual void OnScrollEvent(int delta) { }

}
