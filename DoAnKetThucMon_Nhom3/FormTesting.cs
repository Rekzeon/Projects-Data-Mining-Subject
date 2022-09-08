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
    public partial class FormTesting : Form
    {
        DSDoiTuong run = new DSDoiTuong();
        public FormTesting()
        {
            InitializeComponent();
        }

        private void chọnNguồnDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                string locationFile = ofd.FileName;
                run.nhapDataTest(locationFile);
                run.createATree();
                MessageBox.Show("Xử lý dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                colorTBx.ReadOnly = false;
                shapeTBx.ReadOnly = false;
                sizeTBx.ReadOnly = false;
            }    
            
        }

        private void resultBtn_Click(object sender, EventArgs e)
        {            
            DoiTuong input = new DoiTuong();
            if (sizeTBx.Text != string.Empty)
            {
                ThuocTinh temp = new ThuocTinh();
                temp.STenThuocTinh = label2.Text;
                temp.SGiaTri = sizeTBx.Text;
                input.LThuocTinh.Add(temp);
            }
            else
            {
                MessageBox.Show("Dữ liệu " + label2.Text + " chưa được điền điền!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (colorTBx.Text != string.Empty)
            {
                ThuocTinh temp = new ThuocTinh();
                temp.STenThuocTinh = label3.Text;
                temp.SGiaTri = colorTBx.Text;
                input.LThuocTinh.Add(temp);
            }
            else
            {
                MessageBox.Show("Dữ liệu " + label3.Text + " chưa được điền điền!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (shapeTBx.Text != string.Empty)
            {
                ThuocTinh temp = new ThuocTinh();
                temp.STenThuocTinh = f.Text;
                temp.SGiaTri = shapeTBx.Text;
                input.LThuocTinh.Add(temp);
            }
            else
            {
                MessageBox.Show("Dữ liệu " + f.Text + " chưa được điền điền!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string result = run.ATreeD.findResult2Decide(input, run.ATreeD.Root);
            if (result == null)
                resultLabel.Text = "Trường hợp trên không có kết quả quyết định!";
            else
                resultLabel.Text = "Trường hợp trên có kết quả là " + result;
        }
    }
}
