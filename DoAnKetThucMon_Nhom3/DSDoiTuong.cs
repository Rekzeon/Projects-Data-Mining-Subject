using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Collections;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using DoAnKetThucMon_Nhom3.Make_a_tree_decision;


namespace DoAnKetThucMon_Nhom3
{
    public class DSDoiTuong
    {
        List<DoiTuong> lDoiTuong = new List<DoiTuong>();
        ArrayList kqDoLoiThgTin = new ArrayList();
        TreeDecision aTreeD = new TreeDecision();

        public List<DoiTuong> LDoiTuong { get => lDoiTuong; set => lDoiTuong = value; }
        public TreeDecision ATreeD { get => aTreeD; set => aTreeD = value; }

        public void nhapData(string file)
        {
            var package = new ExcelPackage(new FileInfo(file));
            ExcelWorksheet workSheet = package.Workbook.Worksheets[2];
            for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
            {
                DoiTuong dtTemp = new DoiTuong();
                for (int j = 2; j < workSheet.Dimension.End.Column; j++)
                {
                    ThuocTinh ttTemp = new ThuocTinh();
                    ttTemp.STenThuocTinh = workSheet.Cells[workSheet.Dimension.Start.Row, j].Value.ToString();
                    ttTemp.SGiaTri = workSheet.Cells[i, j].Value.ToString();
                    dtTemp.LThuocTinh.Add(ttTemp);
                }
                //Xét đối tượng đã tồn tại chưa
                //function
                if(xetTonTaiDT(dtTemp)==false)
                {
                    dtTemp.SKetQua = workSheet.Cells[i, workSheet.Dimension.End.Column].Value.ToString();
                    lDoiTuong.Add(dtTemp);
                }                    
            }
        }

        public void nhapDataTest(string file)
        {
            var package = new ExcelPackage(new FileInfo(file));
            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
            for(int i = workSheet.Dimension.Start.Row+1;i<= workSheet.Dimension.End.Row;i++)
            {
                DoiTuong dtTemp = new DoiTuong();
                for (int j = 2; j < workSheet.Dimension.End.Column; j++)
                {
                    ThuocTinh ttTemp = new ThuocTinh();
                    ttTemp.STenThuocTinh = workSheet.Cells[workSheet.Dimension.Start.Row, j].Value.ToString();
                    ttTemp.SGiaTri = workSheet.Cells[i, j].Value.ToString();
                    dtTemp.LThuocTinh.Add(ttTemp);
                }
                dtTemp.SKetQua= workSheet.Cells[i, workSheet.Dimension.End.Column].Value.ToString();
                lDoiTuong.Add(dtTemp);                
            }
        }


        public double tinhTiLeTTinh(string tenTTinh)
        {
            int xettontai = 0, tongSLTC = 0, tongSLKQTC = 0;
            double tileTTinh = 0, entropyTT = 0;
            List<string> kqCanXet = new List<string>();
            List<string> tcCanXet = new List<string>();
            foreach (DoiTuong dt in lDoiTuong)
            {
                foreach (string t in kqCanXet)
                    if (string.Compare(dt.SKetQua, t) == 0)
                    {
                        xettontai = 1;
                        break;
                    }
                if (xettontai == 0)
                    kqCanXet.Add(dt.SKetQua);
                else
                    xettontai = 0;
            }

            foreach (DoiTuong tt in lDoiTuong)
            {
                foreach (ThuocTinh tc in tt.LThuocTinh)
                {
                    foreach (string t in tcCanXet)
                        if (string.Compare(tc.SGiaTri, t) == 0)
                        {
                            xettontai = 1;
                            break;
                        }
                    if (xettontai == 0 && string.Compare(tc.STenThuocTinh, tenTTinh) == 0)
                        tcCanXet.Add(tc.SGiaTri);
                    else
                        xettontai = 0;
                }
            }

            foreach (string i in tcCanXet)
            {
                foreach (string j in kqCanXet)
                {
                    foreach (DoiTuong dt in lDoiTuong)
                    {
                        tongSLTC += dt.xetTChat(tenTTinh, i);
                        tongSLKQTC += dt.xetKQTChat(tenTTinh, i, j);
                    }
                    if (tongSLKQTC != 0)
                        tileTTinh += -tongSLKQTC * 1.0 / tongSLTC * Math.Log(tongSLKQTC * 1.0 / tongSLTC, 2);
                    tongSLTC = tongSLKQTC = 0;
                }
                entropyTT += xetTiLeTChat(tenTTinh, i, lDoiTuong.Count) * 1.0 * tileTTinh;
                tileTTinh = 0;
            }
            return entropyTT;
        }

