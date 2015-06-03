using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MergeHelper
{
    public partial class MainForm : Form
    {
        #region Properties
        private string OriginalText { get; set; }
        #endregion

        #region Constructor(s)
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void copyTextLineIntoStr(string line, ref string content)
        {
            content += line + Environment.NewLine;
        }

        private string searchTextInContent(string textToSearch, string content)
        {
            string output = String.Empty;

            var lines = new List<string>(content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            var result = lines.Where(i => i.ToLower().Contains(textToSearch.ToLower())).ToList();
            result.ForEach(i => copyTextLineIntoStr(i, ref output));

            return output;
        }
        #endregion

        #region Events
        private void txtToSearch_TextChanged(object sender, EventArgs e)
        {
            txtContent.Text = searchTextInContent(txtToSearch.Text, OriginalText);
        }

        private void txtToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (String.IsNullOrEmpty((sender as TextBox).Text))
            {
                OriginalText = txtContent.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtToSearch.Clear();
        }
        #endregion
    }
}
