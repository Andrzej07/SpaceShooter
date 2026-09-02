using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace SpaceShooter
{
	[CreateAssetMenu]
	public class LabelDataSource : ScriptableObject, IDataSourceViewHashProvider, INotifyBindablePropertyChanged
	{
		[CreateProperty]
		public string labelContent;
		
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
		
		public long GetViewHashCode()
		{
			return labelContent?.GetHashCode() ?? 0;
		}

		public void Commit()
		{
			propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(nameof(labelContent)));
		}
	}
}