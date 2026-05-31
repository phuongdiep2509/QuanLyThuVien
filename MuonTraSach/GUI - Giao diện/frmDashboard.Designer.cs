namespace Dashboard_Search.GUI
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            this.pnlSach = new System.Windows.Forms.Panel();
            this.lblTongSach = new System.Windows.Forms.Label();
            this.lblTieuDeSach = new System.Windows.Forms.Label();
            this.pnlDocGia = new System.Windows.Forms.Panel();
            this.lblTongDocGia = new System.Windows.Forms.Label();
            this.lblTieuDeDocGia = new System.Windows.Forms.Label();
            this.pnlDangMuon = new System.Windows.Forms.Panel();
            this.lblDangMuon = new System.Windows.Forms.Label();
            this.lblTieuDeDangMuon = new System.Windows.Forms.Label();
            this.pnlQuaHan = new System.Windows.Forms.Panel();
            this.lblQuaHan = new System.Windows.Forms.Label();
            this.lblTieuDeQuaHan = new System.Windows.Forms.Label();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblGrid1 = new System.Windows.Forms.Label();
            this.lblGrid2 = new System.Windows.Forms.Label();
            this.dgvPhieuMuonMoi = new System.Windows.Forms.DataGridView();
            this.dgvQuaHan = new System.Windows.Forms.DataGridView();
            this.pnlSach.SuspendLayout();
            this.pnlDocGia.SuspendLayout();
            this.pnlDangMuon.SuspendLayout();
            this.pnlQuaHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuonMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuaHan)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(200, 22);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(130, 50);
            this.dtpTuNgay.TabIndex = 14;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(450, 22);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 50);
            this.dtpDenNgay.TabIndex = 12;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.Location = new System.Drawing.Point(110, 25);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(146, 45);
            this.lblTuNgay.TabIndex = 15;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.Location = new System.Drawing.Point(350, 25);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(167, 45);
            this.lblDenNgay.TabIndex = 13;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(650, 18);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(150, 40);
            this.btnLoc.TabIndex = 11;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = false;
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(146)))), ((int)(((byte)(48)))));
            this.btnInBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInBaoCao.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnInBaoCao.Location = new System.Drawing.Point(820, 18);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(150, 40);
            this.btnInBaoCao.TabIndex = 10;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.UseVisualStyleBackColor = false;
            // 
            // pnlSach
            // 
            this.pnlSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(146)))), ((int)(((byte)(48)))));
            this.pnlSach.Controls.Add(this.lblTongSach);
            this.pnlSach.Controls.Add(this.lblTieuDeSach);
            this.pnlSach.Location = new System.Drawing.Point(20, 75);
            this.pnlSach.Name = "pnlSach";
            this.pnlSach.Size = new System.Drawing.Size(225, 110);
            this.pnlSach.TabIndex = 9;
            // 
            // lblTongSach
            // 
            this.lblTongSach.AutoSize = true;
            this.lblTongSach.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTongSach.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.lblTongSach.ForeColor = System.Drawing.Color.White;
            this.lblTongSach.Location = new System.Drawing.Point(15, 38);
            this.lblTongSach.Name = "lblTongSach";
            this.lblTongSach.Size = new System.Drawing.Size(91, 106);
            this.lblTongSach.TabIndex = 0;
            this.lblTongSach.Text = "0";
            // 
            // lblTieuDeSach
            // 
            this.lblTieuDeSach.AutoSize = true;
            this.lblTieuDeSach.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeSach.ForeColor = System.Drawing.Color.White;
            this.lblTieuDeSach.Location = new System.Drawing.Point(12, 12);
            this.lblTieuDeSach.Name = "lblTieuDeSach";
            this.lblTieuDeSach.Size = new System.Drawing.Size(316, 54);
            this.lblTieuDeSach.TabIndex = 1;
            this.lblTieuDeSach.Text = "TỔNG SỐ SÁCH";
            // 
            // pnlDocGia
            // 
            this.pnlDocGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(146)))), ((int)(((byte)(48)))));
            this.pnlDocGia.Controls.Add(this.lblTongDocGia);
            this.pnlDocGia.Controls.Add(this.lblTieuDeDocGia);
            this.pnlDocGia.Location = new System.Drawing.Point(265, 75);
            this.pnlDocGia.Name = "pnlDocGia";
            this.pnlDocGia.Size = new System.Drawing.Size(225, 110);
            this.pnlDocGia.TabIndex = 8;
            // 
            // lblTongDocGia
            // 
            this.lblTongDocGia.AutoSize = true;
            this.lblTongDocGia.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.lblTongDocGia.ForeColor = System.Drawing.Color.White;
            this.lblTongDocGia.Location = new System.Drawing.Point(15, 38);
            this.lblTongDocGia.Name = "lblTongDocGia";
            this.lblTongDocGia.Size = new System.Drawing.Size(91, 106);
            this.lblTongDocGia.TabIndex = 0;
            this.lblTongDocGia.Text = "0";
            // 
            // lblTieuDeDocGia
            // 
            this.lblTieuDeDocGia.AutoSize = true;
            this.lblTieuDeDocGia.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeDocGia.ForeColor = System.Drawing.Color.White;
            this.lblTieuDeDocGia.Location = new System.Drawing.Point(12, 12);
            this.lblTieuDeDocGia.Name = "lblTieuDeDocGia";
            this.lblTieuDeDocGia.Size = new System.Drawing.Size(312, 54);
            this.lblTieuDeDocGia.TabIndex = 1;
            this.lblTieuDeDocGia.Text = "TỔNG ĐỘC GIẢ";
            // 
            // pnlDangMuon
            // 
            this.pnlDangMuon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(146)))), ((int)(((byte)(48)))));
            this.pnlDangMuon.Controls.Add(this.lblDangMuon);
            this.pnlDangMuon.Controls.Add(this.lblTieuDeDangMuon);
            this.pnlDangMuon.Location = new System.Drawing.Point(510, 75);
            this.pnlDangMuon.Name = "pnlDangMuon";
            this.pnlDangMuon.Size = new System.Drawing.Size(225, 110);
            this.pnlDangMuon.TabIndex = 7;
            // 
            // lblDangMuon
            // 
            this.lblDangMuon.AutoSize = true;
            this.lblDangMuon.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.lblDangMuon.ForeColor = System.Drawing.Color.White;
            this.lblDangMuon.Location = new System.Drawing.Point(15, 38);
            this.lblDangMuon.Name = "lblDangMuon";
            this.lblDangMuon.Size = new System.Drawing.Size(91, 106);
            this.lblDangMuon.TabIndex = 0;
            this.lblDangMuon.Text = "0";
            // 
            // lblTieuDeDangMuon
            // 
            this.lblTieuDeDangMuon.AutoSize = true;
            this.lblTieuDeDangMuon.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeDangMuon.ForeColor = System.Drawing.Color.White;
            this.lblTieuDeDangMuon.Location = new System.Drawing.Point(10, 12);
            this.lblTieuDeDangMuon.Name = "lblTieuDeDangMuon";
            this.lblTieuDeDangMuon.Size = new System.Drawing.Size(401, 54);
            this.lblTieuDeDangMuon.TabIndex = 1;
            this.lblTieuDeDangMuon.Text = "SÁCH ĐANG MƯỢN";
            // 
            // pnlQuaHan
            // 
            this.pnlQuaHan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.pnlQuaHan.Controls.Add(this.lblQuaHan);
            this.pnlQuaHan.Controls.Add(this.lblTieuDeQuaHan);
            this.pnlQuaHan.Location = new System.Drawing.Point(755, 75);
            this.pnlQuaHan.Name = "pnlQuaHan";
            this.pnlQuaHan.Size = new System.Drawing.Size(225, 110);
            this.pnlQuaHan.TabIndex = 6;
            // 
            // lblQuaHan
            // 
            this.lblQuaHan.AutoSize = true;
            this.lblQuaHan.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.lblQuaHan.ForeColor = System.Drawing.Color.White;
            this.lblQuaHan.Location = new System.Drawing.Point(15, 38);
            this.lblQuaHan.Name = "lblQuaHan";
            this.lblQuaHan.Size = new System.Drawing.Size(91, 106);
            this.lblQuaHan.TabIndex = 0;
            this.lblQuaHan.Text = "0";
            // 
            // lblTieuDeQuaHan
            // 
            this.lblTieuDeQuaHan.AutoSize = true;
            this.lblTieuDeQuaHan.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeQuaHan.ForeColor = System.Drawing.Color.White;
            this.lblTieuDeQuaHan.Location = new System.Drawing.Point(12, 12);
            this.lblTieuDeQuaHan.Name = "lblTieuDeQuaHan";
            this.lblTieuDeQuaHan.Size = new System.Drawing.Size(329, 54);
            this.lblTieuDeQuaHan.TabIndex = 1;
            this.lblTieuDeQuaHan.Text = "SÁCH QUÁ HẠN";
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            this.lblChartTitle.Location = new System.Drawing.Point(20, 200);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(1526, 72);
            this.lblChartTitle.TabIndex = 5;
            this.lblChartTitle.Text = "Thống kê số lượng sách được mượn trong 7 ngày gần nhất";
            // 
            // chartThongKe
            // 
            chartArea12.AxisX.MajorGrid.Enabled = false;
            chartArea12.Name = "ChartArea1";
            this.chartThongKe.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chartThongKe.Legends.Add(legend12);
            this.chartThongKe.Location = new System.Drawing.Point(20, 240);
            this.chartThongKe.Name = "chartThongKe";
            series12.ChartArea = "ChartArea1";
            series12.Color = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            series12.Legend = "Legend1";
            series12.LegendText = "Số lượng sách mượn";
            series12.Name = "SoLuongMuon";
            this.chartThongKe.Series.Add(series12);
            this.chartThongKe.Size = new System.Drawing.Size(960, 220);
            this.chartThongKe.TabIndex = 4;
            // 
            // lblGrid1
            // 
            this.lblGrid1.AutoSize = true;
            this.lblGrid1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblGrid1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            this.lblGrid1.Location = new System.Drawing.Point(20, 480);
            this.lblGrid1.Name = "lblGrid1";
            this.lblGrid1.Size = new System.Drawing.Size(649, 65);
            this.lblGrid1.TabIndex = 3;
            this.lblGrid1.Text = "Danh sách phiếu mượn mới";
            // 
            // lblGrid2
            // 
            this.lblGrid2.AutoSize = true;
            this.lblGrid2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblGrid2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            this.lblGrid2.Location = new System.Drawing.Point(512, 480);
            this.lblGrid2.Name = "lblGrid2";
            this.lblGrid2.Size = new System.Drawing.Size(672, 65);
            this.lblGrid2.TabIndex = 1;
            this.lblGrid2.Text = "Danh sách phiếu trả quá hạn";
            // 
            // dgvPhieuMuonMoi
            // 
            this.dgvPhieuMuonMoi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuMuonMoi.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhieuMuonMoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvPhieuMuonMoi.ColumnHeadersHeight = 46;
            this.dgvPhieuMuonMoi.EnableHeadersVisualStyles = false;
            this.dgvPhieuMuonMoi.Location = new System.Drawing.Point(20, 510);
            this.dgvPhieuMuonMoi.Name = "dgvPhieuMuonMoi";
            this.dgvPhieuMuonMoi.RowHeadersWidth = 82;
            this.dgvPhieuMuonMoi.Size = new System.Drawing.Size(470, 170);
            this.dgvPhieuMuonMoi.TabIndex = 2;
            // 
            // dgvQuaHan
            // 
            this.dgvQuaHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuaHan.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(96)))));
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuaHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvQuaHan.ColumnHeadersHeight = 46;
            this.dgvQuaHan.EnableHeadersVisualStyles = false;
            this.dgvQuaHan.Location = new System.Drawing.Point(510, 510);
            this.dgvQuaHan.Name = "dgvQuaHan";
            this.dgvQuaHan.RowHeadersWidth = 82;
            this.dgvQuaHan.Size = new System.Drawing.Size(470, 170);
            this.dgvQuaHan.TabIndex = 0;
            // 
            // frmDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.dgvQuaHan);
            this.Controls.Add(this.lblGrid2);
            this.Controls.Add(this.dgvPhieuMuonMoi);
            this.Controls.Add(this.lblGrid1);
            this.Controls.Add(this.chartThongKe);
            this.Controls.Add(this.lblChartTitle);
            this.Controls.Add(this.pnlQuaHan);
            this.Controls.Add(this.pnlDangMuon);
            this.Controls.Add(this.pnlDocGia);
            this.Controls.Add(this.pnlSach);
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.pnlSach.ResumeLayout(false);
            this.pnlSach.PerformLayout();
            this.pnlDocGia.ResumeLayout(false);
            this.pnlDocGia.PerformLayout();
            this.pnlDangMuon.ResumeLayout(false);
            this.pnlDangMuon.PerformLayout();
            this.pnlQuaHan.ResumeLayout(false);
            this.pnlQuaHan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuonMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuaHan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnInBaoCao;

        private System.Windows.Forms.Panel pnlSach;
        private System.Windows.Forms.Label lblTieuDeSach;
        private System.Windows.Forms.Label lblTongSach;

        private System.Windows.Forms.Panel pnlDocGia;
        private System.Windows.Forms.Label lblTieuDeDocGia;
        private System.Windows.Forms.Label lblTongDocGia;

        private System.Windows.Forms.Panel pnlDangMuon;
        private System.Windows.Forms.Label lblTieuDeDangMuon;
        private System.Windows.Forms.Label lblDangMuon;

        private System.Windows.Forms.Panel pnlQuaHan;
        private System.Windows.Forms.Label lblTieuDeQuaHan;
        private System.Windows.Forms.Label lblQuaHan;

        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;

        private System.Windows.Forms.Label lblGrid1;
        private System.Windows.Forms.Label lblGrid2;
        private System.Windows.Forms.DataGridView dgvPhieuMuonMoi;
        private System.Windows.Forms.DataGridView dgvQuaHan;
    }
}