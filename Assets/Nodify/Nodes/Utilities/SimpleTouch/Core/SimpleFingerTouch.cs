using System.Collections;
using UnityEngine;

public enum touchState
{
    NONE = 0,
    DOWN = 1,
    MOVE = 2,
    HOLD = 3,
    UP = 4
}

public enum touchButton
{
    FIRE1,
    FIRE2,
    FIRE3 = 2
}

public delegate void TouchCallback(TTouchEvent touch);

[System.Serializable]
public class TTouchEvent
{
    public touchState state;
    public Vector2 downPosition = new Vector2(0, 0);
    public Vector2 movePosition = new Vector2(0, 0);
    public Vector2 holdPosition = new Vector2(0, 0);
    public Vector2 deltaPosition = new Vector2(0, 0);
    public Vector2 upPosition = new Vector2(0, 0);
    public bool wasDown = false;
    public bool isDown = false;
    public string state_name = "NONE";

    public TouchCallback OnTouchDown;
    public TouchCallback OnTouchHold;
    public TouchCallback OnTouchMove;
    public TouchCallback OnTouchUp;

    public void ResetState()
    {
        this.state = touchState.NONE;
    }

    public void Call_OnDown()
    {
        if (this.OnTouchDown != null) this.OnTouchDown(this);
    }

    public void Call_OnHold()
    {
        if (this.OnTouchHold != null) this.OnTouchHold(this);
    }

    public void Call_OnMove()
    {
        if (this.OnTouchMove != null) this.OnTouchMove(this);
    }

    public void Call_OnUp()
    {
        if (this.OnTouchUp != null) this.OnTouchUp(this);
    }
}

public class SimpleFingerTouch : Nodify.Runtime.Singleton<SimpleFingerTouch>
{
    public int maxFingersCount = 1;
    public TTouchEvent[] touchEvents;
    public bool debug = false;
    public float holdTime = 1.0f;

    private float currentTime = 0.0f;

    // ***************************************************************************************************
    // OnGUI
    // ***************************************************************************************************
    private void OnGUI()
    {
        if (this.debug)
        {
            for (int i = 0; i < this.maxFingersCount; i++)
            {
                int tx = (int)this.touchEvents[i].movePosition.x;
                int ty = (int)this.touchEvents[i].movePosition.y;
                int ix = i * 200;

                if (this.touchEvents[i].wasDown)
                {
                    GUI.Box(new Rect(tx - 16, Screen.height - ty - 16, 32, 32), i.ToString());
                }

                GUI.Label(new Rect(ix, 28, 256, 32), "Touch ID: " + i.ToString());
                GUI.Label(new Rect(ix, 48, 256, 32), " down at: " + this.touchEvents[i].downPosition.ToString());
                GUI.Label(new Rect(ix, 68, 256, 32), " hold at: " + this.touchEvents[i].holdPosition.ToString());
                GUI.Label(new Rect(ix, 88, 256, 32), " move at: " + this.touchEvents[i].movePosition.ToString());
                GUI.Label(new Rect(ix, 108, 256, 32), " up at: " + this.touchEvents[i].upPosition.ToString());
                GUI.Label(new Rect(ix, 128, 256, 32), " state: " + this.touchEvents[i].state_name);
            }
        }
    }

    // ***************************************************************************************************
    // Use this for initialization
    // ***************************************************************************************************

    private void Start()
    {
        this.touchEvents = new TTouchEvent[this.maxFingersCount];

        for (int i = 0; i < this.maxFingersCount; i++)
        {
            this.touchEvents[i] = new TTouchEvent();
        }
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_IPHONE || UNITY_ANDROID
			this.DoTouchFor_Mobile();
#else
        for (int i = 0; i < this.maxFingersCount; i++)
        {
            this.DoTouchFor_x86(i);
        }
#endif
    }

