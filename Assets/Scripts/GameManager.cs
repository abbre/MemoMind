using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public static Camera Camera;
    private void OnEnable()
    {
        Camera = _camera;
    }
}
