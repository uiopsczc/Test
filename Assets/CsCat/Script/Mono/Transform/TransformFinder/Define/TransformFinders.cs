using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class TransformFinders : ICopyable
	{
		[SerializeField] private TransformFinder0 _transformFinder0 = new TransformFinder0();
		[SerializeField] private TransformFinder1 _transformFinder1 = new TransformFinder1();

		public TransformFinderBase this[int index]
		{
			get => this.GetFieldValue<TransformFinderBase>(string.Format("_transformFinder{0}", index));
			set => this.SetFieldValue(string.Format("_transformFinder{0}", index), value);
		}

		public void CopyTo(object dest)
		{
			var _dest = dest as TransformFinders;
			_transformFinder0.CopyTo(_dest._transformFinder0);
			_transformFinder1.CopyTo(_dest._transformFinder1);
		}

		public void CopyFrom(object source)
		{
			var _source = source as TransformFinders;
			_transformFinder0.CopyFrom(_source._transformFinder0);
			_transformFinder1.CopyFrom(_source._transformFinder1);
		}
	}
}