using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWorkSheet : MonoBehaviour
{
    public float floatNumber;
    private int intNumber;
    Image image;
    Rigidbody rigidbody;
    Camera camera;
    Animator animator;


    Queue<Transform> queue = new Queue<Transform>();

    void Start()
    {
        int i = ConvertToInt(7.5f);
        int u = MultipleInt(10, 31);

        int[] intArray = new int[8];
        List<int> intList = new List<int>();

    }

    void Update()
    {
        
    }

    int ConvertToInt(float number)
    {
        return Mathf.FloorToInt(number);
    }

    int MultipleInt(int a, int b)
    {
        return a * b;
    }
}
