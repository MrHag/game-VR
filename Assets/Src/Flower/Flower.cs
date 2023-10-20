using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Animator animator;

    public bool open;

    private bool _open;
    public bool Open
    {
        get
        {
            return _open;
        }
        set
        {
            if (value == _open)
                return;

            _open = value;
            animator.SetBool("Open", _open);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        Open = open;
    }
}
