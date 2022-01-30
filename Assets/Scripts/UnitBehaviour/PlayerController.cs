using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStateMachineTarget {

	public InputMaster.PlayerActions Input { get; private set; }

	[SerializeField] private Faction faction;
	[Required]
	[SerializeField] private StateMachine stateMachine;

	private PlayerInteractor interactor;
	private InputMaster inputMaster;

	private void Awake() {
		InputMaster inputMaster = new InputMaster();
		Input = inputMaster.Player;
		Input.Enable();

		interactor = new PlayerInteractor(Input, faction);
		interactor.Enable();// Should be decided by player states in the future
	}

	private void Start() {
		stateMachine.Initialize(this);
	}

}