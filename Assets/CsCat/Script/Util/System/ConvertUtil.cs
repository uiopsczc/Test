using System;

namespace CsCat
{
    public class ConvertUtil
    {
        public static bool CanConvertToType(Type sourceType, Type targetType)
        {
            if (sourceType == null)
                return true;
            if (sourceType.IsPrimitive && targetType.IsPrimitive)
            {
                var sourceCode = Type.GetTypeCode(sourceType);
                var targetCode = Type.GetTypeCode(targetType);
                // If both type1 and type2 have the same type, return true.
                if (sourceCode == targetCode)
                    return true;
                switch (sourceCode)
                {
                    // Possible conversions from Char follow.
                    case TypeCode.Char:
                        switch (targetCode)
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
                    case TypeCode.Byte:
                        switch (targetCode)
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
                    case TypeCode.SByte:
                        switch (targetCode)
                        {
                            case TypeCode.Int16: return true;
                            case TypeCode.Int32: return true;
                            case TypeCode.Int64: return true;
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    // Possible conversions from UInt16 follow.
                    case TypeCode.UInt16:
                        switch (targetCode)
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
                    case TypeCode.Int16:
                        switch (targetCode)
                        {
                            case TypeCode.Int32: return true;
                            case TypeCode.Int64: return true;
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                        break;
                    // Possible conversions from UInt32 follow.
                    case TypeCode.UInt32:
                        switch (targetCode)
                        {
                            case TypeCode.UInt64: return true;
                            case TypeCode.Int64: return true;
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    // Possible conversions from Int32 follow.
                    case TypeCode.Int32:
                        switch (targetCode)
                        {
                            case TypeCode.Int64: return true;
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    // Possible conversions from UInt64 follow.
                    case TypeCode.UInt64:
                        switch (targetCode)
                        {
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    // Possible conversions from Int64 follow.
                    case TypeCode.Int64:
                        switch (targetCode)
                        {
                            case TypeCode.Single: return true;
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    // Possible conversions from Single follow.
                    case TypeCode.Single:
                        switch (targetCode)
                        {
                            case TypeCode.Double: return true;
                            default: return false;
                        }

                    default:
                        return false;
                }
            }

            if (targetType.IsByRef) targetType = targetType.GetElementType();
            // 总是可以转换为 Object。
            if (targetType == typeof(object)) return true;
            //      if (source_type == target_type || source_type.IsSubclassOf(target_type)) return true;
            if (sourceType.IsSubTypeOf(targetType))
                return true;
            //Nullable处理
            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType != null)
            {
                //        if (source_type == underlyingType || source_type.IsSubclassOf(underlyingType))
                if (sourceType.IsSubTypeOf(underlyingType))
                    return true;
            }

            return false;
        }
    }
}