/*
 * Filename: DontDestroyOnLoad.cs
 * Author: Jamie Adaway
 * Last Updated: 18/04/23 14:50
 * Desc: Simple script for adding DontDestroyOnLoad calls to GameObjects
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
