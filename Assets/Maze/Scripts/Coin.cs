using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject CoinPoofPrefab;

    [Header("Audio")]
    [SerializeField]
    private AudioClip CoinAudio;

    [Header("Animation")]
    public float MaxSize = 2.0f;
    public float MinSize = 1.0f;
    public float Speed = 1.0f;
    public float PointerScale = 1.5f;

    //Internal
    private bool GazeOver = false;

    public void OnCoinEnter()
    {
        GazeOver = true;
    }

    public void OnCoinExit()
    {
        GazeOver = false;
    }

    public void OnCoinClicked()
    {
        GameObject poof = Instantiate<GameObject>(CoinPoofPrefab);

        poof.transform.parent = transform.parent;
        poof.transform.position = transform.position;
        poof.transform.rotation = Quaternion.Euler(-90,0,0) * transform.rotation;

        AudioSource s = poof.AddComponent<AudioSource>();
        s.clip = CoinAudio;
        s.playOnAwake = false;

        UserController ctrl = Camera.main.GetComponent<UserController>();

        if (ctrl != null) ctrl.OnCoinClick();


        if (s != null) s.Play();

        Destroy(gameObject);        
    }

    void Update()
    {
        float factor = (GazeOver == false) ? 1.0f : PointerScale;

        float size = MinSize * factor + factor * (MaxSize - MinSize) * Mathf.Abs(Mathf.Cos(Speed * Time.time * 1.5f * factor));

        gameObject.transform.localScale = Vector3.one * size;
    }

}
