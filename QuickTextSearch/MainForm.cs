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
            if (String.IsNullOrEmpty(txtToSearch.Text))
            {
                return OriginalText;
            }

            string output = String.Empty;
            var itemsToSearchFor = textToSearch.Trim().Split(' ').ToList();
            var lines = new List<string>(OriginalText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            var occurrences = new List<string>();
            var itemsFound = new List<string>();
            var dict = new Dictionary<int, string>();

            foreach(string item in itemsToSearchFor)
            {
                var tmpOccurrences = new List<string>();
                foreach(string line in lines)
                {
                    if(line.ToLower().Contains(item.ToLower()))
                    {
                        tmpOccurrences.Add(line);
                        itemsFound.Add(item);
                    }
                }
                occurrences.AddRange(tmpOccurrences);
            }

            var itemsNotFound = itemsToSearchFor.Except(itemsFound).ToList();
            txtItemsNotFound.Clear();
            itemsNotFound.ForEach(item => txtItemsNotFound.AppendText(item + Environment.NewLine));

            occurrences = occurrences.Distinct().ToList();
            occurrences.ForEach(i => dict.Add(lines.IndexOf(i), i));

            var keys = dict.Keys.ToList();
            keys.Sort();
            keys.ForEach(i => copyTextLineIntoStr(dict[i], ref output));

            return output;
        }
        #endregion

        #region Events
        private void txtToSearch_TextChanged(object sender, EventArgs e)
        {
            txtContent.Text = searchTextInContent(txtToSearch.Text, OriginalText);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtToSearch.Clear();
        }
        #endregion

        private void txtContent_KeyUp(object sender, KeyEventArgs e)
        {
            OriginalText = txtContent.Text;
        }
    }
}
