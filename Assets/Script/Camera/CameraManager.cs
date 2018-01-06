using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private bool IsFollowTarget = false;
    private Transform targetTrans = null;
    private Transform movingObjTrans = null;
    private Vector3 offsetPos = Vector3.zero;

    private bool cameraMovingByUpdate = false;
    private float upDateMovingTime = 0;

    private Action MovingByUpdateEndCallback = null;

    public void CameraMovingByCoroutine(Vector3 startPos_, Vector3 DestPos_, Transform movingCameraObjTrans_, float speed_, Action MovingByUpdateEndCallback_ = null)
    {
        MovingByUpdateEndCallback = MovingByUpdateEndCallback_;
        StartCoroutine(CarmeraMove(startPos_, DestPos_, movingCameraObjTrans_, speed_));
    }
    public void CameraMovingByCoroutine(Vector3 startPos_, Transform TargetTrans_, Transform movingCameraObjTrans_, float speed_, Action MovingByUpdateEndCallback_ = null)
    {
        targetTrans = TargetTrans_;
        MovingByUpdateEndCallback = MovingByUpdateEndCallback_;
        StartCoroutine(CarmeraMove(startPos_, TargetTrans_.position, movingCameraObjTrans_, speed_));
    }

    public void CameraRotatingByCoroutine(Quaternion startRotate_, Quaternion DestRotate_, Transform movingCameraObjTrans_, float speed_)
    {
        StartCoroutine(CameraRotate(startRotate_, DestRotate_, movingCameraObjTrans_, speed_));
    }

    public void CameraMovingByUpdate(Transform movingCameraObjTrans_, Transform TargetTrans_, float speed_, Action MovingByUpdateEndCallback_ = null)
    {
        MovingByUpdateEndCallback = MovingByUpdateEndCallback_;
        cameraMovingByUpdate = true;
        IsFollowTarget = false;
        targetTrans = TargetTrans_;
        movingObjTrans = movingCameraObjTrans_;
    }


    IEnumerator CarmeraMove(Vector3 startPos_, Vector3 DestPos_, Transform movingCameraObjTrans_, float speed_)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * speed_;
            movingCameraObjTrans_.position = Vector3.Lerp(startPos_, DestPos_, t);
            yield return 0;
        }

        offsetPos = DestPos_;
        IsFollowTarget = true;
        if (null != MovingByUpdateEndCallback)
            MovingByUpdateEndCallback();
    }

    IEnumerator CameraRotate(Quaternion startRotate_, Quaternion DestRotate_, Transform movingCameraObjTrans_, float speed_)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * speed_;
            movingCameraObjTrans_.rotation = Quaternion.Slerp(startRotate_, DestRotate_, t);
            yield return 0;
        }
    }

    private void Update()
    {
        if(true == IsFollowTarget)
        {
            if(null != targetTrans && null != movingObjTrans)
            {
                movingObjTrans.position = targetTrans.position;
            }
        }

        if(true == cameraMovingByUpdate)
        {
            upDateMovingTime += Time.unscaledDeltaTime * 0.06f;
            if (null != targetTrans && null != movingObjTrans)
            {
                movingObjTrans.position = Vector3.Lerp(movingObjTrans.position, targetTrans.position, upDateMovingTime);
            }

            if(upDateMovingTime >= 0.1f)
            {
                cameraMovingByUpdate = false;
                IsFollowTarget = true;

                if (null != MovingByUpdateEndCallback)
                    MovingByUpdateEndCallback();
            }
        }
    }

    public void FollowTarget(Transform targetTrans_, Transform movingObjTrans_)
    {
        if(null != targetTrans_)
            IsFollowTarget = true;
        else
            IsFollowTarget = false;

        targetTrans = targetTrans_;
        movingObjTrans = movingObjTrans_;
    }
}
