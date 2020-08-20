using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrowsePearls.Utility.ENUM
{
    public static class EnumHelper<TEnum> where TEnum : struct
    {
        #region 校验一个整数，字符串，或枚举变量是不是一个指定的枚举类型的合法的存在形式
        public static bool IsDefined(string name) 
        {
            return Enum.IsDefined(typeof(TEnum), name);
        }

        public static bool IsDefined(int value) 
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        public static bool IsDefined(TEnum enumObj)
        {
            return Enum.IsDefined(typeof(TEnum), enumObj);
        }
        #endregion

        #region 枚举和数字互转
        public static int Enum2Value(TEnum enumObj, bool validation = true)
        {
            if (validation)
            {
                if (!Enum.IsDefined(typeof(TEnum), enumObj))
                {
                    throw new ArgumentException($"Enum Instance's value is {enumObj} and it isn't defined.");
                }
            }
            return int.Parse(Enum.Format(typeof(TEnum), enumObj, "D"));
        }

        public static TEnum Value2Enum(int value, bool validation = true)
        {
            if (validation)
            {
                if (!Enum.IsDefined(typeof(TEnum), value))
                {
                    throw new ArgumentException($"Failed because {value} isn't defined.");
                }
            }
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }
        #endregion

        #region 枚举和字符串互转
        public static string Enum2Name(TEnum enumObj, bool validation = true)
        {
            if (validation)
            {
                if (!Enum.IsDefined(typeof(TEnum), enumObj))
                {
                    throw new ArgumentException($"Enum Instance's value is {enumObj} and it isn't defined.");
                }
            }
            return enumObj.ToString();
        }

        public static TEnum Name2Enum(string enumName, bool ignoreCase = true)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), enumName, ignoreCase);
        }
        #endregion

        #region 数字和字符串互转
        public static string Value2Name(int value,bool validation = true)
        {
            if (validation)
            {
                if (!Enum.IsDefined(typeof(TEnum), value))
                {
                    throw new ArgumentException($"Failed because {value} isn't defined.");
                }
            }
            return Value2Enum(value, false).ToString();
        }

        public static int Name2Value(string name)
        {
            return (int)Enum.Parse(typeof(TEnum), name);
        }
        #endregion

        #region 整体信息
        public static string[] GetNames()
        {
            return Enum.GetNames(typeof(TEnum));
        }

        public static int[] GetValues()
        {
            return Enum.GetValues(typeof(TEnum)) as int[];
        }

        public static TEnum[] GetEnums()
        {
            return Enum.GetValues(typeof(TEnum)) as TEnum[];
        }
        #endregion

        #region 获取枚举的描述特性
        public static string GetEnumDescription(TEnum enumObj, bool validation = true)
        {
            if (validation)
            {
                if (!Enum.IsDefined(typeof(TEnum), enumObj))
                {
                    throw new ArgumentException($"Enum Instance's value is {enumObj} and it isn't defined.");
                }
            }
            string value = enumObj.ToString();
            FieldInfo field = enumObj.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) //不存在描述属性时，返回枚举的字符串形式
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        public static string GetEnumDescription(int value, bool validation = true)
        {
            return GetEnumDescription(Value2Enum(value, validation), validation);
        }

        public static string GetEnumDescription(string name, bool validation = true)
        {
            return GetEnumDescription(Name2Enum(name, validation), validation);
        }
        #endregion
    }
}