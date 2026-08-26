using System;

namespace SpaceShooter
{
	[Flags]
	public enum ObjectTags
	{
		Empty = 0,
		Player = 1 << 0
	}
}