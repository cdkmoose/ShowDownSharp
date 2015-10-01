using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

using DS.Showdown.ShowdownGameSvc;

namespace ShowdownGameServer
{
    public partial class GameServerForm : Form
    {
        ServiceHost host;
        Uri baseAddress = new Uri("http://10.0.9.112:8750/ShowdownGameSvc");
        public GameServerForm()
        {
            InitializeComponent();
        }

        private void GameServerForm_Load(object sender, EventArgs e)
        {
            statusList.Items.Clear();

            statusList.Items.Add("Initializing");

            host = new ServiceHost(typeof(GameService), baseAddress);
            host.AddDefaultEndpoints();

            statusList.Items.Add("Host started");
        }

        private void GameServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (host.State == CommunicationState.Opened)
                host.Close();
        }
    }
}
