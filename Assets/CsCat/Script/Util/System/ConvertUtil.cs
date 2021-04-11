using System;

namespace CsCat
{
  public class ConvertUtil
  {
    public static bool CanConvertToType(Type source_type, Type target_type)
    {
      if (source_type == null)
        return true;
      if (source_type.IsPrimitive && target_type.IsPrimitive)
      {
        var source_code = Type.GetTypeCode(source_type);
        var target_code = Type.GetTypeCode(target_type);
        // If both type1 and type2 have the same type, return true.
        if (source_code == target_code)
          return true;
        // Possible conversions from Char follow.
        if (source_code == TypeCode.Char)
          switch (target_code)
          {
            case TypeCode.UInt16: return true;
            case TypeCode.UInt32: return true;
            case TypeCode.Int32: return true;
            case TypeCode.UInt64: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from Byte follow.
        if (source_code == TypeCode.Byte)
          switch (target_code)
          {
            case TypeCode.Char: return true;
            case TypeCode.UInt16: return true;
            case TypeCode.Int16: return true;
            case TypeCode.UInt32: return true;
            case TypeCode.Int32: return true;
            case TypeCode.UInt64: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from SByte follow.
        if (source_code == TypeCode.SByte)
          switch (target_code)
          {
            case TypeCode.Int16: return true;
            case TypeCode.Int32: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from UInt16 follow.
        if (source_code == TypeCode.UInt16)
          switch (target_code)
          {
            case TypeCode.UInt32: return true;
            case TypeCode.Int32: return true;
            case TypeCode.UInt64: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from Int16 follow.
        if (source_code == TypeCode.Int16)
          switch (target_code)
          {
            case TypeCode.Int32: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from UInt32 follow.
        if (source_code == TypeCode.UInt32)
          switch (target_code)
          {
            case TypeCode.UInt64: return true;
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from Int32 follow.
        if (source_code == TypeCode.Int32)
          switch (target_code)
          {
            case TypeCode.Int64: return true;
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from UInt64 follow.
        if (source_code == TypeCode.UInt64)
          switch (target_code)
          {
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from Int64 follow.
        if (source_code == TypeCode.Int64)
          switch (target_code)
          {
            case TypeCode.Single: return true;
            case TypeCode.Double: return true;
            default: return false;
          }

        // Possible conversions from Single follow.
        if (source_code == TypeCode.Single)
          switch (target_code)
          {
            case TypeCode.Double: return true;
            default: return false;
          }

        return false;
      }

      if (target_type.IsByRef) target_type = target_type.GetElementType();
      // 总是可以转换为 Object。
      if (target_type == typeof(object)) return true;
//      if (source_type == target_type || source_type.IsSubclassOf(target_type)) return true;
      if (source_type.IsSubTypeOf(target_type))
        return true;
      //Nullable处理
      Type underlyingType = Nullable.GetUnderlyingType(target_type);
      if (underlyingType != null)
      {
//        if (source_type == underlyingType || source_type.IsSubclassOf(underlyingType))
        if(source_type.IsSubTypeOf(underlyingType))
          return true;
      }
      return false;
    }
  }
}