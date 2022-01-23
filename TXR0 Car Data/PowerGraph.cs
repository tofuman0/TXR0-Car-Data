using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TXR0_Car_Data
{
    public partial class PowerGraph : Form
    {
        public DataGridViewRow datacells;
        private bool Dragging = false;
        public PowerGraph(DataGridViewRow cells)
        {
            InitializeComponent();
            datacells = cells;
            PopulateFields();
            RefreshGraph();
        }
        private void InitGraph()
        {
            chartPower.Series[0].Points.Clear();
        }
        private void PopulateFields()
        {
            tbTorque.Text = datacells.Cells["Torque"].Value.ToString();
            tbBhpRpm.Text = datacells.Cells["BHP RPM 1"].Value.ToString();
            tbTorqueRpm1.Text = datacells.Cells["Torque RPM 1"].Value.ToString();
            tbTorqueRpm2.Text = datacells.Cells["Torque RPM 2"].Value.ToString();
            tbRpmIdle.Text = datacells.Cells["RPM Idle"].Value.ToString();
            tbRpmLimit.Text = datacells.Cells["RPM Limit"].Value.ToString();

            this.Text = "Power Graph - " + datacells.Cells["Name"].Value.ToString();
        }

        private void RefreshGraph()
        {
            chartPower.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            if (datacells.Cells.Count > 0)
            {
                Int32 rpmPowerLimit = Math.Min(129, (Convert.ToInt32(datacells.Cells["RPM Limit"].Value) / 100) + 1);
                for (Int32 i = 0; i < rpmPowerLimit; i++)
                {
                    chartPower.Series[0].Points.AddXY((i) * 100, Convert.ToDouble(datacells.Cells["Power Graph Value " + (i + 1)].Value.ToString()));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void tbTorque_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            datacells.Cells["Torque"].Value = Convert.ToSingle(tbValue.Text);
            chartPower.ChartAreas[0].AxisY.Maximum = Math.Round(Convert.ToSingle(tbValue.Text) + 1.0);
        }

        private void tbBhpRpm_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            UInt16 value = Convert.ToUInt16(tbValue.Text);
            if (value > 13000)
            {
                value = 13000;
                tbValue.Text = value.ToString();
            }
            datacells.Cells["BHP RPM 1"].Value = value;
        }

        private void tbTorqueRpm1_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            UInt16 value = Convert.ToUInt16(tbValue.Text);
            if (value > 13000)
            {
                value = 13000;
                tbValue.Text = value.ToString();
            }
            datacells.Cells["Torque RPM 1"].Value = value;
        }

        private void tbTorqueRpm2_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            UInt16 value = Convert.ToUInt16(tbValue.Text);
            if (value > 13000)
            {
                value = 13000;
                tbValue.Text = value.ToString();
            }
            datacells.Cells["Torque RPM 2"].Value = value;
        }

        private void tbRpmLimit_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            UInt16 value = Convert.ToUInt16(tbValue.Text);
            if (value > 13000)
            {
                value = 13000;
                tbValue.Text = value.ToString();
            }
            datacells.Cells["RPM Limit"].Value = value;
            chartPower.ChartAreas[0].AxisX.Maximum = value;
        }

        private void tbRpmIdle_TextChanged(object sender, EventArgs e)
        {
            TextBox tbValue = (TextBox)sender;
            UInt16 value = Convert.ToUInt16(tbValue.Text);
            if (value > 13000)
            {
                value = 13000;
                tbValue.Text = value.ToString();
            }
            datacells.Cells["RPM Idle"].Value = value;
        }

        private void chartPower_MouseDown(object sender, MouseEventArgs e)
        {
            var results = chartPower.HitTest(e.X, e.Y, false, System.Windows.Forms.DataVisualization.Charting.ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.PlottingArea)
                {
                    Dragging = true;
                    return;
                }
            }
            Dragging = false;
        }
        private void chartPower_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void chartPower_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                var results = chartPower.HitTest(e.X, e.Y, false, System.Windows.Forms.DataVisualization.Charting.ChartElementType.PlottingArea);
                foreach (var result in results)
                {
                    if (result.ChartElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.PlottingArea)
                    {
                        Int32 rpmValue = (Convert.ToInt32(result.ChartArea.AxisX.PixelPositionToValue(e.X)) / 100) + 2;
                        if (rpmValue < 1)
                            rpmValue = 1;
                        else if (rpmValue > 130)
                            rpmValue = 130;
                        Single torqueValue = Convert.ToSingle(result.ChartArea.AxisY.PixelPositionToValue(e.Y));
                        datacells.Cells["Power Graph Value " + rpmValue].Value = torqueValue;
                        InitGraph();
                        RefreshGraph();
                    }
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
