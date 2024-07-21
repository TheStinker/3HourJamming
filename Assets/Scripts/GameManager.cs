using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 
    private static CameraFollower instance;
    public static CameraFollower Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<CameraFollower>();
            }
            return instance;
        }
    }
    #endregion 

}
