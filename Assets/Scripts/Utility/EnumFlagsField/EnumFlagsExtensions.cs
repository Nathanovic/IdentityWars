using System;

public static class EnumFlagsExtensions {
	public static bool HasFlag(this Enum mask, Enum flags) {
#if UNITY_EDITOR
		if (mask.GetType() != flags.GetType())
			throw new ArgumentException(
				string.Format("The argument type, '{0}', is not the same as the enum type '{1}'.",
				flags.GetType(), mask.GetType()));
#endif
		return ((int)(IConvertible)mask & (int)(IConvertible)flags) == (int)(IConvertible)flags;
	}
}