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
                ModelFileLabel.Text = OpenModelFile.FileName;
                DataOutput.Text = OpenModelFile.FileName;
            }
        }

        private void DataOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void IntDataCapVal_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OpenDisturbFile.ShowDialog() == DialogResult.OK)
            {
                DisturbFileLabel.Text = OpenDisturbFile.FileName;
            }
        }

        private void ElevFileButton(object sender, EventArgs e)
        {
            if (OpenElevFile.ShowDialog() == DialogResult.OK)
            {
                ElevFileLabel.Text = OpenElevFile.FileName;
            }
        }

        private void DensityFileButton(object sender, EventArgs e)
        {
            if (OpenDensityFile.ShowDialog() == DialogResult.OK)
            {
                DensityFileLabel.Text = OpenDensityFile.FileName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
        }

        private void VLabel_Click(object sender, EventArgs e)
        {

        }

        private void LSMHSOFT_Load(object sender, EventArgs e)
        {

        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void ModelLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void MLabel_Click(object sender, EventArgs e)
        {

        }

        private void PLabel_Click(object sender, EventArgs e)
        {

        }

        private void CLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ILabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void RLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunButton_Click(object sender, EventArgs e)
        {

        }
    }
}
