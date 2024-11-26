using UnityEngine;

public class Cuber : MonoBehaviour
{
    public Transform cubeTransform; 
    public float move =0.2f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            cubeTransform.position = new Vector3(cubeTransform.position.x+move, cubeTransform.position.y, cubeTransform.position.z);
        if (Input.GetKeyDown(KeyCode.A))
            cubeTransform.position = new Vector3(cubeTransform.position.x, cubeTransform.position.y, cubeTransform.position.z+move);
        if (Input.GetKeyDown(KeyCode.S))
            cubeTransform.position = new Vector3(cubeTransform.position.x-move, cubeTransform.position.y, cubeTransform.position.z);
        if (Input.GetKeyDown(KeyCode.D))
            cubeTransform.position = new Vector3(cubeTransform.position.x, cubeTransform.position.y, cubeTransform.position.z-move);
    }
}
