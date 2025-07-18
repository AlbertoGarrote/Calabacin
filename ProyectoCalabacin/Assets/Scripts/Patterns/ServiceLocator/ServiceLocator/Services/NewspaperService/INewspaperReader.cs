using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator.Services;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public interface INewspaperReader
    {
        public NewspaperService newspaperService { get; set; }
        public void InitializeReader();
        public void FinalizeReader();
        public void UpdateReader(string newspaper, object[] parameters);
    }
}
