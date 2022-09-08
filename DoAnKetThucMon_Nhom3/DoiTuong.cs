using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnKetThucMon_Nhom3
{
    public class DoiTuong
    {
        List<ThuocTinh> lThuocTinh = new List<ThuocTinh>();
        string sKetQua;
        public List<ThuocTinh> LThuocTinh { get => lThuocTinh; set => lThuocTinh = value; }
        public string SKetQua { get => sKetQua; set => sKetQua = value; }

        public ThuocTinh taoThuocTinh(string name, string value)
        {
            ThuocTinh newTT = new ThuocTinh();
            newTT.STenThuocTinh = name;
            newTT.SGiaTri = value;
            lThuocTinh.Add(newTT);
            return newTT;
        }

        public int xetTChat(string tenTTinh, string giaTri)
        {
            foreach (ThuocTinh t in lThuocTinh)
                if (string.Compare(t.STenThuocTinh, tenTTinh) == 0 &&
                    string.Compare(t.SGiaTri, giaTri) == 0)
                    return 1;
            return 0;
        }

        public int xetKQTChat(string tenTTinh, string giaTri, string KetQua)
        {
            foreach (ThuocTinh t in lThuocTinh)
                if (string.Compare(t.STenThuocTinh, tenTTinh) == 0 &&
                    string.Compare(t.SGiaTri, giaTri) == 0 && string.Compare(KetQua, sKetQua) == 0)
                    return 1;
            return 0;
        }

        public string takeValueTTinh(string Thuoc_Tinh)
        {
            foreach(ThuocTinh tt in lThuocTinh)
            {
                if (tt.STenThuocTinh.Equals(Thuoc_Tinh))
                    return tt.SGiaTri;
            }
            return null;
        }
    }
}
