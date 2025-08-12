using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Lever[] levers;
    public GameObject door;
    public bool destroyDoor;

    public void CheckLevers(){
        foreach(Lever lever in levers)
        {
            if (!lever.IsUp)
            return;
        }

        if(destroyDoor)
        Destroy(door);
        else
        door.SetActive(false);

        Debug.Log("Puzzle resolvido");
    }

}
