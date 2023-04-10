using Maui.GoogleMaps;
using System;

namespace EShopMobile.Pages;

public partial class ContactPage : BasePage
{
    readonly Pin _pinOffice = new()
    {
        Type = PinType.Place,
        Label = "Office",
        Address = "Damianou 10, Kavala, Greece",
        Position = new Position(40.93909798857936d, 24.40787526859458d)
    };

    public ContactPage()
	{
        InitializeComponent();
        MyMap.Pins.Add(_pinOffice);
        MyMap.UiSettings.ScrollGesturesEnabled = true;

        var hasPermission = Task.Run(MauiProgram.CheckLocationPermission).Result;

        if (hasPermission)
        {
            MyMap.UiSettings.MyLocationButtonEnabled = true;
            MyMap.MyLocationEnabled = true;
        }
    }
}