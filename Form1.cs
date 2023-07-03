using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSMHSOFT___Desktop
{
    public partial class LSMHSOFT : Form
    {
        private delegate void SafeCallOutput(string text);
        public LSMHSOFT()
        {
            InitializeComponent();

            args.m = MTextBox.Text;
            args.p = PTextBox.Text;
            args.c = CTextBox.Text;
            args.v = VTextBox.Text;
            args.r = RTextBox.Text;
            args.i = ITextBox.Text;
            args.dataOutputBuffer = "";
            args.nLinesBuffer = 0;
            args.outputStarting = false;
        }

        private void ModelFileButton_Click(object sender, EventArgs e)
        {
            if (OpenModelFile.ShowDialog() == DialogResult.OK)
            {
                ModelFileLabel.Text = OpenModelFile.FileName;
                args.model = OpenModelFile.FileName;
            }
        }

        private void PTextBox_TextChanged(object sender, EventArgs e)
        {
            args.p = PTextBox.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OpenDisturbFile.ShowDialog() == DialogResult.OK)
            {
                DisturbFileLabel.Text = OpenDisturbFile.FileName;
                args.disturb = OpenDisturbFile.FileName;
            }
        }

        private void ElevFileButton(object sender, EventArgs e)
        {
            if (OpenElevFile.ShowDialog() == DialogResult.OK)
            {
                ElevFileLabel.Text = OpenElevFile.FileName;
                args.elev = OpenElevFile.FileName;
            }
        }

        private void DensityFileButton(object sender, EventArgs e)
        {
            if (OpenDensityFile.ShowDialog() == DialogResult.OK)
            {
                DensityFileLabel.Text = OpenDensityFile.FileName;
                args.density = OpenDensityFile.FileName;
            }
        }

        private void MTextBox_TextChanged(object sender, EventArgs e)
        {
            args.m = MTextBox.Text;
        }

        private void CTextBox_TextChanged(object sender, EventArgs e)
        {
            args.c = CTextBox.Text;
        }

        private void ITextBox_TextChanged(object sender, EventArgs e)
        {
            args.i = ITextBox.Text;
        }

        private void RTextBox_TextChanged(object sender, EventArgs e)
        {
            args.r = RTextBox.Text;
        }

        private void VTextBox_TextChanged(object sender, EventArgs e)
        {
            args.v = VTextBox.Text;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            
            PopUp modelError = new PopUp();
            if (args.model == null)
            {
                modelError.ErrorLabel.Text = "You must select a model file";
                modelError.ShowDialog();
            } else if (args.disturb == null)
            {
                modelError.ErrorLabel.Text = "You must select a disturbances file";
                modelError.ShowDialog();
            } else if (args.elev == null)
            {
                modelError.ErrorLabel.Text = "You must select an elevation file";
                modelError.ShowDialog();
            } else
            {
                string startArguments;
                if (args.density == null)
                {
                    startArguments = "-G\"" + args.model + "\" -D\"" + args.disturb
                        + "\" -E\"" + args.elev + "\" -M\"" + args.m + "\" -P\"" + args.p + "\" -C\""
                        + args.c + "\" -V\"" + args.v + "\" -R\"" + args.r + "\" -I\"" + args.i + "\"";
                } else
                {
                    startArguments = "-G\"" + args.model + "\" -D\"" + args.disturb
                        + "\" -E\"" + args.elev + "\" -T\"" + args.density + "\" -M\"" + args.m
                        + "\" -P\"" + args.p + "\" -C\"" + args.c + "\" -V\"" + args.v + "\" -R\""
                        + args.r + "\" -I\"" + args.i + "\"";
                }

                DataOutput.Text = "Starting calculation with arguments:\r\n" +
                    startArguments + "\r\n\r\nWaiting for output...";
                args.outputStarting = true;

                backgroundWorker1.RunWorkerAsync();
            }

            modelError.Dispose();
        }

        private void CalculationOutputHandler(object sendingProcess,
            DataReceivedEventArgs outline)
        {
            if (!String.IsNullOrEmpty(outline.Data) )
            {
                if(args.nLinesBuffer < 200)
                {
                    args.dataOutputBuffer += outline.Data + Environment.NewLine;
                    ++args.nLinesBuffer;
                } else
                {
                    backgroundWorker1.ReportProgress(50, args.dataOutputBuffer 
                        + outline.Data + Environment.NewLine);
                    args.dataOutputBuffer = "";
                    args.nLinesBuffer = 0;
                }
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Process calculation = new Process();

            calculation.StartInfo.FileName = ".\\LSMHSOFT\\LSMHSOFT.exe";
            calculation.StartInfo.UseShellExecute = false;
            calculation.StartInfo.RedirectStandardOutput = true;
            calculation.OutputDataReceived += CalculationOutputHandler;

            if (args.density == null)
            {
                calculation.StartInfo.Arguments = "-G\"" + args.model + "\" -D\"" + args.disturb
                    + "\" -E\"" + args.elev + "\" -M\"" + args.m + "\" -P\"" + args.p + "\" -C\"" 
                    + args.c + "\" -V\"" + args.v + "\" -R\"" + args.r + "\" -I\"" + args.i + "\"";
            } else
            {
                calculation.StartInfo.Arguments = "-G\"" + args.model + "\" -D\"" + args.disturb
                    + "\" -E\"" + args.elev + "\" -T\"" + args.density + "\" -M\"" + args.m 
                    + "\" -P\"" + args.p + "\" -C\"" + args.c + "\" -V\"" + args.v + "\" -R\"" 
                    + args.r + "\" -I\"" + args.i + "\"";
            }

            calculation.Start();
            calculation.BeginOutputReadLine();
            calculation.WaitForExit();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataOutput.AppendText(e.UserState.ToString());
        }
        private void backgroundWorker1_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (args.outputStarting)
            {
                DataOutput.Text = "";
                args.outputStarting = false;
            }
            if (args.nLinesBuffer > 0)
            {
                DataOutput.AppendText(args.dataOutputBuffer);
                args.dataOutputBuffer = "";
                args.nLinesBuffer = 0;
            }
            //display button to see if the users wants to save file.
            SaveButton.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveOutput.ShowDialog();
            if (SaveOutput.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)SaveOutput.OpenFile();

                byte[] data = new UTF8Encoding(true).GetBytes(DataOutput.Text);
                fs.Write(data, 0, data.Length);

                fs.Close();
            }
        }
    }
}
