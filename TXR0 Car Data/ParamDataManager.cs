using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Memory;

namespace TXR0_Car_Data
{
    public class ParamDataManager
    {
        public enum VarType
        {
            Data,
            DataLength,
            Index,
            Index1,
            Button
        };
        #region Structures
        public struct FileStructure
        {
            public FileStructure(VarType _varType, System.Type _dataType, String _name = null, Int32 _length = 0, DataGridViewColumn _columnType = null) { varType = _varType; dataType = _dataType; name = _name; length = _length; columnType = _columnType; }
            public VarType varType;
            public System.Type dataType;
            public String name;
            public Int32 length;
            public DataGridViewColumn columnType;
        };
        public struct ELFType
        {
            public ELFType(UInt32 _hash, UInt32 _address, String _type) { hash = _hash; address = _address; type = _type; }
            public UInt32 hash;
            public UInt32 address;
            public String type;
        }
        #endregion Structures
        #region Variables
        ParamData paramData = new ParamData("Car Data");
        public DataSet dsParamData = null;
        Byte[] ELFData = null;
        public readonly List<ELFType> elfTypes = new List<ELFType>()
        {
            new ELFType(0x562BCFD0, 0x1D8F20, "US"),
            new ELFType(0x765A0E92, 0x1D98E0, "EU"),
            new ELFType(0xC3859771, 0x1E0500, "JP")
        };
        public String elfType = "Unknown";
        public Mem PCSX2 = new Mem();
        #endregion Variables
        #region File Structures
        public readonly List<FileStructure> fsCarData = new List<FileStructure>() {
            new FileStructure(VarType.Index1, typeof(Int32), "Car Number"),
            new FileStructure(VarType.Data, typeof(Byte), "Car Flags"),
            new FileStructure(VarType.Data, typeof(Byte), "Unknown1_1"),
            new FileStructure(VarType.Data, typeof(Byte), "Drag Coef"),
            new FileStructure(VarType.Data, typeof(Byte), "Unknown1_3"),
            new FileStructure(VarType.Data, typeof(Byte), "Drive Type"),
            new FileStructure(VarType.Data, typeof(Byte), "Unknown2"),
            new FileStructure(VarType.Data, typeof(Byte), "Class"),
            new FileStructure(VarType.Data, typeof(Byte), "Shop Table"),
            new FileStructure(VarType.Data, typeof(String), "Name", 12),
            new FileStructure(VarType.Data, typeof(Single), "Gear Ratio ", 6),
            new FileStructure(VarType.Data, typeof(Single), "Final Drive"),
            new FileStructure(VarType.Data, typeof(Byte), "Engine Type"),
            new FileStructure(VarType.Data, typeof(Byte), "Cyclnders"),
            new FileStructure(VarType.Data, typeof(UInt16), "Displacement"),
            new FileStructure(VarType.Data, typeof(Single), "BHP"),
            new FileStructure(VarType.Data, typeof(UInt16), "BHP RPM ", 2),
            new FileStructure(VarType.Data, typeof(Single), "Torque"),
            new FileStructure(VarType.Data, typeof(UInt16), "Torque RPM ", 2),
            new FileStructure(VarType.Data, typeof(UInt16), "RPM Limit"),
            new FileStructure(VarType.Data, typeof(UInt16), "RPM Idle"),
            new FileStructure(VarType.Button, typeof(String), "Power Graph"),
            new FileStructure(VarType.Data, typeof(Single), "Power Graph Value ", 130),
            new FileStructure(VarType.Data, typeof(Single), "Length"),
            new FileStructure(VarType.Data, typeof(Single), "Width"),
            new FileStructure(VarType.Data, typeof(Single), "Height"),
            new FileStructure(VarType.Data, typeof(Single), "Wheelbase"),
            new FileStructure(VarType.Data, typeof(Single), "Front Wheel Offset"),
            new FileStructure(VarType.Data, typeof(Single), "Rear Wheel Offset"),
            new FileStructure(VarType.Data, typeof(Single), "Unknown3"),
            new FileStructure(VarType.Data, typeof(Single), "Weight"),
            new FileStructure(VarType.Data, typeof(Single), "Front Gravity"),
            new FileStructure(VarType.Data, typeof(Single), "Tire Adhesion Front"),
            new FileStructure(VarType.Data, typeof(Single), "Rear Gravity"),
            new FileStructure(VarType.Data, typeof(Single), "Tire Adhesion Rear"),
            new FileStructure(VarType.Data, typeof(Single), "Unknown4_", 10),
            new FileStructure(VarType.Data, typeof(Int32), "Unknown5_", 3),
            new FileStructure(VarType.Data, typeof(Single), "Unknown6_", 2),
            new FileStructure(VarType.Data, typeof(UInt16), "Unknown7_", 2),
            new FileStructure(VarType.Data, typeof(Single), "Unknown8_", 2),
            new FileStructure(VarType.Data, typeof(Single), "Gear Length Multiplier"),
            new FileStructure(VarType.Data, typeof(Single), "Gear Speed Multiplier"),
            new FileStructure(VarType.Data, typeof(Single), "Front DownForce"),
            new FileStructure(VarType.Data, typeof(Single), "Rear Downforce"),
            new FileStructure(VarType.Data, typeof(UInt16), "Unknown9_", 6),
            new FileStructure(VarType.Data, typeof(Single), "Steering Response"),
            new FileStructure(VarType.Data, typeof(Single), "Weight Transfer Front"),
            new FileStructure(VarType.Data, typeof(Single), "Weight Transfer Rear"),
            new FileStructure(VarType.Data, typeof(UInt16), "Unknown11_", 2),
            new FileStructure(VarType.Data, typeof(Single), "Unknown12_", 3),
            new FileStructure(VarType.Data, typeof(UInt16), "Unknown13_", 2),
            new FileStructure(VarType.Data, typeof(UInt16), "Price"),
            new FileStructure(VarType.Data, typeof(Byte), "Engine Position"),
            new FileStructure(VarType.Data, typeof(Byte), "Drivetrain Flag"),
            new FileStructure(VarType.Data, typeof(Byte), "Unknown14_", 4)
        };                                                         
        #endregion File Structures
        #region Data Loading
        public DataSet OpenFile(String TableName, String DataFileName, List<FileStructure> FileStructure)
        {
            //try
            {
                Byte[] data = null;

                if (DataFileName != null)
                {
                    using (FileStream stream = new FileStream(DataFileName, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        data = new byte[stream.Length];
                        stream.Read(data, 0, Convert.ToInt32(stream.Length));
                    }
                }
                if ((dsParamData = LoadTableData(data, TableName, FileStructure)) == null)
                    return null;
                return dsParamData;
            }
            //catch(Exception ex)
            //{
            //    MessageBox.Show(null, ex.Message, "Error loading data files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return null;
            //}
        }
        public DataSet OpenELFFile(String TableName, String DataFileName, List<FileStructure> FileStructure)
        {
            //try
            {
                Byte[] data = null;

                if (DataFileName != null)
                {
                    using (FileStream stream = new FileStream(DataFileName, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        UInt32 hash = GetHashFromELF(reader, 64);
                        elfType = GetELFType(hash);
                        UInt32 dataOffset = GetELFDataOffset(hash);
                        if (dataOffset != 0)
                        {
                            reader.BaseStream.Position = dataOffset;
                            data = reader.ReadBytes(137280);
                            if (data.Length != 137280)
                                return null;
                        }
                        else
                            return null;
                    }
                }
                // Clear potential already loaded data
                dsParamData = null;
                return LoadTableData(data, TableName, FileStructure);
            }
            //catch(Exception ex)
            //{
            //    MessageBox.Show(null, ex.Message, "Error loading data files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return null;
            //}
        }

        public bool LoadELFData(String ELFFileName)
        {
            if (ELFFileName != null)
            {
                using (FileStream stream = new FileStream(ELFFileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    ELFData = reader.ReadBytes(Convert.ToInt32(stream.Length));
                    if (ELFData.Length == Convert.ToInt32(stream.Length))
                        return true;
                    else
                        ELFData = null;
                }
            }
            return false;
        }
        private DataSet LoadTableData(Byte[] data, String TableName, List<FileStructure> structures)
        {
            //try
            {
                Int32 dataOffset = 0;
                Int32 dataCount = 0;
                object length = 0;
                if(data != null)
                {
                    if (data.Length % 0x30C != 0) // Check if Car Data
                        return null;
                    dataCount = data.Length / 0x30C;
                    dsParamData = paramData.CreateDataStructure(TableName, structures);
                    DataTable dtTable = dsParamData.Tables[TableName];
                    for (Int32 i = 0; i < dataCount; i++)
                    {
                        DataRow newRow = dtTable.NewRow();
                        length = 0;
                        foreach (FileStructure structure in structures)
                        {
                            if (structure.varType == VarType.DataLength)
                            {
                                length = GetValue(data, dataOffset, structure.dataType);
                                dataOffset += GetTypeLength(structure.dataType);
                            }
                            else if (structure.varType == VarType.Button)
                            {
                                newRow[structure.name] = "btn:" + structure.name;
                            }
                            else if (structure.varType == VarType.Index || structure.varType == VarType.Index1)
                            {
                                newRow[structure.name] = (structure.varType == VarType.Index) ? i : i + 1;
                            }
                            else if (structure.dataType == typeof(System.String) && ((Int32)length > 0 || structure.length > 0))
                            {
                                if (structure.length > 0)
                                    length = structure.length;
                                newRow[structure.name] = GetValue(data, dataOffset, structure.dataType, (Int32)length);
                                dataOffset += (Int32)length;
                            }
                            else if (structure.dataType != typeof(System.String))
                            {
                                if (structure.length != 0)
                                {
                                    for (Int32 j = 0; j < structure.length; j++)
                                    {
                                        String name = structure.name + Convert.ToString(j + 1);
                                        newRow[name] = GetValue(data, dataOffset, structure.dataType);
                                        dataOffset += GetTypeLength(structure.dataType);
                                    }
                                }
                                else
                                {
                                    newRow[structure.name] = GetValue(data, dataOffset, structure.dataType);
                                    dataOffset += GetTypeLength(structure.dataType);
                                }
                            }
                        }
                        dtTable.Rows.Add(newRow);
                    }
                    return dsParamData;
                }
                else
                    return null;
            }
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        private Int32 FindPCSX2Offset(Byte[] bytes)
        {
            if (PCSX2.mProc.Process != null)
            {
                String byteString = "";
                Int32 offset;
                foreach (Byte thisByte in bytes)
                {
                    byteString += thisByte.ToString("X") + " ";
                }
                byteString = byteString.Substring(0, byteString.Length - 1);
                var find = PCSX2.AoBScan(byteString, true, true);
                find.Wait();
                var res = find.Result;
                offset = Convert.ToInt32(res.SingleOrDefault());
                return offset;
            }
            else
                return 0;
        }
        public DataSet PullFromPCSX2(String TableName, List<FileStructure> FileStructure)
        {
            if (PCSX2.mProc.Process != null)
            {
                Byte[] data = null;
                Int32 LoadedGameStringPtr = FindPCSX2Offset(Encoding.ASCII.GetBytes("BOOT2 = cdrom0:"));
                if (LoadedGameStringPtr != 0)
                {
                    String LoadedGame = PCSX2.ReadString(LoadedGameStringPtr.ToString("X"));
                    String dataOffset = "202D8EA0";
                    if (LoadedGame.Contains("SLUS_201.89")) // US
                        dataOffset = "202D8EA0";
                    else if (LoadedGame.Contains("SLES_501.15")) // EU
                        dataOffset = "202D9860";
                    //else if (LoadedGame.Contains("SLPS_250.28")) // JP
                    //    dataOffset = "";
                    if (dataOffset != "")
                    {
                        data = PCSX2.ReadBytes(dataOffset, 137280);
                        if (data.Length != 137280)
                            return null;
                    }
                }
                else
                    return null;
                if (data != null)
                {
                    dsParamData = null;
                    return LoadTableData(data, TableName, FileStructure);
                }
                else
                    return null;
            }
            else
                return null;
        }
        #endregion Data Loading
        #region Data Saving
        private Byte[] GetByteDataFromTable(String TableName)
        {
            if(dsParamData != null && dsParamData.Tables.Contains(TableName))
            {
                using(DataTable table = dsParamData.Tables[TableName])
                {
                    Byte[] data = new Byte[table.Rows.Count * 0x30C]; // Create Data buffer for Car Data. Entry size is 0x30C
                    MemoryStream datastream = new MemoryStream(data);
                    BinaryWriter datawriter = new BinaryWriter(datastream);
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (FileStructure structure in fsCarData)
                        {
                            if (structure.varType != VarType.Index && structure.varType != VarType.Index1 && structure.varType != VarType.Button) // Ignore index and button var types as those are used by this app not game
                            {
                                Func<String, Int32> writedata = (String name) =>
                                {
                                    var cell = row[name];
                                    if (structure.dataType == typeof(Int32))
                                    {
                                        datawriter.Write(Convert.ToInt32(cell));
                                    }
                                    else if (structure.dataType == typeof(UInt32))
                                    {
                                        datawriter.Write(Convert.ToUInt32(cell));
                                    }
                                    else if (structure.dataType == typeof(Int16))
                                    {
                                        datawriter.Write(Convert.ToInt16(cell));
                                    }
                                    else if (structure.dataType == typeof(UInt16))
                                    {
                                        datawriter.Write(Convert.ToUInt16(cell));
                                    }
                                    else if (structure.dataType == typeof(SByte))
                                    {
                                        datawriter.Write(Convert.ToSByte(cell));
                                    }
                                    else if (structure.dataType == typeof(Byte))
                                    {
                                        datawriter.Write(Convert.ToByte(cell));
                                    }
                                    else if (structure.dataType == typeof(String))
                                    {
                                        Byte[] tempstringbuf = new Byte[structure.length];
                                        //Byte[] cellbytes = Encoding.ASCII.GetBytes(cell.ToString());
                                        Byte[] cellbytes = System.Text.Encoding.GetEncoding(932).GetBytes(cell.ToString());
                                        for (Int32 i = 0; i < cellbytes.Count(); i++)
                                        {
                                            tempstringbuf[i] = cellbytes[i];
                                        }
                                        datawriter.Write(tempstringbuf);
                                    }
                                    else if (structure.dataType == typeof(Single))
                                    {
                                        datawriter.Write(Convert.ToSingle(cell));
                                    }
                                    return 0;
                                };

                                if (structure.dataType != typeof(String) && structure.length > 0)
                                {
                                    for (Int32 i = 0; i < structure.length; i++)
                                    {
                                        writedata(structure.name + Convert.ToString(i + 1));
                                    }
                                }
                                else
                                {
                                    writedata(structure.name);
                                }
                            }
                        }
                    }
                    return data;
                }
            }
            return null;
        }

        public bool SaveELF(String FileName)
        {
            Byte[] data = GetByteDataFromTable("Car Data");
            if (FileName != null && ELFData != null)
            {
                MemoryStream datastream = new MemoryStream(ELFData);
                BinaryReader datareader = new BinaryReader(datastream);

                UInt32 hash = GetHashFromELF(datareader, 64);
                Int32 dataOffset = Convert.ToInt32(GetELFDataOffset(hash));
                elfType = GetELFType(hash);

                if (dataOffset != 0)
                {
                    BinaryWriter datawriter = new BinaryWriter(datastream);
                    using (FileStream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                    {
                        datawriter.Seek(dataOffset, SeekOrigin.Begin);
                        datawriter.Write(data);
                        stream.Write(ELFData, 0, ELFData.Length);
                        ELFData = null; // Clear ELF Data once written
                        return true;
                    }
                }
                else
                    return false;
            }
            return false;
        }
        public bool SaveCarData(String FileName)
        {
            Byte[] data = GetByteDataFromTable("Car Data");
            if (FileName != null)
            {
                using (FileStream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(data, 0, data.Length);
                    return true;
                }
            }
            else
                return false;
        }
        public bool PushToPCSX2()
        {
            if (PCSX2.mProc.Process != null)
            {
                Int32 LoadedGameStringPtr = FindPCSX2Offset(Encoding.ASCII.GetBytes("BOOT2 = cdrom0:"));
                if (LoadedGameStringPtr != 0)
                {
                    String LoadedGame = PCSX2.ReadString(LoadedGameStringPtr.ToString("X"));
                    String dataOffset = "202D8EA0";
                    if (LoadedGame.Contains("SLUS_201.89")) // US
                        dataOffset = "202D8EA0";
                    else if (LoadedGame.Contains("SLES_501.15")) // EU
                        dataOffset = "202D9860";
                    //else if (LoadedGame.Contains("SLPS_250.28")) // JP
                    //    dataOffset = "";
                    if (dataOffset != "")
                    {
                        Byte[] data = GetByteDataFromTable("Car Data");
                        PCSX2.WriteBytes(dataOffset, data);
                        return true;
                    }
                    else
                        return false;
                }
                return false;
            }
            else
                return false;
        }
        #endregion Data Saving
        #region Data Processing
        private object GetValue(Byte[] data, Int32 offset, System.Type datatype, Int32 length = 0)
        {
            object value = null;
            if(datatype == typeof(System.Byte))
            {
                value = Convert.ToByte(data[offset]);
            }
            else if (datatype == typeof(System.SByte))
            {
                value = Convert.ToSByte((SByte)data[offset]);
            }
            else if (datatype == typeof(System.UInt16))
            {
                value = BitConverter.ToUInt16(data, offset);
            }
            else if (datatype == typeof(System.Int16))
            {
                value = BitConverter.ToInt16(data, offset);
            }
            else if (datatype == typeof(System.UInt32))
            {
                value = BitConverter.ToUInt32(data, offset);
            }
            else if (datatype == typeof(System.Int32))
            {
                value = BitConverter.ToInt32(data, offset);
            }
            else if (datatype == typeof(System.UInt64))
            {
                value = BitConverter.ToUInt64(data, offset);
            }
            else if (datatype == typeof(System.Int64))
            {
                value = BitConverter.ToInt64(data, offset);
            }
            else if (datatype == typeof(System.Single))
            {
                value = BitConverter.ToSingle(data, offset);
            }
            else if (datatype == typeof(System.Double))
            {
                value = BitConverter.ToDouble(data, offset);
            }
            else if (datatype == typeof(System.String))
            {
                if (length == 0)
                {
                    length = GetStringLength(data, offset);
                }
                //String dataString = System.Text.ASCIIEncoding.ASCII.GetString(data, offset, length);
                String dataString = System.Text.Encoding.GetEncoding(932).GetString(data, offset, length);
                //String dataString = System.Text.Encoding.GetEncoding(51932).GetString(data, offset, length);
                value = dataString;
            }
            return value;
        }
        private Int32 GetStringLength(byte[] data, Int32 offset)
        {
            for(Int32 i = 0; i < data.Length - offset; i++)
            {
                if (data[offset + i] == 0) return i;
            }
            return 0;
        }
        private Int32 GetTypeLength(System.Type type)
        {
            if (type == typeof(System.Byte) || type == typeof(System.SByte))
                return sizeof(Byte);
            else if (type == typeof(System.Int16) || type == typeof(System.UInt16))
                return sizeof(Int16);
            else if (type == typeof(System.Single) || type == typeof(System.Int32) || type == typeof(System.UInt32))
                return sizeof(Int32);
            else if (type == typeof(System.Double) || type == typeof(System.Int64) || type == typeof(System.UInt64))
                return sizeof(Double);
            else
                return 0;
        }
        private Int32 GetLanguageRow(DataTable dt, Int32 id)
        {
            if(dt.Columns.Contains("ID"))
            {
                String select = "ID = " + Convert.ToString(id);
                DataRow[] rows = dt.Select(select);
                if (rows.Length > 0)
                {
                    Int32 index = dt.Rows.IndexOf(rows[0]);
                    return index;
                }
                else
                    return -1;
            }
            return -1;
        }
        private UInt32 GetHashFromELF(BinaryReader reader, UInt32 count)
        {
            UInt32 hash = 0;
            for (UInt32 i = 0; i < count; i++)
            {
                hash = (hash >> 1) | ((hash & 1) << 31);
                hash ^= reader.ReadUInt32();
            }
            return hash;
        }
        private UInt32 GetELFDataOffset(UInt32 hash)
        {
            for(Int32 i = 0; i < elfTypes.Count; i++)
            {
                if (elfTypes[i].hash == hash)
                    return elfTypes[i].address;
            }
            return 0;
        }
        private String GetELFType(UInt32 hash)
        {
            for (Int32 i = 0; i < elfTypes.Count; i++)
            {
                if (elfTypes[i].hash == hash)
                    return elfTypes[i].type;
            }
            return "Unknown";
        }
        #endregion Data Processing
    }
}
