using System;
using System.Reflection;
using System.Collections.Generic;
using FileConverter.Models;
using FileConverter.Data;

namespace FileConverter.Services
{
    public static class ConverterRegistrationScanner
    {
        private static IDictionary<string, IFileWriter> _supportedWriters;
        private static IDictionary<string, IFileReader> _supportedReaders;

        public static IDictionary<string, IFileWriter> SupportedWriters
        {
            get 
            {
                if (_supportedWriters == null)
                    _supportedWriters = BuildFromType<IFileWriter>();

                return _supportedWriters;
            }
        }

        public static IDictionary<string, IFileReader> SupportedReaders
        {
            get
            {
                if (_supportedReaders == null)
                    _supportedReaders = BuildFromType<IFileReader>();

                return _supportedReaders;
            }
        }

        private static IDictionary<string, TBase> BuildFromType<TBase>()
            where TBase : ITypeRegistration
        {
            var baseType = typeof(TBase);
            var converters = new Dictionary<string, TBase>(StringComparer.OrdinalIgnoreCase);

            var assembly = Assembly.GetAssembly(typeof(ConverterRegistrationScanner));

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
                    continue;

                if (type.IsAbstract)
                    continue;

                if (!baseType.IsAssignableFrom(type))
                    continue;

                var instance = (TBase)Activator.CreateInstance(type);
                converters.Add(instance.Extension, instance);
            }

            return converters;
        }
    }
}
