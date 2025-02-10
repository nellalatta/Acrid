using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
        public float stopDistance = 1f; // Distance at which the enemy stops moving towards the player
        public float dodgeDistance = 2f; // How far enemies dodge left or right
        public float dodgeInterval = 2f; // Time between dodges
        public float chaseDelay = 1.5f; // Time to wait before chasing again when player moves out of range

        private float lastDodgeTime;
        private float lastLostPlayerTime;
        private bool waitingToChase;
		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;

			target = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update() {
			if (target == null || ai == null) return;

			float distanceToPlayer = Vector3.Distance(transform.position, target.position);

			if (distanceToPlayer > stopDistance) {
				if (waitingToChase) return;

				ai.destination = target.position;
				lastLostPlayerTime = Time.time; // Reset chase delay timer
			} 
			else {
				// Player in range, dodge
				if (Time.time - lastDodgeTime > dodgeInterval) {
					lastDodgeTime = Time.time;
					Vector3 dodgeDirection = (Random.value < 0.5f ? -1f : 1f) * transform.right * dodgeDistance;
					ai.destination = transform.position + dodgeDirection;
				}
			}

			// Start delay when player leaves range
			if (distanceToPlayer > stopDistance && !waitingToChase) {
				StartCoroutine(WaitBeforeChasing());
			}
		}

		private IEnumerator WaitBeforeChasing() {
			waitingToChase = true;
			yield return new WaitForSeconds(chaseDelay);
			waitingToChase = false;
		}
	}
}
