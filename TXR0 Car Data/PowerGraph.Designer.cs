
namespace TXR0_Car_Data
{
    partial class PowerGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTorque = new System.Windows.Forms.Label();
            this.lblBhpRpm = new System.Windows.Forms.Label();
            this.tbTorque = new System.Windows.Forms.TextBox();
            this.tbBhpRpm = new System.Windows.Forms.TextBox();
            this.chartPower = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbTorqueRpm1 = new System.Windows.Forms.TextBox();
            this.lblTorqueRpm1 = new System.Windows.Forms.Label();
            this.lblTorqueRpm2 = new System.Windows.Forms.Label();
            this.lblRpmLimit = new System.Windows.Forms.Label();
            this.lblRpmIdle = new System.Windows.Forms.Label();
            this.tbTorqueRpm2 = new System.Windows.Forms.TextBox();
            this.tbRpmLimit = new System.Windows.Forms.TextBox();
            this.tbRpmIdle = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chartPower)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(6, 583);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 28);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(90, 583);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTorque
            // 
            this.lblTorque.AutoSize = true;
            this.lblTorque.Location = new System.Drawing.Point(8, 6);
            this.lblTorque.Name = "lblTorque";
            this.lblTorque.Size = new System.Drawing.Size(54, 17);
            this.lblTorque.TabIndex = 2;
            this.lblTorque.Text = "Torque";
            // 
            // lblBhpRpm
            // 
            this.lblBhpRpm.AutoSize = true;
            this.lblBhpRpm.Location = new System.Drawing.Point(6, 59);
            this.lblBhpRpm.Name = "lblBhpRpm";
            this.lblBhpRpm.Size = new System.Drawing.Size(82, 17);
            this.lblBhpRpm.TabIndex = 3;
            this.lblBhpRpm.Text = "BHP RPM 1";
            // 
            // tbTorque
            // 
            this.tbTorque.Location = new System.Drawing.Point(8, 26);
            this.tbTorque.Name = "tbTorque";
            this.tbTorque.Size = new System.Drawing.Size(156, 22);
            this.tbTorque.TabIndex = 4;
            this.tbTorque.TextChanged += new System.EventHandler(this.tbTorque_TextChanged);
            // 
            // tbBhpRpm
            // 
            this.tbBhpRpm.Location = new System.Drawing.Point(8, 79);
            this.tbBhpRpm.Name = "tbBhpRpm";
            this.tbBhpRpm.Size = new System.Drawing.Size(156, 22);
            this.tbBhpRpm.TabIndex = 5;
            this.tbBhpRpm.TextChanged += new System.EventHandler(this.tbBhpRpm_TextChanged);
            // 
            // chartPower
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Angle = -45;
            chartArea3.AxisX.LabelStyle.Interval = 500D;
            chartArea3.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisX.Title = "RPM";
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.Title = "Torque";
            chartArea3.Name = "ChartArea1";
            this.chartPower.ChartAreas.Add(chartArea3);
            this.chartPower.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chartPower.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartPower.Legends.Add(legend3);
            this.chartPower.Location = new System.Drawing.Point(3, 3);
            this.chartPower.Name = "chartPower";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartPower.Series.Add(series3);
            this.chartPower.Size = new System.Drawing.Size(1173, 620);
            this.chartPower.TabIndex = 6;
            this.chartPower.Text = "Power Graph";
            this.chartPower.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartPower_MouseDown);
            this.chartPower.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartPower_MouseMove);
            this.chartPower.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartPower_MouseUp);
            // 
            // tbTorqueRpm1
            // 
            this.tbTorqueRpm1.Location = new System.Drawing.Point(8, 132);
            this.tbTorqueRpm1.Name = "tbTorqueRpm1";
            this.tbTorqueRpm1.Size = new System.Drawing.Size(156, 22);
            this.tbTorqueRpm1.TabIndex = 7;
            this.tbTorqueRpm1.TextChanged += new System.EventHandler(this.tbTorqueRpm1_TextChanged);
            // 
            // lblTorqueRpm1
            // 
            this.lblTorqueRpm1.AutoSize = true;
            this.lblTorqueRpm1.Location = new System.Drawing.Point(6, 112);
            this.lblTorqueRpm1.Name = "lblTorqueRpm1";
            this.lblTorqueRpm1.Size = new System.Drawing.Size(100, 17);
            this.lblTorqueRpm1.TabIndex = 8;
            this.lblTorqueRpm1.Text = "Torque RPM 1";
            // 
            // lblTorqueRpm2
            // 
            this.lblTorqueRpm2.AutoSize = true;
            this.lblTorqueRpm2.Location = new System.Drawing.Point(6, 165);
            this.lblTorqueRpm2.Name = "lblTorqueRpm2";
            this.lblTorqueRpm2.Size = new System.Drawing.Size(100, 17);
            this.lblTorqueRpm2.TabIndex = 9;
            this.lblTorqueRpm2.Text = "Torque RPM 2";
            // 
            // lblRpmLimit
            // 
            this.lblRpmLimit.AutoSize = true;
            this.lblRpmLimit.Location = new System.Drawing.Point(6, 218);
            this.lblRpmLimit.Name = "lblRpmLimit";
            this.lblRpmLimit.Size = new System.Drawing.Size(71, 17);
            this.lblRpmLimit.TabIndex = 10;
            this.lblRpmLimit.Text = "RPM Limit";
            // 
            // lblRpmIdle
            // 
            this.lblRpmIdle.AutoSize = true;
            this.lblRpmIdle.Location = new System.Drawing.Point(8, 271);
            this.lblRpmIdle.Name = "lblRpmIdle";
            this.lblRpmIdle.Size = new System.Drawing.Size(64, 17);
            this.lblRpmIdle.TabIndex = 11;
            this.lblRpmIdle.Text = "RPM Idle";
            // 
            // tbTorqueRpm2
            // 
            this.tbTorqueRpm2.Location = new System.Drawing.Point(8, 185);
            this.tbTorqueRpm2.Name = "tbTorqueRpm2";
            this.tbTorqueRpm2.Size = new System.Drawing.Size(156, 22);
            this.tbTorqueRpm2.TabIndex = 12;
            this.tbTorqueRpm2.TextChanged += new System.EventHandler(this.tbTorqueRpm2_TextChanged);
            // 
            // tbRpmLimit
            // 
            this.tbRpmLimit.Location = new System.Drawing.Point(8, 238);
            this.tbRpmLimit.Name = "tbRpmLimit";
            this.tbRpmLimit.Size = new System.Drawing.Size(156, 22);
            this.tbRpmLimit.TabIndex = 13;
            this.tbRpmLimit.TextChanged += new System.EventHandler(this.tbRpmLimit_TextChanged);
            // 
            // tbRpmIdle
            // 
            this.tbRpmIdle.Location = new System.Drawing.Point(8, 291);
            this.tbRpmIdle.Name = "tbRpmIdle";
            this.tbRpmIdle.Size = new System.Drawing.Size(156, 22);
            this.tbRpmIdle.TabIndex = 14;
            this.tbRpmIdle.TextChanged += new System.EventHandler(this.tbRpmIdle_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.Controls.Add(this.chartPower, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1359, 626);
            this.tableLayoutPanel1.TabIndex = 15;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTorqueRpm2);
            this.panel1.Controls.Add(this.lblRpmIdle);
            this.panel1.Controls.Add(this.tbTorque);
            this.panel1.Controls.Add(this.lblBhpRpm);
            this.panel1.Controls.Add(this.tbBhpRpm);
            this.panel1.Controls.Add(this.tbRpmIdle);
            this.panel1.Controls.Add(this.tbRpmLimit);
            this.panel1.Controls.Add(this.tbTorqueRpm1);
            this.panel1.Controls.Add(this.tbTorqueRpm2);
            this.panel1.Controls.Add(this.lblTorqueRpm1);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblRpmLimit);
            this.panel1.Controls.Add(this.lblTorque);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1182, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 620);
            this.panel1.TabIndex = 15;
            // 
            // PowerGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 626);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PowerGraph";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Power Graph";
            ((System.ComponentModel.ISupportInitialize)(this.chartPower)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTorque;
        private System.Windows.Forms.Label lblBhpRpm;
        private System.Windows.Forms.TextBox tbTorque;
        private System.Windows.Forms.TextBox tbBhpRpm;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPower;
        private System.Windows.Forms.TextBox tbTorqueRpm1;
        private System.Windows.Forms.Label lblTorqueRpm1;
        private System.Windows.Forms.Label lblTorqueRpm2;
        private System.Windows.Forms.Label lblRpmLimit;
        private System.Windows.Forms.Label lblRpmIdle;
        private System.Windows.Forms.TextBox tbTorqueRpm2;
        private System.Windows.Forms.TextBox tbRpmLimit;
        private System.Windows.Forms.TextBox tbRpmIdle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}