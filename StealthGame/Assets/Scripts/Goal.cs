using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //  Player‚ªÚG‚µ‚½‚çAGameClear‚ÉˆÚs
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GameClear();
        }
    }
}
