using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField]
    private AudioClip LockSound;

    [SerializeField]
    private AudioClip UnlockSound;

    [Header("Animation")]
    public float DX = -10f;
    public float Interval = 2.1f;
    public bool ClickToOpen = true;

    [Header("Material")]
    [SerializeField]
    private Material Normal;

    [SerializeField]
    private Material OverRed;

    [SerializeField]
    private Material OverGreen;
    //Flags
    private bool locked = true;
    private bool anim_click = false;
    private bool anim_done = false;

    //Internal
    private Vector3 _p0;
    private float dt = 0;
    private bool GazeOver = false;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _p0 = gameObject.transform.position;
    }

    public void OnDoorEnter()
    {
        GazeOver = true;
    }

    public void OnDoorExit()
    {
        GazeOver = false;
    }

    void Update()
    {
        if (!ClickToOpen)
        {
            if (locked == false && anim_done == false)
            {
                gameObject.transform.position = _p0 + DX * Interval * dt * Vector3.right;

                dt += Time.deltaTime;

                if (dt >= 1.0f)
                {
                    anim_done = true;
                }
            }
        }
        else
        {
            // The user must click to open the door
            if (locked == false && anim_done == false && anim_click == true)
            {
                gameObject.transform.position = _p0 + DX * Interval * dt * Vector3.right;

                dt += Time.deltaTime;

                if (dt >= 1.0f)
                {
                    anim_done = true;
                }
            }
        }

        if(GazeOver == true)
        {
            if(locked)
            {
                gameObject.GetComponent<MeshRenderer>().material = OverRed;
            } else
            {
                gameObject.GetComponent<MeshRenderer>().material = OverGreen;
            }

        } else
        {
            gameObject.GetComponent<MeshRenderer>().material = Normal;
        }
    }

    public void OnDoorClick()
    {
        if(locked)
        {
            _audioSource.clip = LockSound;
            _audioSource.Play();
        } else if(anim_done == false)
        {
            anim_click = true;

            if (!ClickToOpen) anim_done = true;

            _audioSource.clip = UnlockSound;
            _audioSource.Play();
        }
    }

    public void Unlock()
    {
        locked = false;
    }
}
