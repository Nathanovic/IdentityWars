using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableVariable<Y, T> : ScriptableObject where Y : ScriptableVariable<Y, T> {

	public abstract T DefaultValue { get; }

	protected T Value;

	private Dictionary<IUniqueModel, Y> Variables = new Dictionary<IUniqueModel, Y>();

	public virtual T GetValue(IUniqueModel model) {
		Y var = GetVariable(model);
		return var.Value;
	}

	public virtual T SetValue(IUniqueModel model) {
		Y var = GetVariable(model);
		return var.Value;
	}

	private Y GetVariable(IUniqueModel model) {
		if (Variables.ContainsKey(model)) {
			return Variables[model];
		}
		else {
			Y newVar = Instantiate((Y)this);
			Variables.Add(model, newVar);
			return newVar;
		}
	}

}