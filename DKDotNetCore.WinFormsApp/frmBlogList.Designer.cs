namespace DKDotNetCore.WinFormsApp
{
    partial class frmBlogList
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
            dgvData = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            ColEdit = new DataGridViewButtonColumn();
            ColDelete = new DataGridViewButtonColumn();
            colTitle = new DataGridViewTextBoxColumn();
            ColAuthor = new DataGridViewTextBoxColumn();
            ColContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { id, ColEdit, ColDelete, colTitle, ColAuthor, ColContent });
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 0);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowTemplate.Height = 25;
            dgvData.Size = new Size(800, 450);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            // 
            // id
            // 
            id.DataPropertyName = "BlogId";
            id.HeaderText = "ID";
            id.Name = "id";
            id.ReadOnly = true;
            // 
            // ColEdit
            // 
            ColEdit.HeaderText = "Edit";
            ColEdit.Name = "ColEdit";
            ColEdit.ReadOnly = true;
            ColEdit.Text = "Edit";
            ColEdit.UseColumnTextForButtonValue = true;
            // 
            // ColDelete
            // 
            ColDelete.HeaderText = "Delete";
            ColDelete.Name = "ColDelete";
            ColDelete.ReadOnly = true;
            ColDelete.Text = "Delete";
            ColDelete.UseColumnTextForButtonValue = true;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "BlogTitle";
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // ColAuthor
            // 
            ColAuthor.DataPropertyName = "BlogAuthor";
            ColAuthor.HeaderText = "Author";
            ColAuthor.Name = "ColAuthor";
            ColAuthor.ReadOnly = true;
            // 
            // ColContent
            // 
            ColContent.DataPropertyName = "BlogContent";
            ColContent.HeaderText = "Content";
            ColContent.Name = "ColContent";
            ColContent.ReadOnly = true;
            // 
            // frmBlogList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvData);
            Name = "frmBlogList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blogs";
            Load += frmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private DataGridViewTextBoxColumn id;
        private DataGridViewButtonColumn ColEdit;
        private DataGridViewButtonColumn ColDelete;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn ColAuthor;
        private DataGridViewTextBoxColumn ColContent;
    }
}