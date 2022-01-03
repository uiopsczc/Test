using System.Collections;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
	public static partial class CompositeCollider2DExtension
	{
		public static Hashtable GetSerializeHashtable(this CompositeCollider2D self)
		{
			Hashtable hashtable = new Hashtable();
			hashtable[StringConst.String_isTrigger] = self.isTrigger;
			hashtable[StringConst.String_usedByEffector] = self.usedByEffector;
			hashtable[StringConst.String_offset] = self.offset.ToStringOrDefault();
			hashtable[StringConst.String_geometryType] = (int)self.geometryType;
			hashtable[StringConst.String_generationType] = (int)self.generationType;
			hashtable[StringConst.String_vertexDistance] = self.vertexDistance;
			hashtable[StringConst.String_offsetDistance] = self.offsetDistance;
			hashtable[StringConst.String_edgeRadius] = self.edgeRadius;
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(this CompositeCollider2D self, Hashtable hashtable)
		{
			self.isTrigger = hashtable.Get<bool>(StringConst.String_isTrigger);
			self.usedByEffector = hashtable.Get<bool>(StringConst.String_usedByEffector);
			self.offset = hashtable.Get<string>(StringConst.String_offset).ToVector2OrDefault();
			self.geometryType = hashtable.Get<int>(StringConst.String_geometryType)
				.ToEnum<CompositeCollider2D.GeometryType>();
			self.generationType = hashtable.Get<int>(StringConst.String_generationType)
				.ToEnum<CompositeCollider2D.GenerationType>();
			self.vertexDistance = hashtable.Get<float>(StringConst.String_vertexDistance);
			self.offsetDistance = hashtable.Get<float>(StringConst.String_offsetDistance);
			self.edgeRadius = hashtable.Get<float>(StringConst.String_edgeRadius);
		}
	}
}