﻿using Easy.Extend;
using Easy.IOCAdapter;
using Easy.IOCAdapter.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    public static class Loader
    {
        public static T CreateInstance<T>()
        {
            Type ty = typeof(T);
            if ((ty.IsInterface || ty.IsAbstract))
            {
                if (Container.All.ContainsKey(ty))
                {
                    return (T)System.Activator.CreateInstance(Container.All[ty].First());
                }
                else
                {
                    return default(T);
                    //throw new UnregisteredException(ty);
                }
            }
            else if (!ty.IsInterface)
            {
                return System.Activator.CreateInstance<T>();
            }
            return default(T);
        }
        public static T CreateInstance<T>(string assemblyName, string fullClassName) where T : class
        {
            return System.Activator.CreateInstance(assemblyName, fullClassName).Unwrap() as T;
        }

        public static List<T> ResolveAll<T>(Type _interface) where T : class
        {
            var result = new List<T>();
            if (Container.All.ContainsKey(_interface))
            {
                Container.All[_interface].ForEach(m => result.Add(Activator.CreateInstance(m) as T));
            }
            else
            {
                AppDomain.CurrentDomain.GetAssemblies().Each(m => m.GetTypes().Each(type =>
                {
                    if (type.IsClass && _interface.IsAssignableFrom(type))
                    {
                        Container.Register(_interface, type);
                        result.Add(Activator.CreateInstance(type) as T);
                    }
                }));
            }
            return result;
        }
        public static object CreateInstance(Type type)
        {
            if (Container.All.ContainsKey(type))
            {
                type = Container.All[type].First();
            }
            return Activator.CreateInstance(type);
        }
        public static Type GetType<T>()
        {
            Type ty = typeof(T);
            if (Container.All.ContainsKey(ty))
            {
                return Container.All[ty].First();
            }
            else
            {
                return ty;
            }
        }
        public static Type GetType(Type t)
        {
            if (Container.All.ContainsKey(t))
            {
                return Container.All[t].First();
            }
            else
            {
                return t;
            }

        }
    }
}
