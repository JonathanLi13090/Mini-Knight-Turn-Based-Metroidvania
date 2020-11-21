using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coding : MonoBehaviour
{
    int[] A = { 10, 6, 3, 5, 1, 9, 8 };
    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        for(int j = 0; j < A.Length; j += 1)
        {
            for (int i = 0; i < A.Length - 1 - j; i += 1)
            {
                count += 1;
                if (A[i] > A[i + 1])
                {
                    int temp = A[i];
                    A[i] = A[i + 1];
                    A[i + 1] = temp;
                }
                string display = "";
                foreach (int value in A)
                {
                    display += value + ", ";
                }
                Debug.Log(display);
            }
        }
        Debug.Log(count + " times");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected List<int> MyFunction()
    {
        return new List<int>();
    }
}