        public double xetTiLeTChat(string tenTT, string tenTChat, double tongSLgDTuong) //Tỉ lệ tính chất chiếm trong tổng số lượng ĐỐI TƯỢNG
        {
            int count = 0;
            foreach (DoiTuong dt in LDoiTuong)
                foreach (ThuocTinh tt in dt.LThuocTinh)
                    if (string.Compare(tt.STenThuocTinh, tenTT) == 0 &&
                        string.Compare(tt.SGiaTri, tenTChat) == 0)
                    {
                        count++;
                        break;
                    }
            return count * 1.0 / tongSLgDTuong;
        }

        //public ArrayList tinhEntropyTungTChat()
        //{
        //    ArrayList KetQua = new ArrayList();
        //    List<string> ttinhphaixet = new List<string>();
        //    foreach (ThuocTinh tt in lDoiTuong[0].LThuocTinh)
        //        ttinhphaixet.Add(tt.STenThuocTinh);
        //    foreach (string t in ttinhphaixet)
        //    {
        //        KetQua.Add(Math.Round(tinhTiLeTTinh(t), 3));
        //    }
        //    return KetQua;
        //}

        public double tinhEntropyKQ()
        {
            int xettontai = 0, tongSLTC = 0;
            double entropyKQ = 0;
            List<string> tcCanXet = new List<string>();
            foreach(DoiTuong dt in lDoiTuong)
            {
                ThuocTinh tt = dt.LThuocTinh[dt.LThuocTinh.Count - 1];
                foreach (string t in tcCanXet)
                    if (string.Compare(tt.SGiaTri, t) == 0)
                    {
                        xettontai = 1;
                        break;
                    }
                if (xettontai == 0)
                    tcCanXet.Add(tt.SGiaTri);
                else
                    xettontai = 0;
            }

            foreach (string i in tcCanXet)
            {
                foreach (DoiTuong dt in lDoiTuong)
                {
                    tongSLTC += dt.xetTChat(dt.LThuocTinh[dt.LThuocTinh.Count - 1].STenThuocTinh, i);
                }
                entropyKQ += -tongSLTC * 1.0 / lDoiTuong.Count * Math.Log(tongSLTC * 1.0 / lDoiTuong.Count, 2);
                tongSLTC =  0;
            }
            return entropyKQ;
        }

        public ArrayList tinhIGainTungTTinh()
        {
            ArrayList KetQua = new ArrayList();
            List<string> ttinhphaixet = new List<string>();
            foreach (ThuocTinh tt in lDoiTuong[0].LThuocTinh)
                ttinhphaixet.Add(tt.STenThuocTinh);
            foreach (string t in ttinhphaixet)
            {
                KetQua.Add(Math.Round(tinhEntropyKQ()-tinhTiLeTTinh(t), 3));
            }
            return KetQua;
        }

        public string xacdinhTTinh()
        {
            kqDoLoiThgTin = tinhIGainTungTTinh();
            int max = 0;
            for (int i = 1; i < kqDoLoiThgTin.Count; i++)
            {
                double temp = (double)kqDoLoiThgTin[max];
                double temp2 = (double)kqDoLoiThgTin[i];
                if (temp < temp2)
                    max = i;
            }
            string value = lDoiTuong[0].LThuocTinh[max].STenThuocTinh;
            return value;
        }

