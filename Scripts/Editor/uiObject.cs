using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class uiObject
{

    public abstract void OnDestroy();

    public abstract void OnDraw();

    public abstract void OnKeyEvent(int button, bool state, int x, int y);

    public abstract void OnMouseEvent(int keycode, bool pressed);

    public abstract void OnMotionEvent(int x, int y);

    public abstract void OnScrollEvent(int delta);

}
