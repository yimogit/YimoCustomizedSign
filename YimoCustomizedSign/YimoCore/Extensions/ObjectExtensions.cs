using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace YimoCore
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object obj)
        {
            if (obj != null)
            {
                return (T)Convert.ChangeType(obj, typeof(T), null);
            }

            return default(T);
        }

        public static T DeepClone<T>(this T obj) where T : class
        {
            T objResult;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                objResult = (T)formatter.Deserialize(stream);
            }

            return objResult;
        }
        /// <summary>
        /// 将对象序列化为JSON字符串，不支持存在循环引用的对象
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">动态类型对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);

        }
        /// <summary>
        /// 将JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">要转换的目标类型</typeparam>
        /// <param name="json">JSON字符串 </param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
