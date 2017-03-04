using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Text Points;

    [SerializeField]
    private Text TimeVal;

    // Internals
    private int _points = 0;
    private float _sec = 0;

    void Awake()
    {
        _sec = 0;
    }

    void FixedUpdate()
    {
        _sec += Time.fixedDeltaTime;

        TimeVal.text = string.Format("{0:0.0}", _sec);
    }

    public void OnCoinClick()
    {
        _points++;

        Points.text = string.Format("{0:0}", _points);
    }
}
