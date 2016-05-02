using UnityEngine;

public class ProjectilesCrate : MonoBehaviour {

    public int amount = 20;

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.player)
        {
            col.gameObject.GetComponent<PlayerAttack>().AddProjectiles(amount);

            Destroy(gameObject);
        }
    }
}
