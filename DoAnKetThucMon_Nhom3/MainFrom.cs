using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnKetThucMon_Nhom3
{
    public partial class MainFrom : Form
    {
        DSDoiTuong run = new DSDoiTuong();
        public MainFrom()
        {
            InitializeComponent();
        }

        private void điềuKiệnTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTesting a = new FormTesting();
            a.ShowDialog();
        }

        private void conditionMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string locationFile = ofd.FileName;
                run.nhapData(locationFile);
                try { run.createATree(); }catch { MessageBox.Show("Loi nek"); }
                
                MessageBox.Show("Xử lý dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nameTBx.Enabled = true;
                dobPicker.Enabled = true;
                cmndTBx.Enabled = true;
                phoneTBx.Enabled = true;                
                shkTBx.Enabled = true;
                hopdongChBx.Enabled = true;
                resultBtn.Enabled = true;
            }
            nameTBx.Focus();
        }

        private void resultBtn_Click(object sender, EventArgs e)
        {
            DoiTuong input = new DoiTuong();
            if(checkInfoEmpty())
            {
                MessageBox.Show("Thông tin khách cho vay không đủ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTBx.Focus();
                return;
            }    
            if(processInputData(input)==false)
                return;
            string result = run.ATreeD.findResult2Decide(input, run.ATreeD.Root);
            resultLabel.Text = "Đối tượng trên sẽ quyết định " + result;
        }

        public bool checkInfoEmpty()
        {
            if (nameTBx.Text == string.Empty || cmndTBx.Text == string.Empty || phoneTBx.Text == string.Empty ||   debitTBx.Text == string.Empty|| shkTBx.Text == string.Empty|| shkTBx.Text == string.Empty)
                return true;
            else return false;
        }

        public bool processInputData(DoiTuong input)
        {
            try
            {
                //Xét điều kiện "ĐỘ TUỔI (18-75)"
                if (DateTime.Now.Year - dobPicker.Value.Year > 17 && DateTime.Now.Year - dobPicker.Value.Year < 76)
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Độ tuổi yêu cầu (18 - 75)";
                    temp.SGiaTri = "Đạt";
                    input.LThuocTinh.Add(temp);
                }
                else
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Độ tuổi yêu cầu (18 - 75)";
                    temp.SGiaTri = "Không đạt";
                    input.LThuocTinh.Add(temp);
                }

                //Xét điều kiện "SỔ HỘ KHẨU/SỔ TẠM TRÚ"
                if (int.Parse(shkTBx.Text.ToString()) >= 100000000 && int.Parse(shkTBx.Text.ToString()) <= 999999999)
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Hộ khẩu/Tạm trú";
                    temp.SGiaTri = "Có";
                    input.LThuocTinh.Add(temp);
                }
                else
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Hộ khẩu/Tạm trú";
                    temp.SGiaTri = "Không";
                    input.LThuocTinh.Add(temp);
                }

                //Xét điều kiện "CMND"
                if (int.Parse(cmndTBx.Text.ToString()) >= 100000000 && int.Parse(cmndTBx.Text.ToString()) <= 999999999)
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "CMND (Có/Không)";
                    temp.SGiaTri = "Có";
                    input.LThuocTinh.Add(temp);
                }
                else
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "CMND (Có/Không)";
                    temp.SGiaTri = "Không";
                    input.LThuocTinh.Add(temp);
                }

                //Xét điều kiện "HỢP ĐỒNG LAO ĐỘNG"
                if (tenCTYTBx.Text == string.Empty || chucvuTBx.Text == string.Empty || salaryTBx.Text == string.Empty)
                {
                    if (MessageBox.Show("Thông tin việc làm không đầy đủ! Hệ thống sẽ xử lý thông tin hợp đồng lao động cho người vay là trống!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        ThuocTinh temp = new ThuocTinh();
                        temp.STenThuocTinh = "Hợp đồng lao động (Có/Không)";
                        temp.SGiaTri = "Không";
                        input.LThuocTinh.Add(temp);
                    }
                    else
                    {
                        tenCTYTBx.Focus();
                    }
                }
                else
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Hợp đồng lao động (Có/Không)";
                    temp.SGiaTri = "Có";
                    input.LThuocTinh.Add(temp);
                }

                //Xét điều kiện "THU NHẬP ỔN ĐỊNH"
                if (int.Parse(salaryTBx.Text.ToString()) >= 6000000)
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Thu nhập ổn định (>=6tr)";
                    temp.SGiaTri = "Đạt";
                    input.LThuocTinh.Add(temp);
                }
                else
                {
                    ThuocTinh temp = new ThuocTinh();
                    temp.STenThuocTinh = "Thu nhập ổn định (>=6tr)";
                    temp.SGiaTri = "Không đạt";
                    input.LThuocTinh.Add(temp);
                }
            }
            catch
            {
                MessageBox.Show("Thông tin nhập bị sai!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTBx.Focus();
                return false;
            }
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void hopdongChBx_CheckedChanged(object sender, EventArgs e)
        {
            if(hopdongChBx.Checked==true)
            {
                tenCTYTBx.Enabled = true;
                chucvuTBx.Enabled = true;
                salaryTBx.Enabled = true;
                debitTBx.Enabled = true;
            }    
            else
            {
                tenCTYTBx.Text = "";
                chucvuTBx.Text = "";
                salaryTBx.Text = "0";
                debitTBx.Text = "";

                tenCTYTBx.Enabled = false;
                chucvuTBx.Enabled = false;
                salaryTBx.Enabled = false;
                debitTBx.Enabled = false;
            }          
        }
    }
}
