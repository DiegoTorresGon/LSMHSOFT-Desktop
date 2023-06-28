using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSMHSOFT___Desktop
{
    public partial class LSMHSOFT : Form
    {
        public LSMHSOFT()
        {
            InitializeComponent();
        }

        private void ModelFileButton_Click(object sender, EventArgs e)
        {
            if (OpenModelFile.ShowDialog() == DialogResult.OK)
            {
                DataOutput.Text = OpenModelFile.FileName;
            }
        }

        private void DataOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