        public void createATree()
        {
            while(lDoiTuong.Count !=0)
            {
                List<string> tcCanXet = new List<string>();
                List<string> kqCanXet = new List<string>();
                int xettontai = 0, count = 0/*, xetPhanTich = 0*/;
                List<string> conditionTC = new List<string>();
                DecisionNode nodeDTemp = new DecisionNode();
                nodeDTemp.Info = xacdinhTTinh(); // lấy tính chất cần xét của thuật toán ID3
                foreach (DoiTuong tt in lDoiTuong)
                {
                    foreach (ThuocTinh tc in tt.LThuocTinh)
                    {
                        foreach (string t in tcCanXet)
                            if (string.Compare(tc.SGiaTri, t) == 0)
                            {
                                xettontai = 1;
                                break;
                            }
                        if (xettontai == 0 && string.Compare(tc.STenThuocTinh, nodeDTemp.Info) == 0)
                        {
                            tcCanXet.Add(tc.SGiaTri);
                            ConditionNode nodeCTemp = new ConditionNode(tc.SGiaTri);
                            nodeDTemp.Condition.Add(nodeCTemp);
                        }
                        else
                            xettontai = 0;
                    }
                }

                foreach (DoiTuong dt in lDoiTuong)
                {
                    foreach (string t in kqCanXet)
                        if (string.Compare(dt.SKetQua, t) == 0)
                        {
                            xettontai = 1;
                            break;
                        }
                    if (xettontai == 0)
                        kqCanXet.Add(dt.SKetQua);
                    else
                        xettontai = 0;
                }

                foreach (string t in tcCanXet)
                {
                    List<DoiTuongXD> demSLgTHop = new List<DoiTuongXD>(kqCanXet.Count);
                    foreach (string i in kqCanXet)
                    {
                        DoiTuongXD doituongtemp = new DoiTuongXD();
                        foreach (DoiTuong dt in LDoiTuong)
                            foreach (ThuocTinh tt in dt.LThuocTinh)
                                if (string.Compare(tt.STenThuocTinh, nodeDTemp.Info) == 0 &&
                                    string.Compare(tt.SGiaTri, t) == 0 &&
                                    string.Compare(dt.SKetQua, i) == 0)
                                {
                                    count++;
                                    doituongtemp.A.Add(dt);
                                    break;
                                }
                        doituongtemp.Dem = count;
                        count = 0;
                        demSLgTHop.Add(doituongtemp);
                    }
                    int demKQKhac0 = 0;
                    foreach (DoiTuongXD l in demSLgTHop)
                        if (l.Dem != 0)
                            demKQKhac0++;
                    if (demKQKhac0 == 1)
                    {
                        DoiTuongXD max = demSLgTHop[0];
                        foreach (DoiTuongXD k in demSLgTHop)
                            if (max.Dem < k.Dem)
                            {
                                max = k;
                            }
                        ResultNode nodeRtemp = new ResultNode(max.A[0].SKetQua);
                        foreach(ThuocTinh tt in max.A[0].LThuocTinh)
                        {
                            if(tt.STenThuocTinh==nodeDTemp.Info)
                            {
                                nodeDTemp.addResult2Condition(tt.SGiaTri, nodeRtemp);
                                break;
                            }                                   
                        }    
                        foreach (DoiTuong i in max.A)
                            lDoiTuong.Remove(i);
                    }      
                }
                aTreeD.addNewD(nodeDTemp, aTreeD.Root, aTreeD);
            }
        }

        public bool xetTonTaiDT(DoiTuong DTXet)
        {
            int iDiemGiong = 0;
            foreach(DoiTuong dt in lDoiTuong)
            {
                foreach (ThuocTinh tt1 in dt.LThuocTinh)
                    foreach (ThuocTinh tt2 in DTXet.LThuocTinh)
                        if (tt1.STenThuocTinh == tt2.STenThuocTinh && tt1.SGiaTri == tt2.SGiaTri)
                            iDiemGiong++;
                if (iDiemGiong == dt.LThuocTinh.Count())
                    return true;
                else
                    iDiemGiong = 0;
            }
            return false;
        }
    }
}
