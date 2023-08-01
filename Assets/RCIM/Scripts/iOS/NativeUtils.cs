#if UNITY_IOS
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace cn_rongcloud_im_unity
{
    class NativeUtils
    {
        internal static string PtrToString(IntPtr p)
        {
            if (p == IntPtr.Zero)
                return "";
            return Marshal.PtrToStringAnsi(p);
        }

        internal static void GetStructListByPtr<StructType>(ref StructType[] list, IntPtr ptr, long count)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            for (int i = 0; i < count; ++i)
            {
                IntPtr item_ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(StructType)) * i);

                if ((list != null) && (item_ptr != null))
                {
                    list[i] = (StructType)Marshal.PtrToStructure(item_ptr, typeof(StructType));
                }
            }
        }
        internal static void GetStructListByPtr<StructType>(ref StructType[] list, IntPtr ptr, int count)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            for (int i = 0; i < count; ++i)
            {
                IntPtr item_ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(StructType)) * i);

                if ((list != null) && (item_ptr != null))
                {
                    list[i] = (StructType)Marshal.PtrToStructure(item_ptr, typeof(StructType));
                }
            }
        }

        internal static void GetStructListByPtr<StructType>(ref List<StructType> list, IntPtr ptr, uint count)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            for (uint i = 0; i < count; ++i)
            {
                IntPtr item_ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(StructType)) * i);

                if ((list != null) && (item_ptr != null))
                {
                    list.Add((StructType)Marshal.PtrToStructure(item_ptr, typeof(StructType)));
                }
            }
        }
        internal static StructType GetStructByPtr<StructType>(IntPtr ptr)
        {
            return (StructType)Marshal.PtrToStructure(ptr, typeof(StructType));
        }

        internal static IntPtr GetStructPointer(ValueType a)
        {
            int nSize = Marshal.SizeOf(a);                 //定义指针长度
            IntPtr pointer = Marshal.AllocHGlobal(nSize);        //定义指针
            Marshal.StructureToPtr(a, pointer, false);                //将结构体a转为结构体指针
            return pointer;
        }

        internal static IntPtr GetStructListPointer<StructType>(ref List<StructType> clist)
        {
            int size = Marshal.SizeOf(typeof(StructType));
            IntPtr list_ptr = Marshal.AllocHGlobal(clist.Count * size);
            for (int i = 0; i < clist.Count; i++)
            {
                Marshal.StructureToPtr(clist[i], list_ptr+size*i, false);
            }
            ios_c_list map;
            map.list = list_ptr;
            map.count = clist.Count;
            IntPtr result_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ios_c_list)));
            Marshal.StructureToPtr(map, result_ptr, false);
            return result_ptr;
        }

        internal static List<StructType> GetObjectListByPtr<StructType>(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return new List<StructType>();
            }
            ios_c_list clist = Marshal.PtrToStructure<ios_c_list>(ptr);
            StructType[] cobjects = new StructType[clist.count];
            GetStructListByPtr(ref cobjects, clist.list, clist.count);
            return new List<StructType>(cobjects);
        }

        internal static StructType[] GetObjectListByStruct<StructType>(ref ios_c_list clist)
        {
            StructType[] cobjects = new StructType[clist.count];
            GetStructListByPtr(ref cobjects, clist.list, clist.count);
            return cobjects;
        }

        internal static void FreeStructListByPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            ios_c_list clist = Marshal.PtrToStructure<ios_c_list>(ptr);
            if (clist.list != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(clist.list);
            }
        }

        internal static IntPtr GetStringListPointer(List<string> clist)
        {
            IntPtr[] ptrArray = new IntPtr[clist.Count];
            for (int i = 0; i < clist.Count; i++)
            {
                ptrArray[i] = Marshal.StringToHGlobalAnsi(clist[i]);
            }
            IntPtr list_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * clist.Count);
            for (int j = 0; j < clist.Count; j++)
            {
                IntPtr item_ptr = new IntPtr(list_ptr.ToInt64() + Marshal.SizeOf(typeof(IntPtr)) * j);
                Marshal.StructureToPtr(ptrArray[j], item_ptr, false);
            }
            ios_c_list map;
            map.list = list_ptr;
            map.count = clist.Count;
            IntPtr result_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ios_c_list)));
            Marshal.StructureToPtr(map, result_ptr, false);
            return result_ptr;
        }

        internal static List<string> GetStringListByPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return new List<string>();
            }
            ios_c_list clist = Marshal.PtrToStructure<ios_c_list>(ptr);
            List<string> cobjects = new List<string>();
            for (int i = 0; i < clist.count; i++)
            {
                IntPtr item_ptr = new IntPtr(clist.list.ToInt64() + Marshal.SizeOf(typeof(IntPtr)) * i);
                IntPtr str_ptr = Marshal.PtrToStructure<IntPtr>(item_ptr);
                cobjects.Add(PtrToString(str_ptr));
            }
            return cobjects;
        }

        internal static void FreeStringListByPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            ios_c_list clist = Marshal.PtrToStructure<ios_c_list>(ptr);
            if (clist.list != IntPtr.Zero)
            {
                for (int i = 0; i < clist.count; i++)
                {
                    IntPtr item_ptr = new IntPtr(clist.list.ToInt64() + Marshal.SizeOf(typeof(IntPtr)) * i);
                    IntPtr str_ptr = Marshal.PtrToStructure<IntPtr>(item_ptr);
                    Marshal.FreeHGlobal(str_ptr);
                }
                Marshal.FreeHGlobal(clist.list);
            }
        }

        internal static IntPtr GetStructMapPointer<StructType>(ref List<StructType> clist)
        {
            int size = Marshal.SizeOf(typeof(StructType));
            IntPtr list_ptr = Marshal.AllocHGlobal(clist.Count * size);
            for (int i = 0; i < clist.Count; i++)
            {
                Marshal.StructureToPtr(clist[i], list_ptr+size*i, false);
            }
            ios_c_list map;
            map.list = list_ptr;
            map.count = clist.Count;
            IntPtr map_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ios_c_list)));
            Marshal.StructureToPtr(map, map_ptr, false);
            return map_ptr;
        }

        internal static Dictionary<K,V> GetObjectMapByPtr<K,V>(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return null;
            }
            Dictionary<K,V> result = new Dictionary<K,V>();
            ios_c_list clist = Marshal.PtrToStructure<ios_c_list>(ptr);
            ios_c_map_item[] cobjects = new ios_c_map_item[clist.count];
            GetStructListByPtr(ref cobjects, clist.list, clist.count);
            foreach (var item in cobjects)
            {
                K key = (K)ConvertString<K>(item.key);
                V value = (V)ConvertString<V>(item.value);
                if (key != null && value != null)
                {
                    result.Add(key, value);
                }
            }
            return result;
        }

        internal static Dictionary<K, V> GetObjectMapByStruct<K, V>(ref ios_c_list clist)
        {
            ios_c_map_item[] cobjects = new ios_c_map_item[clist.count];
            GetStructListByPtr(ref cobjects, clist.list, clist.count);
            Dictionary<K, V> result = new Dictionary<K, V>();
            foreach (var item in cobjects)
            {
                K key = (K)ConvertString<K>(item.key);
                V value = (V)ConvertString<V>(item.value);
                result.Add(key, value);
            }
            return result;
        }

        private static object ConvertString<T>(string text)
        {
            if (typeof(T).IsInstanceOfType(text))
            {
                return text;
            }
            object ret;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            bool flag = converter.CanConvertFrom(text.GetType());
            if (!flag)
            {
                converter = TypeDescriptor.GetConverter(text.GetType());
            }
            try
            {
                ret = flag ? converter.ConvertFrom(null, null, text) : converter.ConvertTo(null, null, text, typeof(T));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("类型转换出错：" + text + "==>" + typeof(T), e);
            }
            return ret;
        }

        internal static IntPtr GetObjPointer(System.Object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj);
            IntPtr result = (IntPtr)handle;
            handle.Free();
            return result;
        }

        internal static void ReleaseAllStructPointers(ArrayList arrayList)
        {
            foreach (System.IntPtr item in arrayList)
            {

                Marshal.FreeHGlobal(item);//释放分配的非托管内存

            }
            arrayList.Clear();
        }

        #region callback map
	
        internal static ConcurrentDictionary<Int64, object> _blockObjMap = new ConcurrentDictionary<Int64, object>();

        static Int64 _seqId;

        static Int64 GetSeqId()
        {
            _seqId++;
            return _seqId;
        }

        internal static Int64 AddCallback<T>(T callback)
        {
            if (callback != null)
            {
                Int64 handle = GetSeqId();
                _blockObjMap[handle] = callback;
                return handle;
            }
            return -1;
        }

        internal static T GetCallback<T>(Int64 handle) where T : class
        {
            object callback = _blockObjMap[handle];
            if (callback != null)
            {
                T result = callback as T;
                return result;
            }
            return null;
        }

        internal static T TakeCallback<T>(Int64 handle) where T : class
        {
            _blockObjMap.TryRemove(handle, out var callback);
            return callback as T;
        }

        #endregion
    }
}
#endif