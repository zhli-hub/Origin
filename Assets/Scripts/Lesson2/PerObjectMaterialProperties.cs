using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int _baseColorId = Shader.PropertyToID("_BaseColor");
    static MaterialPropertyBlock _block;
    [SerializeField]
    Color baseColor = Color.white;
    
    void OnValidate () {
        if (_block == null) {
            _block = new MaterialPropertyBlock();
        }
        _block.SetColor(_baseColorId, baseColor);
        GetComponent<Renderer>().SetPropertyBlock(_block);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        OnValidate();
    }
}
