using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public float transitionDuration = 0.5f;

    public void CameraMoving(Vector3 startPos_, Vector3 DestPos_, Transform movingObjTrans_, float speed_)
    {
        StartCoroutine(CarmeraMove(startPos_, DestPos_, movingObjTrans_, speed_));
    }

    IEnumerator CarmeraMove(Vector3 startPos_, Vector3 DestPos_, Transform movingObjTrans_, float speed_)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration) * speed_;
            movingObjTrans_.position = Vector3.Lerp(startPos_, DestPos_, t);
            yield return 0;
        }
    }
}
