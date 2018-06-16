using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Message = Microsoft.Azure.ServiceBus.Message;
using System.Text;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static IQueueClient queueClient;
        const string ServiceBusConnectionString = "Endpoint=sb://proyecto3ope.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=aUdZFjbCMuSSe3KmwEyByhFfF8GAS+PH2SXESwM62q4=";
        const string QueueName = "prueba2";
        EditText verde;
        EditText amarillo;
        EditText rojo;
        Switch sVerde;
        Switch sAmarillo;
        Switch sRojo;
        string estadoVerde;
        string estadoAmarillo;
        string estadoRojo;
        static string dato;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            EditText verde = FindViewById<EditText>(Resource.Id.editVerde);
            Switch sVerde = FindViewById<Switch>(Resource.Id.sVerde);

            EditText amarillo = FindViewById<EditText>(Resource.Id.editAmarillo);
            Switch sAmarillo = FindViewById<Switch>(Resource.Id.sAmarillo);

            EditText rojo = FindViewById<EditText>(Resource.Id.editRojo);
            Switch sRojo = FindViewById<Switch>(Resource.Id.sRojo);

            Button translateButton = FindViewById<Button>(Resource.Id.button1);
            translateButton.Click += (o, e) => {
                if (sVerde.Checked)
                    estadoVerde = "si";
                else
                    estadoVerde = "no";

                if (sAmarillo.Checked)
                    estadoAmarillo = "si";
                else
                    estadoAmarillo = "no";

                if (sRojo.Checked)
                    estadoRojo = "si";
                else
                    estadoRojo = "no";


                dato = "Verde" + "-" + verde.Text + "-" + estadoVerde + ", "+
                        "Amarillo" + "-" + amarillo.Text + "-" + estadoAmarillo + ", " +
                        "Rojo" + "-" + rojo.Text+ "-" + estadoRojo;
                MainAsync().GetAwaiter().GetResult();
                //Toast.MakeText(this, dato, ToastLength.Short).Show();
            };
        }

        static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            await SendMessagesAsync();
            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync()
        {
            string messageBody = dato;
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await queueClient.SendAsync(message);
            return;
        }  
    }
}

