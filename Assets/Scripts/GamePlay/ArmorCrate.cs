using UnityEngine;

public class ArmorCrate : MonoBehaviour {

    [HideInInspector]
    public int amount = 25;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.player)
        {
            col.gameObject.GetComponent<PlayerHealth>().AddArmor(amount);

            Destroy(gameObject);
        }
    }
}
