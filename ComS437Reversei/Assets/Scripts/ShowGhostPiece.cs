using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGhostPiece : MonoBehaviour
{

    public static bool hasSpawnedPiece = false;
    public GameObject ghostPiece;
    public static GameObject spawnedPiece;
    private float placeHeight = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PieceValueController.isPickedUp && !hasSpawnedPiece)
        {
            GameObject spawnedPiece = Instantiate(ghostPiece) as GameObject;
            spawnedPiece.transform.position = new Vector3(0f, 1.2f, 0f);
            hasSpawnedPiece = true;
        }
    }
}
