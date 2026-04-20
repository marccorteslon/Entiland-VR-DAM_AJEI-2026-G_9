using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Gun : MonoBehaviour
{
    public LayerMask layer_player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Physics.SphereCast(transform.position, 0.25f, transform.forward, out RaycastHit hit, layer_player))
        {
            SceneManager.LoadScene(0);
        }
    }
}
