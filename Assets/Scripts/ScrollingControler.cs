using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingControler : MonoBehaviour
{
    public float scrollingSpeed;
    public GameObject pc;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        pc.GetComponent<Foo>().Move(scrollingSpeed);
    }
}