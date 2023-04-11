using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Vector3 target;
	private Enemy enemy;
	private NavMeshAgent navMeshAgent;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		target = new Vector3(70, transform.position.y,-70);
		navMeshAgent.destination = target;
	}

	private void Update()
	{
		if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance == 0)
			EndPath();
		
		enemy.speed = enemy.startSpeed;
	}

	private void EndPath()
	{
		PlayerStats.lives--;
		WaveSpawner.enemiesAlive--;
		Destroy(gameObject);
	}
}
