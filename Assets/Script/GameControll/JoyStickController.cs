using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour {

    [SerializeField]
    private UIEventListener stick = null;
    [SerializeField]
    private Transform stickTrans = null;
    [SerializeField]
    private bool IsDragingStick = false;

	// Use this for initialization
	void Start ()
    {
		if(null != stick)
        {
            stick.onClick = HandleOnClickStick;
            stick.onDrag = HandleOnDrag;
            stick.onDragEnd = HandleOnDragEnd;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (false == IsDragingStick)
            return;

		if(null != stickTrans)
        {
            stickTrans.localPosition = UIManager.Instance.UICamera.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(stickTrans.localPosition);
        }
	}

    private void HandleOnClickStick(GameObject obj_)
    {
        stickTrans.localPosition = Input.mousePosition;
    }

    private void HandleOnDrag(GameObject obj_, Vector2 vec_)
    {
        IsDragingStick = true;
    }

    private void HandleOnDragEnd(GameObject obj_)
    {
        IsDragingStick = false;


        if (null != stickTrans)
        {
            stickTrans.localPosition = Vector3.zero;
        }
    }
}
