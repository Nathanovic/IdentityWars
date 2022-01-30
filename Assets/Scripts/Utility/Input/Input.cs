public class Input : MonoSingleton<Input> {

	public InputMaster InputMaster { get; private set; }

	protected override void Initialize() {
		InputMaster = new InputMaster();
	}

}