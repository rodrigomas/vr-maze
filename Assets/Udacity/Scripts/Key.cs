using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject KeyPoofPrefab;

    [SerializeField]
    private Door door;

    [Header("Audio")]
    [SerializeField]
    private AudioClip KeyAudio;

    [Header("Animation")]
    public float Delta = 0.5f;
    public Vector2 Speed = Vector2.one;
    public float PointerScale = 1.5f;

    //Internal
    private bool GazeOver = false;
    private Vector3 _p0;

    void Awake()
    {
        _p0 = gameObject.transform.position;
    }

    public void OnKeyEnter()
    {
        GazeOver = true;
    }

    public void OnKeyExit()
    {
        GazeOver = false;
    }

    void Update()
	{
        float factor = (!GazeOver) ? 1.0f : PointerScale;

        gameObject.transform.rotation = Quaternion.Euler(0, 360.0f * Speed.y * Time.time * factor, 0);
        gameObject.transform.position = _p0 + Vector3.up * Delta * Mathf.Abs(Mathf.Cos(Speed.x * Time.time * factor));
    }

	public void OnKeyClicked()
	{
        GameObject poof = Instantiate<GameObject>(KeyPoofPrefab);

        poof.transform.parent = transform.parent;
        poof.transform.position = transform.position;
        poof.transform.rotation = Quaternion.Euler(0,0,0);

        AudioSource s = poof.AddComponent<AudioSource>();
        s.clip = KeyAudio;
        s.playOnAwake = false;

        if (s != null) s.Play();

        if (door != null)
        {
            door.Unlock();
        }

        Destroy(gameObject);
    }

}
