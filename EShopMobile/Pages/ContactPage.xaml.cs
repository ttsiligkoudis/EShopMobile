using Client;
using DataModels.Dtos;
using EShopMobile.Helpers;
using Helpers;
using Maui.GoogleMaps;

namespace EShopMobile.Pages;

public partial class ContactPage : BasePage
{
    private readonly IClient _client;
    private double _lastX, _lastY;

    readonly Pin _pinOffice = new()
    {
        Type = PinType.Place,
        Label = "Office",
        Address = "Damianou 10, Kavala, Greece",
        Position = new Position(40.93909798857936d, 24.40787526859458d)
    };

    public ContactPage(IClient client)
	{
        InitializeComponent();
        _client = client;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MyMap.Pins.Add(_pinOffice);
        MyMap.UiSettings.ScrollGesturesEnabled = true;
        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnMapPan;
        MyMap.GestureRecognizers.Add(panGesture);
        var hasPermission = Task.Run(MauiProgram.CheckLocationPermission).Result;

        if (hasPermission)
        {
            MyMap.UiSettings.MyLocationButtonEnabled = true;
            MyMap.MyLocationEnabled = true;
        }
    }

    private async void BtnSendMessage_Clicked(object sender, EventArgs e)
    {
        Loader.IsRunning = true;
        Loader.IsVisible = true;
        Frame.IsVisible = true;

        string errors;

        errors = string.IsNullOrEmpty(Name.Text) ? "Name" : string.Empty;
        errors += string.IsNullOrEmpty(Email.Text) ? " Email" : string.Empty;
        errors += string.IsNullOrEmpty(Message.Text) ? " Message" : string.Empty;

        if (!string.IsNullOrEmpty(errors))
        {
            errors += "not Valid";
            AlertService.DisplayAlert("Operation Issue", errors, "Ok");
            return;
        }

        var message = new MessageDto
        {
            Email = Email.Text,
            Subject = "Contact Form Submission",
            Body = EmailHelper.ContactMessageHtml(Name.Text, Email.Text, Message.Text)
        };

        await _client.PostAsync(message, $"Messages/SendMessage");
        var text = "We have successfully received your message and our team will get back to you as soon as possible.";
        Loader.IsRunning = false;
        Loader.IsVisible = false;
        Frame.IsVisible = false;
        AlertService.DisplayAlert("Operation completed successfully", text, "Ok");
    }

    private void BtnForm_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var show = btn.Text != "Hide";
        ChangeFormVisibility(show);
        btn.Text = show ? "Hide" : "Show";
    }

    private void ChangeFormVisibility(bool show)
    {
        Text.IsVisible = show;
        Name.IsVisible = show;
        Email.IsVisible = show;
        Message.IsVisible = show;
        SendMessage.IsVisible = show;

    }

    private void OnMapPan(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Started)
        {
            _lastX = MyMap.TranslationX;
            _lastY = MyMap.TranslationY;
            //ScrollView.IsEnabled = false;
        }
        else if (e.StatusType == GestureStatus.Running)
        {
            var newX = _lastX + e.TotalX;
            var newY = _lastY + e.TotalY;

            MyMap.TranslationX = newX;
            MyMap.TranslationY = newY;
        }
        else if (e.StatusType == GestureStatus.Canceled || e.StatusType == GestureStatus.Completed)
        {
            //ScrollView.IsEnabled = true;
        }
    }
}