    // ***************************************************************************************************
    // Do Touch for MAC/PC Button[0,1,2]
    // ***************************************************************************************************
    private void DoTouchFor_x86(int id)
    {
        int btn_id = id + 1;

        this.touchEvents[id].isDown = Input.GetButton("Fire" + btn_id.ToString());

        if ((this.touchEvents[id].isDown) && (this.touchEvents[id].wasDown == false) && this.touchEvents[id].state == touchState.NONE)
        {
            this.touchEvents[id].wasDown = true;
            this.touchEvents[id].state = touchState.DOWN;
            this.touchEvents[id].downPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            this.currentTime = Time.time;
            this.touchEvents[id].state_name = "DOWN";

            this.touchEvents[id].Call_OnDown();
        }
        else if ((this.touchEvents[id].isDown) && (this.touchEvents[id].wasDown == true))
        {
            this.touchEvents[id].holdPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (this.touchEvents[id].downPosition == this.touchEvents[id].holdPosition)
            {
                if (this.currentTime + this.holdTime < Time.time)
                {
                    this.touchEvents[id].state = touchState.HOLD;
                    this.touchEvents[id].holdPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    this.touchEvents[id].state_name = "HOLD";
                    this.touchEvents[id].Call_OnHold();
                }
            }
            else
            {
                if (this.touchEvents[id].state != touchState.HOLD)
                {
                    this.touchEvents[id].state = touchState.MOVE;
                    this.touchEvents[id].movePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    this.touchEvents[id].deltaPosition = this.touchEvents[id].downPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    this.touchEvents[id].state_name = "MOVE";
                    this.touchEvents[id].Call_OnMove();
                }
            }
        }
        else if (this.touchEvents[id].wasDown)
        {
            /*
            if (this.touchEvents [id].state == touchState.HOLD)
            {
                this.touchEvents [id].state = touchState.HOLD;
            }
            else
            {
                this.touchEvents [id].state = touchState.UP;
                this.touchEvents [id].upPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
            }
            */
            this.touchEvents[id].state = touchState.UP;
            this.touchEvents[id].upPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            this.touchEvents[id].wasDown = false;
            this.touchEvents[id].state_name = "UP";
            this.touchEvents[id].Call_OnUp();
        }

        this.touchEvents[id].state = touchState.NONE;
    }

    // ***************************************************************************************************
    // Touch for iPad / iPhone / Android devices
    // ***************************************************************************************************

    private void DoTouchFor_Mobile()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId < this.maxFingersCount)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            this.touchEvents[touch.fingerId].state = touchState.DOWN;
                            this.touchEvents[touch.fingerId].isDown = true;
                            this.touchEvents[touch.fingerId].downPosition = touch.position;
                            this.currentTime = Time.time;
                            this.touchEvents[touch.fingerId].Call_OnDown();
                            break;

                        case TouchPhase.Moved:
                            if (this.touchEvents[touch.fingerId].state != touchState.HOLD)
                            {
                                this.touchEvents[touch.fingerId].state = touchState.MOVE;
                                this.touchEvents[touch.fingerId].movePosition = touch.position;
                                this.touchEvents[touch.fingerId].deltaPosition = this.touchEvents[touch.fingerId].downPosition - this.touchEvents[touch.fingerId].holdPosition;
                                this.touchEvents[touch.fingerId].Call_OnMove();
                            }
                            break;

                        case TouchPhase.Stationary:
                            if ((this.currentTime + this.holdTime < Time.time) && (this.touchEvents[touch.fingerId].state != touchState.MOVE))
                            {
                                this.touchEvents[touch.fingerId].state = touchState.HOLD;
                                this.touchEvents[touch.fingerId].holdPosition = touch.position;
                                this.touchEvents[touch.fingerId].deltaPosition = touch.deltaPosition;
                                this.touchEvents[touch.fingerId].Call_OnHold();
                            }
                            break;

                        case TouchPhase.Ended:
                            this.touchEvents[touch.fingerId].state = touchState.UP;
                            this.touchEvents[touch.fingerId].isDown = false;
                            this.touchEvents[touch.fingerId].upPosition = touch.position;
                            this.touchEvents[touch.fingerId].Call_OnUp();
                            break;
                    }
                }
            }
        }
    }

    public bool IsDown(int id)
    {
        if (this.touchEvents[id].state == touchState.DOWN)
        {
            return true;
        }

        return false;
    }

    public bool IsHold(int id)
    {
        if (this.touchEvents[id].state == touchState.HOLD)
        {
            return true;
        }

        return false;
    }

    public bool IsMove(int id)
    {
        if (this.touchEvents[id].state == touchState.MOVE)
        {
            return true;
        }

        return false;
    }

    public bool IsUp(int id)
    {
        if (this.touchEvents[id].state == touchState.UP)
        {
            return true;
        }

        return false;
    }

    public void ResetState(int id)
    {
        this.touchEvents[id].ResetState();
    }
}