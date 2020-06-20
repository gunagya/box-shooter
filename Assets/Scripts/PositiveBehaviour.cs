using UnityEngine;
using System.Collections;

public class PositiveBehaviour : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;
    public GameObject spawnObject;

    // explosion when hit?
    public GameObject explosionPrefab;

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManager.gm) {
				GameManager.gm.targetHit (scoreAmount, timeAmount);
			}
            if (spawnObject)
                MakeThingToSpawn();

            // destroy the projectile
            Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);
		}
	}

    void MakeThingToSpawn()
    {
        Vector3 spawnPosition;

        // get a random position between the specified ranges
        spawnPosition.x = Random.Range(-25, 25);
        spawnPosition.y = Random.Range(8, 25);
        spawnPosition.z = Random.Range(-25, 25);

        // actually spawn the game object
        GameObject spawnedObject = Instantiate(spawnObject, spawnPosition, transform.rotation) as GameObject;

    }
}

