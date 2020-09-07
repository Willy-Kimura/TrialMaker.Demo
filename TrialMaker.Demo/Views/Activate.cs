using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.TrialMakerNS;
using WK.Libraries.TrialMakerNS.Models;

namespace TrialMakerDemo.CSharp.Views
{
    public partial class Activate : Form
    {
        #region Constructor

        public Activate()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void ShowBusy()
        {
            btnActivate.Text = "Activating...";
            Cursor = Cursors.WaitCursor;
            btnActivate.Enabled = false;
        }

        private void HideBusy()
        {

            btnActivate.Enabled = true;
            Cursor = Cursors.Default;
            btnActivate.Text = "Activate";
        }

        #endregion

        #region Events

        private void Activate_Load(object sender, EventArgs e)
        {
            txtHardwareID.Text = TrialMaker.HardwareID;
        }

        private void BtnActivate_Click(object sender, EventArgs e)
        {
            ShowBusy();

            var lic = TrialMaker.Instance.Activate(txtLicense.Text);

            if (lic.Status == LicenseStatus.Invalid)
            {
                HideBusy();

                MessageBox.Show("The license is invalid.");
                
                // [NB] You can display the list of errors to clients.
                // string errors = string.Join(",\n", TrialMaker.Instance.ValidationErrors);
            }
            else
            {
                HideBusy();

                MessageBox.Show(
                    $"Thank you {lic.Client} for purchasing {lic.Product}!\n" +
                    $"Here is your license information:\n\n" +
                    $" * Product: {lic.Product}\n" +
                    $" * License Type: {lic.Type}\n" +
                    $" * License Key: {lic.LicenseKey}\n" +
                    $" * License Status: {lic.Status}\n" +
                    $" * Activation Date: {lic.ActivationDate}\n",
                    $"Premium License Activated");

                MainForm.Instance.UpdateTitles();

                Close();
            }
        }

        private void BtnCopyCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHardwareID.Text);
        }

        #endregion
    }
}