using System;
using System.Collections.Generic;
using UnityEngine;

public class FiniteBehaviourQueue {

	private Queue<FiniteBehaviour> behaviourQueue;
	private FiniteBehaviour currentBehaviour;
	private Action onQueueFinished;

	public FiniteBehaviourQueue(params FiniteBehaviour[] behaviours) {
		behaviourQueue = new Queue<FiniteBehaviour>();
		foreach (FiniteBehaviour behaviour in behaviours) {
			behaviourQueue.Enqueue(behaviour);
		}
	}

	public void Update() {
		if (currentBehaviour == null) {
			Debug.LogWarning("Trying to update finite behaviour queue, but there is no behaviour available.");
			return;
		}

		currentBehaviour.Update();
	}

	public void FixedUpdate() {
		if (currentBehaviour == null) {
			Debug.LogWarning("Trying to update finite behaviour queue, but there is no behaviour available.");
			return;
		}

		currentBehaviour.FixedUpdate();
	}

	public void Start(Action onDone = null) {
		onQueueFinished = onDone;
		StartNextBehaviour();
	}

	private bool StartNextBehaviour() {
		if (behaviourQueue.Count == 0) { return false; }

		currentBehaviour = behaviourQueue.Dequeue();
		currentBehaviour.Start(OnBehaviourFinished);

		return true;
	}

	private void OnBehaviourFinished() {
		bool canStartNext = StartNextBehaviour();
		if (!canStartNext) {
			onQueueFinished?.Invoke();
			currentBehaviour = null;
		}
	}

}