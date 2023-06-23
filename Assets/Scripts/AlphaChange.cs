using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    private Renderer model;

    void Awake()
    {
        model = GetComponent<Renderer>();
    }

    public void MakeTransparent()
    {
        Color color = model.material.color;
        color.a = 0.5f;
        model.material.color = color;
    }

    public void MakeOpaque()
    {
        Color color = model.material.color;
        color.a = 1;
        model.material.color = color;
    }

    private void OnDestroy()
    {
        Destroy(model.material);
    }
}
