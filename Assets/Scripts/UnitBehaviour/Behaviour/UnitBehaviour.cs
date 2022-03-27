using UnityEngine;

// Set execution order so all behaviours can execute before the states go and check whether the job is finished
[DefaultExecutionOrder(-1)]
public abstract class UnitBehaviour<T> : MonoBehaviour, IUnitBehaviour {

	public bool IsActive { get; private set; }

	protected Animator animator { get; private set; }
	protected new Transform transform { get; private set; }
	protected new Rigidbody rigidbody { get; private set; }
	protected UnitSkillSet skillSet { get; private set; }
	protected int skillValue {
		get { 
			return skillSet.GetValue(requiredSkill); 
		} 
	}

	[AssetDropdown("Settings/Skills")]
	[SerializeField] private Skill requiredSkill;

	public void Initialize(Animator animator, Unit unit) {
		this.animator = animator;
		transform = unit.transform;
		rigidbody = unit.GetComponent<Rigidbody>();
		skillSet = unit.SkillSet;

		if (requiredSkill != null && !skillSet.HasSkil(requiredSkill)) {
			Debug.LogWarning("Required skill is not available in unit skillset: " + requiredSkill.name + ", this behaviour will not work: " + name, this);
			name = name + " (SKILL MISSING)";
			gameObject.SetActive(false);
		}
	}

	public void StartBehaviour(T data) {
		IsActive = true;
		OnStart(data);
	}

	public void StopBehaviour() {
		IsActive = false;
		OnStop();
	}

	private void Update() {
		if (!IsActive) { return; }
		OnUpdate();
	}
	private void FixedUpdate() {
		if (!IsActive) { return; }
		OnFixedUpdate();
	}

	protected abstract void OnStart(T data);// Potentially later on add OnStart without data and make both optional
	protected virtual void OnStop() { }
	protected virtual void OnUpdate() { }
	protected virtual void OnFixedUpdate() { }

}