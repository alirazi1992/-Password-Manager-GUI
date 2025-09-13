namespace PasswordManagerGUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReveal;

        private System.Windows.Forms.DataGridView dgvAccounts;

        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWebsite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWebsite = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();

            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReveal = new System.Windows.Forms.Button();

            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWebsite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.SuspendLayout();

            // Labels
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Location = new System.Drawing.Point(20, 20);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(52, 13);
            this.lblWebsite.Text = "Website:";

            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(20, 50);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.Text = "Username:";

            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.Text = "Password:";

            // Textboxes
            this.txtWebsite.Location = new System.Drawing.Point(90, 17);
            this.txtWebsite.Size = new System.Drawing.Size(200, 20);

            this.txtUsername.Location = new System.Drawing.Point(90, 47);
            this.txtUsername.Size = new System.Drawing.Size(200, 20);

            this.txtPassword.Location = new System.Drawing.Point(90, 77);
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.UseSystemPasswordChar = true;

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(310, 15);
            this.btnAdd.Size = new System.Drawing.Size(80, 25);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Location = new System.Drawing.Point(310, 45);
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnReveal.Location = new System.Drawing.Point(310, 75);
            this.btnReveal.Size = new System.Drawing.Size(80, 25);
            this.btnReveal.Text = "Reveal";
            this.btnReveal.Click += new System.EventHandler(this.btnReveal_Click);

            // DataGridView
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colWebsite, this.colUsername
            });
            this.dgvAccounts.Location = new System.Drawing.Point(20, 120);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(500, 250);

            // DataGridView Columns
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = false;

            this.colWebsite.DataPropertyName = "Website";
            this.colWebsite.HeaderText = "Website";
            this.colWebsite.Name = "colWebsite";

            this.colUsername.DataPropertyName = "Username";
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";

            // Form1
            this.ClientSize = new System.Drawing.Size(550, 400);
            this.Controls.Add(this.lblWebsite);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReveal);
            this.Controls.Add(this.dgvAccounts);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Manager (Day 21)";

            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
