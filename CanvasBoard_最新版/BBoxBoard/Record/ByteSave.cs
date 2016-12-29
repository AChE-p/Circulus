using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace saveTest
{
    public class ByteSave<T> where T : SavingForm, new()
    {
        public String fileInfo = "";
        public List<T> savingData;

        public ByteSave()
        {
            savingData = new List<T>();
        }
        public bool SaveToFile(String FilePath, int byteLen)
        {
            //如果已经存在的话要外部删除文件，否则不允许读写
            //如果返回false需要查找原因，可能是每次生成的Byte长度不对
            if (File.Exists(FilePath)) return false;
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            int Count = savingData.Count;
            byte[] byteCount = BitConverter.GetBytes(Count);
            byte[] byteSingle = BitConverter.GetBytes(byteLen);
            fs.Write(byteCount, 0, byteCount.Length);
            fs.Write(byteSingle, 0, byteSingle.Length);
            foreach (T x in savingData)
            {
                fs.Write(x.ConvertToByte(byteLen), 0, byteLen);
            }
            byte[] byteInfo = UnicodeEncoding.UTF8.GetBytes(fileInfo);
            fs.Write(byteInfo, 0, byteInfo.Length);
            fs.Close();
            return true;
        }
        public bool ReadFromFile(String FilePath, int byteLen)
        {
            if (!File.Exists(FilePath)) return false;
            //必须手动清空数据，保证不被误删
            if (savingData.Count != 0) return false;
            FileStream fs = new FileStream(FilePath, FileMode.Open);
            long size = fs.Length;
            byte[] num = new byte[4];
            fs.Read(num, 0, 4);
            int Count = BitConverter.ToInt32(num, 0);
            fs.Read(num, 0, 4);
            int Single = BitConverter.ToInt32(num, 0);
            if (Single != byteLen) return false;
            byte[] data = new byte[byteLen];
            for (int i=0; i<Count; i++)
            {
                T x = new T();
                fs.Read(data, 0, byteLen);
                x.ConvertFromByte(data);
                savingData.Add(x);
            }
            byte[] byteInfo = new byte[1024];
            fs.Read(byteInfo, 0, 1024);
            fileInfo = UnicodeEncoding.UTF8.GetString(byteInfo);
            MessageBox.Show(fileInfo);
            return true;
        } 
    }
    public class SavingForm
    {
        //继承自这个类，写好怎么生成Byte和怎么解析Byte，写好数据
        public SavingForm() { }
        public virtual byte[] ConvertToByte(int byteLen)
        {
            //在这里写转换成Byte的函数，且必须是byteLen位的
            byte[] data = new byte[byteLen];
            return data;
        }
        public virtual void ConvertFromByte(byte[] data)
        {
            //在这里写从Byte转换成数据的代码
        }
    }
}
