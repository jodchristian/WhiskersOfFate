using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject 
{
    public partial class GameLose : MetroFramework.Forms.MetroForm
    {
        public bool GoRetry;
        public GameLose()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GameLose Internal Error: " + ex.Message);
                throw;
            }
        }
        private void buttonyes_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Yes button clicked!");  

            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void buttonno_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0 );
        }
    }
}
