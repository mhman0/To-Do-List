﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Calorie_Counter.Properties.ViewModel;

//Implimenting MVVM
//// Define a class named MainViewModel that implements the INotifyPropertyChanged interface.
//public class MainViewModel : INotifyPropertyChanged
//{
//    // Declare a private field to store the text value.
//    string text;

//    // Define a public property named Text that gets and sets the value of the text field.
//    public string Text
//    {
//        get => text; // Getter returns the current value of the text field.
//        set
//        {
//            text = value; // Setter sets the value of the text field to the provided value.

//            // Invoke the OnPropertyChanged method to notify subscribers about the property change.
//            OnPropertyChanged(nameof(Text));
//        }
//    }

//    // Declare an event of type PropertyChangedEventHandler, which is raised when a property value changes.
//    public event PropertyChangedEventHandler PropertyChanged;

//    // Define a method named OnPropertyChanged that takes the name of the changed property.
//    void OnPropertyChanged(string name) =>
//        // Invoke the PropertyChanged event, passing this instance and PropertyChangedEventArgs with the property name.
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//}

// Declare a partial class named MainViewModel that extends ObservableObject.
public partial class MainViewModel : ObservableObject
{
    IConnectivity conenctivity;
    // Constructor for the MainViewModel class.
    public MainViewModel(IConnectivity conenctivity)
    {
        // Initialize the 'Items' property with a new instance of ObservableCollection<string>.
#pragma warning disable IDE0028 // Simplify collection initialization
        Items = new ObservableCollection<string>();
        this.conenctivity = conenctivity;
#pragma warning restore IDE0028 // Simplify collection initialization
    }

    // Apply the ObservableProperty attribute to the 'items' field.
    // The ObservableProperty attribute generates property-change notification code.
    [ObservableProperty]
    // Declare an ObservableCollection<string> field named 'items'.
    ObservableCollection<string> items;


    // Apply the ObservableProperty attribute to the 'text' field.
    // The ObservableProperty attribute generates property-change notification code.
    [ObservableProperty]
    // Declare a nullable string field named 'text'.
    string text;

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if(conenctivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
            return;
        }

            Items.Add(Text);
//add our item and set text property to empty string
            Text = string.Empty;
            
    }

    [RelayCommand]
    void Delete(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        //referencing the detail page info
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}

