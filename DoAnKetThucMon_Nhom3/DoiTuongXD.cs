using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnKetThucMon_Nhom3
{
    public class DoiTuongXD
    {
        private int dem;
        private List<DoiTuong> a = new List<DoiTuong>();

        public int Dem { get => dem; set => dem = value; }
        public List<DoiTuong> A { get => a; set => a = value; }
    }
}
