using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PusherServer;

namespace PusherTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Pusher can be installed using Nuget with "Install-Package PusherServer -Pre"
            //Alternatively, You can download it and add it to your project manually from https://github.com/pusher/pusher-dotnet-server

            //Create pusher object using YOUR APP KEY and YOUR SECRET APP KEY
            var Pusher = new Pusher("App Id", "APP KEY", "SECRET KEY");

            //Trigger an event on some channel with a message
            var result = Pusher.Trigger("channel1", "test_event", new { message = txtMessage.Text});

            txtMessage.Text = "";

            //result will hold some data about what happened when the event was triggered. StatusCode should just be OK when successful.
            //MessageBox.Show(result.StatusCode + ":" + result.Body);
        }
    }
}
