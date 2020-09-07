#region Copyright

/*
 * The MIT License

 * Copyright (c) 2020, Willy Kimura.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 */

#endregion


using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.TrialMakerNS;
using WK.Libraries.TrialMakerNS.Models;

namespace TrialMakerDemo.CSharp.Views
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            _instance = this;
        }

        #endregion

        #region Fields

        private static MainForm _instance;
        internal static TrialMaker tm = new TrialMaker();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently running instance of <see cref="MainForm"/>.
        /// </summary>
        public static MainForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainForm();

                return _instance;
            }
        }

        /// <summary>
        /// Stores the free-trial evaluation message at the window title.
        /// </summary>
        internal string EvaluationMessage { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Provides license validation logic for the application.
        /// </summary>
        private void ValidateLicense()
        {
            tm.ProductInfo = new ProductInfo()
            {
                ID = "#123#",
                Name = "My App",
                Owner = "Willy Kimura",
                TotalDays = 30,
                PurchasePage = "https://myapp.com/purchase"
            };

            tm.TrackTime = true;
            tm.TrackUsage = true;
            tm.TimeTracking += TimeTracking;
            
            if (!tm.LicenseExists)
            {
                new StartFreeTrial().ShowDialog();
            }

            var lic = tm.Validate();

            if (lic.Status == LicenseStatus.Active)
            {
                if (lic.FirstTime)
                {
                    if (lic.Type == LicenseTypes.FreeTrial)
                    {
                        MessageBox.Show(
                            $"Your license information:\n\n" +
                            $" * Product: {lic.Product}\n" +
                            $" * License Key: {lic.LicenseKey}\n" +
                            $" * License Status: {lic.Status}\n" +
                            $" * Activation Date: {lic.ActivationDate}\n" +
                            $" * Expiry Date: {lic.ExpiryDate}\n" +
                            $" * Total Days: {lic.TotalDays}\n" +
                            $" * Remaining Days: {lic.RemainingDays}",
                            $"{lic.TotalDays}-day Free Trial Activated");
                    }
                }

                UpdateTitles(lic);
            }
            else if (lic.Status == LicenseStatus.Expired)
            {
                MessageBox.Show(
                    $"Your {lic.TotalDays}-day free-trial has expired. " +
                    $"Please purchase a premium license to continue.");
                
                Application.Exit();
            }
            else if (lic.Status == LicenseStatus.Invalid)
            {
                string errors = string.Join(",\n", tm.ValidationErrors);
                string messageTitle = "The following errors were found upon validation:\n\n";
                string messageFooter = "Please ensure they are resolved before continuing";

                if (tm.ValidationErrors.Count == 1)
                {
                    messageTitle = "The following error was found upon validation:\n";
                    messageFooter = "Please ensure it is resolved before continuing";
                }
                
                MessageBox.Show(
                    this,
                    $"{messageTitle}{errors}\n\n{messageFooter}",
                    "License Invalid");
                
                Application.Exit();
            }
        }

        /// <summary>
        /// Updates the system titles based on the customer license information.
        /// </summary>
        public void UpdateTitles()
        {
            var license = TrialMaker.RetrievedLicense;

            if (license.Type == LicenseTypes.Premium)
            {
                activatePremiumToolStripMenuItem.Visible = false;
                EvaluationMessage = $"Premium";

                Text = $"{license.Product} ({EvaluationMessage})";
                lblLicenseMessage.Text = $"Licensed to {license.Client}";
                lblLicenseMessage.ForeColor = Color.ForestGreen;
            }
            else
            {
                DateTime expiryDate = license.ExpiryDate;
                EvaluationMessage = $"{license.TotalDays}-day Evaluation";

                string datePartA = expiryDate.ToString("dddd dd") + GetDaySuffix(expiryDate.Day) + " ";
                string datePartB = expiryDate.ToString("MMMM, yyyy");
                string ExpirationMessage = datePartA + datePartB;

                Text = $"{license.Product} ({EvaluationMessage})";
                lblLicenseMessage.Text = $"Evaluation period expires on {ExpirationMessage}";
            }
        }

        /// <summary>
        /// Updates the system titles based on the customer license information.
        /// </summary>
        /// <param name="license">
        /// Provide the customer license to capture details from.
        /// </param>
        public void UpdateTitles(License license)
        {
            if (license.Type == LicenseTypes.Premium)
            {
                activatePremiumToolStripMenuItem.Visible = false;
                EvaluationMessage = $"Premium";

                Text = $"{license.Product} ({EvaluationMessage})";
                lblLicenseMessage.Text = $"Licensed to {license.Client}";
                lblLicenseMessage.ForeColor = Color.ForestGreen;
            }
            else
            {
                DateTime expiryDate = license.ExpiryDate;
                EvaluationMessage = $"{license.TotalDays}-day Evaluation";

                string datePartA = expiryDate.ToString("dddd dd") + GetDaySuffix(expiryDate.Day) + " ";
                string datePartB = expiryDate.ToString("MMMM, yyyy");
                string ExpirationMessage = datePartA + datePartB;

                Text = $"{license.Product} ({EvaluationMessage})";
                lblLicenseMessage.Text = $"Evaluation period expires on {ExpirationMessage}";
            }
        }

        /// <summary>
        /// Gets the suffix of any day, e.g. 21st.
        /// </summary>
        /// <param name="day">
        /// The day in Integer format.
        /// </param>
        public string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        #endregion

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            ValidateLicense();
        }

        private void TimeTracking(object sender, TrialMaker.TimeTrackingEventArgs e)
        {
            if (Focused)
            {
                e.Continue();

                lblTimerStatus.Text = e.TimeSpan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                e.Pause();
            }
        }
    
        private void LicenseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lic = TrialMaker.RetrievedLicense;

            if (lic.Type == LicenseTypes.FreeTrial)
            {
                MessageBox.Show(
                    $"Your license information:\n\n" +
                    $" * Product: {lic.Product}\n" +
                    $" * License Status: {lic.Status}\n" +
                    $" * License Key: {lic.LicenseKey}\n" +
                    $" * Activation Date: {lic.ActivationDate}\n" +
                    $" * Expiry Date: {lic.ExpiryDate}\n" +
                    $" * Total Days: {lic.TotalDays}\n" +
                    $" * Remaining Days: {lic.RemainingDays}",
                    $"License Information");
            }
            else if (lic.Type == LicenseTypes.Premium)
            {
                MessageBox.Show(
                    $"Your license information:\n\n" +
                    $" * Product: {lic.Product}\n" +
                    $" * License Type: {lic.Type}\n" +
                    $" * License Key: {lic.LicenseKey}\n" +
                    $" * License Status: {lic.Status}\n" +
                    $" * Activation Date: {lic.ActivationDate}\n",
                    $"License Information");
            }
        }

        private void ActivatePremiumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Activate().ShowDialog();
        }

        #endregion
    }
}