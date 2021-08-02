
namespace RedmineTool.UI
{
    partial class FrmAddNewIssueDialog
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
            this.cmbTracker = new System.Windows.Forms.ComboBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtDesciption = new System.Windows.Forms.TextBox();
            this.txtEstimatedDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEstimatedHour = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbAssignee = new System.Windows.Forms.ComboBox();
            this.lblUpperIssueName = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUpperIssueId = new System.Windows.Forms.TextBox();
            this.lblProjectPath = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProjects = new System.Windows.Forms.ComboBox();
            this.chkListWatchers = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTracker
            // 
            this.cmbTracker.FormattingEnabled = true;
            this.cmbTracker.Location = new System.Drawing.Point(11, 139);
            this.cmbTracker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTracker.Name = "cmbTracker";
            this.cmbTracker.Size = new System.Drawing.Size(204, 25);
            this.cmbTracker.TabIndex = 11;
            // 
            // txtSubject
            // 
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubject.Location = new System.Drawing.Point(53, 4);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(507, 25);
            this.txtSubject.TabIndex = 1;
            // 
            // txtDesciption
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtDesciption, 2);
            this.txtDesciption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesciption.Location = new System.Drawing.Point(3, 38);
            this.txtDesciption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDesciption.Multiline = true;
            this.txtDesciption.Name = "txtDesciption";
            this.txtDesciption.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDesciption.Size = new System.Drawing.Size(557, 447);
            this.txtDesciption.TabIndex = 2;
            // 
            // txtEstimatedDate
            // 
            this.txtEstimatedDate.Location = new System.Drawing.Point(452, 11);
            this.txtEstimatedDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEstimatedDate.Name = "txtEstimatedDate";
            this.txtEstimatedDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEstimatedDate.Size = new System.Drawing.Size(33, 25);
            this.txtEstimatedDate.TabIndex = 6;
            this.txtEstimatedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEstimatedDate.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "일";
            // 
            // txtEstimatedHour
            // 
            this.txtEstimatedHour.Location = new System.Drawing.Point(509, 10);
            this.txtEstimatedHour.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEstimatedHour.Name = "txtEstimatedHour";
            this.txtEstimatedHour.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEstimatedHour.Size = new System.Drawing.Size(30, 25);
            this.txtEstimatedHour.TabIndex = 7;
            this.txtEstimatedHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEstimatedHour.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(540, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "시간";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblProjectPath);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.cmbProjects);
            this.splitContainer1.Panel2.Controls.Add(this.chkListWatchers);
            this.splitContainer1.Panel2.Controls.Add(this.cmbTracker);
            this.splitContainer1.Size = new System.Drawing.Size(804, 643);
            this.splitContainer1.SplitterDistance = 573;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cmbAssignee);
            this.splitContainer2.Panel2.Controls.Add(this.lblUpperIssueName);
            this.splitContainer2.Panel2.Controls.Add(this.btnBrowse);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.txtEstimatedHour);
            this.splitContainer2.Panel2.Controls.Add(this.txtEstimatedDate);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.txtUpperIssueId);
            this.splitContainer2.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.splitContainer2.Size = new System.Drawing.Size(573, 643);
            this.splitContainer2.SplitterDistance = 544;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 513F));
            this.tableLayoutPanel1.Controls.Add(this.txtDesciption, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtSubject, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(563, 534);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 39);
            this.panel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(169, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 34);
            this.label9.TabIndex = 4;
            this.label9.Text = "제목";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbAssignee
            // 
            this.cmbAssignee.FormattingEnabled = true;
            this.cmbAssignee.Location = new System.Drawing.Point(421, 49);
            this.cmbAssignee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAssignee.Name = "cmbAssignee";
            this.cmbAssignee.Size = new System.Drawing.Size(149, 25);
            this.cmbAssignee.TabIndex = 8;
            // 
            // lblUpperIssueName
            // 
            this.lblUpperIssueName.AutoSize = true;
            this.lblUpperIssueName.Location = new System.Drawing.Point(9, 52);
            this.lblUpperIssueName.Name = "lblUpperIssueName";
            this.lblUpperIssueName.Size = new System.Drawing.Size(34, 17);
            this.lblUpperIssueName.TabIndex = 6;
            this.lblUpperIssueName.Text = "없음";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(172, 8);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(43, 33);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "탐색";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "담당자";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(357, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "예상소요시간";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "상위 작업";
            // 
            // txtUpperIssueId
            // 
            this.txtUpperIssueId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUpperIssueId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUpperIssueId.Location = new System.Drawing.Point(86, 10);
            this.txtUpperIssueId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUpperIssueId.Name = "txtUpperIssueId";
            this.txtUpperIssueId.Size = new System.Drawing.Size(80, 25);
            this.txtUpperIssueId.TabIndex = 5;
            this.txtUpperIssueId.TextChanged += new System.EventHandler(this.txtUpperIssueId_TextChanged);
            this.txtUpperIssueId.Enter += new System.EventHandler(this.txtUpperIssueId_Enter);
            // 
            // lblProjectPath
            // 
            this.lblProjectPath.AutoSize = true;
            this.lblProjectPath.Location = new System.Drawing.Point(12, 42);
            this.lblProjectPath.Name = "lblProjectPath";
            this.lblProjectPath.Size = new System.Drawing.Size(13, 17);
            this.lblProjectPath.TabIndex = 4;
            this.lblProjectPath.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "프로젝트";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "일감 관람자";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "유형";
            // 
            // cmbProjects
            // 
            this.cmbProjects.FormattingEnabled = true;
            this.cmbProjects.Location = new System.Drawing.Point(11, 67);
            this.cmbProjects.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.Size = new System.Drawing.Size(204, 25);
            this.cmbProjects.TabIndex = 10;
            // 
            // chkListWatchers
            // 
            this.chkListWatchers.CheckOnClick = true;
            this.chkListWatchers.FormattingEnabled = true;
            this.chkListWatchers.Location = new System.Drawing.Point(11, 203);
            this.chkListWatchers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkListWatchers.Name = "chkListWatchers";
            this.chkListWatchers.Size = new System.Drawing.Size(204, 424);
            this.chkListWatchers.TabIndex = 0;
            // 
            // FrmAddNewIssueDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 653);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(830, 692);
            this.Name = "FrmAddNewIssueDialog";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.Text = "새 이슈";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTracker;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtDesciption;
        private System.Windows.Forms.TextBox txtEstimatedDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEstimatedHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbAssignee;
        private System.Windows.Forms.Label lblUpperIssueName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUpperIssueId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProjects;
        private System.Windows.Forms.CheckedListBox chkListWatchers;
        private System.Windows.Forms.Label lblProjectPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
    }
}