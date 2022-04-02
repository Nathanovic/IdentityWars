public class Input : MonoSingleton<Input> {

	public InputMaster InputMaster { get; private set; }

	private void Awake() {
		InputMaster = new InputMaster();
	}

}