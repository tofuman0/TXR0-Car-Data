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
    public partial class MainForm : Form
    {
        public static ParamDataManager paramDataManager = new ParamDataManager();
        public delegate DataSet fileProcess(String TableName, String LanguageFilename, String DataFileName, List<ParamDataManager.FileStructure> FileStructure);
        #region Structures
        struct FileList
        {
            public FileList(String _name, String _dataFilename = null, String _languageFileName = null, fileProcess _function = null, List<ParamDataManager.FileStructure> _structure = null) { name = _name; dataFileName = _dataFilename; languageFileName = _languageFileName; function = _function; structure = _structure; }
            public String name;
            public String dataFileName;
            public String languageFileName;
            public fileProcess function;
            public List<ParamDataManager.FileStructure> structure;
        };
        #endregion Structures
        #region Variables
        private DataSet dsParamData = null;
        private String filename = null;
        private bool unsaved = false;
        private Int32 rowToCopy = -1;
        #endregion Variables

        public MainForm()
        {
            InitializeComponent();
            SetDoubleBuffered(advancedDataGridView);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 res = OpenFileDialog();
            if (res == 0)
            {
                LoadDataTable(dsParamData.Tables["Car Data"]);
            }
            else if (res > 0)
                MessageBox.Show(this, "File is an unsupported format", "Failed to load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void openFromELFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 res = OpenFileDialog(true); // Load from ELF File
            if (res == 0)
            {
                LoadDataTable(dsParamData.Tables["Car Data"]);
            }
            else if (res > 0)
                MessageBox.Show(this, "File is an unsupported format", "Failed to load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 res = SaveFileDialog();
            if (res == 0)
            {
                Status.Text = filename;
                this.Text = "TXR0 Car Data - " + filename;
            }
            else if (res > 0)
                MessageBox.Show(this, "Error saving file", "Failed to save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void saveToELFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog(true, true) == 0) // Load ELF for save
            {
                Int32 res = SaveFileDialog(true);
                if (res == 0)
                {
                    Status.Text = filename;
                    this.Text = "TXR0 Car Data - " + filename;
                }
                else if (res > 0)
                    MessageBox.Show(this, "File is an unsupported version", "Failed to save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(this, "Unable to load ELF File", "Error saving file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsaved == false)
            {
                this.Close();
            }
            else
            {
                DialogResult res = MessageBox.Show(this, "Your work is unsaved. Would you like to save now?", "Unsaved work", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    if (SaveFileDialog() == 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Unable to save", "Error saving file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if(res==DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        private Int32 SaveFileDialog(bool toelffile = false)
        {
            if(toelffile == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Select TXR0 ELF file save",
                };
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (paramDataManager.SaveELF(saveFileDialog.FileName) == true)
                    {
                        filename = saveFileDialog.FileName;
                        return 0;
                    }
                    else
                        return 1;
                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Select TXR0 Car Data file",
                };
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (paramDataManager.SaveCarData(saveFileDialog.FileName) == true)
                        return 0;
                    else
                        return 1;
                }
            }
            return -1;
        }

        private Int32 OpenFileDialog(bool loadelf = false, bool forsave = false)
        {
            if (loadelf == true)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Select TXR0 ELF file for load",
                };
                if (forsave)
                {
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        if (paramDataManager.LoadELFData(openFileDialog.FileName) == true)
                            return 0;
                        else
                            return 1;
                    }
                }
                else
                {
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        dsParamData = paramDataManager.OpenELFFile("Car Data", openFileDialog.FileName, paramDataManager.fsCarData);
                        if (dsParamData == null)
                            return 1;
                        else
                        {
                            filename = openFileDialog.FileName;
                            return 0;
                        }
                    }
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Select TXR0 Car Data file",
                };
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    dsParamData = paramDataManager.OpenFile("Car Data", openFileDialog.FileName, paramDataManager.fsCarData);
                    if (dsParamData == null)
                        return 1;
                    else
                    {
                        filename = openFileDialog.FileName;
                        return 0;
                    }
                }
            }
            return -1;
        }

        private void SetDataSource(String tablename, DataTable dtData, String filter)
        {
            ClearDataGridView();
            advancedDataGridView.DataSource = dtData;
            if(filter != null)
            {
                (advancedDataGridView.DataSource as DataTable).DefaultView.RowFilter = String.Format(filter);
            }
            FormatDataGridView();
            this.Text = "TXR0 Car Data - " + filename;
            Status.Text = filename;
        }

        private void ClearDataGridView()
        {
            while(advancedDataGridView.Columns.Count > 0)
            {
                advancedDataGridView.Columns.RemoveAt(0);
            }
        }

        private void LoadDataTable(DataTable dtData, String filter = null)
        {
            SetDataSource(dtData.TableName, dtData, filter);
            rowToCopy = -1;
            pushToPCSX2ToolStripMenuItem.Enabled = true;
        }

        private void FormatDataGridView()
        {
            for (Int32 i = 0; i < advancedDataGridView.Columns.Count; i++)
            {
                advancedDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (advancedDataGridView.Columns[i].Name == "Car Number" || advancedDataGridView.Columns[i].Name.StartsWith("Power Graph Value "))
                {
                    advancedDataGridView.Columns[i].Visible = false;
                }
                else if (advancedDataGridView.Columns[i].Name == "Power Graph")
                {
                    Int32 index = advancedDataGridView.Columns[i].Index;
                    DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                    btnCol.Name = "Power Graph";

                    advancedDataGridView.Columns.RemoveAt(index);
                    advancedDataGridView.Columns.Insert(index, btnCol);

                    foreach(DataGridViewRow row in advancedDataGridView.Rows)
                    {
                        row.Cells["Power Graph"].Value = "View";
                    }
                }

                advancedDataGridView.Columns[i].Width = TextRenderer.MeasureText(advancedDataGridView.Columns[i].HeaderText, advancedDataGridView.Font).Width + 24;
                if (advancedDataGridView.Columns[i].Width < 80)
                    advancedDataGridView.Columns[i].Width = 80;
            }

            advancedDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            advancedDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.False;            
        }

        private void advancedDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (advancedDataGridView.DataSource != null)
            {
                //FormatDataGridView();
                //this.Text = "TXR0 Car Data - " + filename;
            }
        }

        private void advancedDataGridView_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            (advancedDataGridView.DataSource as DataTable).DefaultView.RowFilter = e.FilterString;
            FormatDataGridView();
        }

        private void advancedDataGridView_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            (advancedDataGridView.DataSource as DataTable).DefaultView.Sort = e.SortString;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void advancedDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = grid.Rows[e.RowIndex].Cells["Car Number"].Value.ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void advancedDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0 && senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && senderGrid.Columns[e.ColumnIndex].Name == "Power Graph")
            {
                PowerGraph powerGraph = new PowerGraph(senderGrid.Rows[e.RowIndex]);
                var res = powerGraph.ShowDialog();
                if (res == DialogResult.OK)
                {
                    for(Int32 i = 0; i < powerGraph.datacells.Cells.Count; i++)
                    {
                        senderGrid.Rows[e.ColumnIndex].Cells[i].Value = powerGraph.datacells.Cells[i].Value;
                    }
                }
            }
        }

        private void advancedDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = advancedDataGridView.HitTest(e.Location.X, e.Location.Y);
            if (cell.Type == DataGridViewHitTestType.Cell && advancedDataGridView.DataSource != null && e.Button == MouseButtons.Right)
            {
                if (rowToCopy != -1)
                {
                    contextMenuActions.Items["toolStripPaste"].Enabled = true;
                }
                else
                {
                    contextMenuActions.Items["toolStripPaste"].Enabled = false;
                }
                advancedDataGridView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Selected = true;
                contextMenuActions.Show(Cursor.Position);
            }
        }

        private void toolStripCopy_Click(object sender, EventArgs e)
        {
            if(advancedDataGridView.SelectedCells.Count > 0)
                rowToCopy = advancedDataGridView.SelectedCells[0].RowIndex;
        }

        private void toolStripPaste_Click(object sender, EventArgs e)
        {
            if(rowToCopy != -1 && advancedDataGridView.SelectedCells.Count > 0)
            {
                Int32 rowToCopyTo = advancedDataGridView.SelectedCells[0].RowIndex;
                for(Int32 i = 0; i < 130; i++) // Power Graph Value Count is 130
                {
                    advancedDataGridView.Rows[rowToCopyTo].Cells["Power Graph Value " + (i + 1)].Value = advancedDataGridView.Rows[rowToCopy].Cells["Power Graph Value " + (i + 1)].Value;
                }
                advancedDataGridView.Rows[rowToCopyTo].Cells["BHP RPM 1"].Value = advancedDataGridView.Rows[rowToCopy].Cells["BHP RPM 1"].Value;
                advancedDataGridView.Rows[rowToCopyTo].Cells["Torque"].Value = advancedDataGridView.Rows[rowToCopy].Cells["Torque"].Value;
                advancedDataGridView.Rows[rowToCopyTo].Cells["Torque RPM 1"].Value = advancedDataGridView.Rows[rowToCopy].Cells["Torque RPM 1"].Value;
                advancedDataGridView.Rows[rowToCopyTo].Cells["Torque RPM 2"].Value = advancedDataGridView.Rows[rowToCopy].Cells["Torque RPM 2"].Value;
                advancedDataGridView.Rows[rowToCopyTo].Cells["RPM Limit"].Value = advancedDataGridView.Rows[rowToCopy].Cells["RPM Limit"].Value;
                advancedDataGridView.Rows[rowToCopyTo].Cells["RPM Idle"].Value = advancedDataGridView.Rows[rowToCopy].Cells["RPM Idle"].Value;
            }
        }

        private void pullFromPCSX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String res;
            if (paramDataManager.PCSX2.OpenProcess("pcsx2.exe", out res) == true)
            {
                dsParamData = paramDataManager.PullFromPCSX2("Car Data", paramDataManager.fsCarData);
                if (dsParamData == null)
                {
                    MessageBox.Show(this, "Failed to pull data from PCSX2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    filename = "Loaded from PCSX2";
                    LoadDataTable(dsParamData.Tables["Car Data"]);
                }
            }
            else
                MessageBox.Show(this, "Failed to attach to PCSX2: " + res, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pushToPCSX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String res;
            if (paramDataManager.PCSX2.OpenProcess("pcsx2.exe", out res) == true)
            {
                if(paramDataManager.PushToPCSX2() == false)
                    MessageBox.Show(this, "Failed to push data to PCSX2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Failed to attach to PCSX2: " + res, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
