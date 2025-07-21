using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns.ServiceLocator.Services;
using Patterns.ServiceLocator;
public class NewspaperTest : MonoBehaviour, INewspaperReader
{
    
    public NewspaperService newspaperService {get; set;}
    void OnEnable()
    {
        InitializeReader();
    }
    void OnDisable()
    {
        FinalizeReader();
    }

    public void InitializeReader()
    {
        if(newspaperService == null)
        newspaperService = ServiceLocator.Instance.GetService<NewspaperService>();

        newspaperService.SubscribeToNewspaper("Test",this);
    }
    public void FinalizeReader(){
        newspaperService.UnsubscribeFromNewspaper("Test",this);
    }

    public void UpdateReader(string newspaper, object[] parameters)
    {
        switch(newspaper){
            case "Test":
                string s = (string) parameters[0];
                int i = (int) parameters[1];
                float f = (float) parameters[2];
                Debug.Log($"{s}; {i}; {f}");
            break;
        }
    }
    
}
