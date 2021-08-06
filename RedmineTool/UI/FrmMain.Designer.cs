
namespace RedmineTool.UI
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnNewIssue = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnRebuildDueDate = new System.Windows.Forms.Button();
            this.btnRefreshIssues = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 22);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(839, 435);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnNewIssue
            // 
            this.btnNewIssue.Location = new System.Drawing.Point(17, 75);
            this.btnNewIssue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNewIssue.Name = "btnNewIssue";
            this.btnNewIssue.Size = new System.Drawing.Size(75, 33);
            this.btnNewIssue.TabIndex = 2;
            this.btnNewIssue.Text = "새 이슈";
            this.btnNewIssue.UseVisualStyleBackColor = true;
            this.btnNewIssue.Click += new System.EventHandler(this.btnNewIssue_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnRebuildDueDate
            // 
            this.btnRebuildDueDate.Location = new System.Drawing.Point(17, 277);
            this.btnRebuildDueDate.Name = "btnRebuildDueDate";
            this.btnRebuildDueDate.Size = new System.Drawing.Size(75, 44);
            this.btnRebuildDueDate.TabIndex = 3;
            this.btnRebuildDueDate.Text = "완료기한 Rebuilder";
            this.btnRebuildDueDate.UseVisualStyleBackColor = true;
            this.btnRebuildDueDate.Click += new System.EventHandler(this.btnRebuildDueDate_Click);
            // 
            // btnRefreshIssues
            // 
            this.btnRefreshIssues.Location = new System.Drawing.Point(17, 135);
            this.btnRefreshIssues.Name = "btnRefreshIssues";
            this.btnRefreshIssues.Size = new System.Drawing.Size(75, 30);
            this.btnRefreshIssues.TabIndex = 4;
            this.btnRefreshIssues.Text = "Refresh";
            this.btnRefreshIssues.UseVisualStyleBackColor = true;
            this.btnRefreshIssues.Click += new System.EventHandler(this.btnRefreshIssues_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefreshIssues);
            this.splitContainer1.Panel1.Controls.Add(this.btnRebuildDueDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnNewIssue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(968, 435);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 5;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 445);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnNewIssue;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnRebuildDueDate;
        private System.Windows.Forms.Button btnRefreshIssues;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

