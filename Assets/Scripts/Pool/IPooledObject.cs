using System;

namespace SpaceShooter
{
	public interface IPooledObject<T>
	{
		Action<T> ReturnToPoolAction { get; set; }
	}
}