using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLibrary.Infrastructure.Ioc
{
    public interface IContainer
    {
        //void Register<TFrom, TTo>(string key = "") where TTo : TFrom;

        //void Register<TInterface>(TInterface instance, string key = "");

        T Resolve<T>(string key = "");

        //object Resolve(Type type, string key = "");

        //void Remove(object instance);
    }
}
