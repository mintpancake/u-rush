using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIndex : MonoBehaviour
{
    public static int index;
    private static int nums = 4;

    public void ToggleLeft()
    {

        index--;
        if (index < 0)
        {
            index = nums - 1;
        }
    }

    public void ToggleRight()
    {

        index++;
        if (index >= nums)
        {
            index = 0;
        }
    }
}
