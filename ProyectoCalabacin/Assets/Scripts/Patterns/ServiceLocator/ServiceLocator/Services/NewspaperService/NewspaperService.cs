using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns.ServiceLocator.Interfaces;
using System;

namespace Patterns.ServiceLocator.Services
{
    public class NewspaperService: IService
    {
        public void InitializeService()
        {

        }

        private readonly Dictionary<string, List<INewspaperReader>> newspapersDictionary = new Dictionary<string, List<INewspaperReader>>();
        public void RegisterNewspaper(string newspaperName)
        {

            if (!newspapersDictionary.ContainsKey(newspaperName))
            {
                newspapersDictionary.Add(newspaperName, new List<INewspaperReader>());
            }
        }
        public void UnregisterNewspaper(string newspaperName)
        {
            if (newspapersDictionary.ContainsKey(newspaperName))
            {
                newspapersDictionary.Remove(newspaperName);
            }
        }
        public void SubscribeToNewspaper(string newspaperName, INewspaperReader reader)
        {
            if (newspapersDictionary.TryGetValue(newspaperName, out var newspaperReaders))
            {
                if (!newspaperReaders.Contains(reader))
                    newspaperReaders.Add(reader);
            }
            else
            {
                newspapersDictionary.Add(newspaperName, new List<INewspaperReader>() { reader });
            }
        }
        public void UnsubscribeFromNewspaper(string newspaperName, INewspaperReader reader)
        {
            if (newspapersDictionary.TryGetValue(newspaperName, out var newspaperReaders))
            {
                if (newspaperReaders.Contains(reader))
                    newspaperReaders.Remove(reader);
            }
        }
        public void PublishToNewspaper(string newspaperName, object[] parameters)
        {
            if (newspapersDictionary.TryGetValue(newspaperName, out var newspaperReaders))
            {
                foreach (var reader in newspaperReaders)
                {
                    reader.UpdateReader(newspaperName, parameters);
                }
            }
        }
    }
}