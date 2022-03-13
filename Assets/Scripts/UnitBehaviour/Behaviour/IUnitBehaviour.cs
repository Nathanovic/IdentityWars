using UnityEngine;

// The purpose of this interface is to make the Initialize method easily accessible for the Unit class
public interface IUnitBehaviour {
	void Initialize(Animator animator, Transform transform);
}