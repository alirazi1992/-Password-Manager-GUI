using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace PasswordManagerGUI
{
    public partial class Form1 : Form
    {
        private string _dbPath;
        private string _connectionString;

        // ⚠ For demo only. In a real app, derive/store a key securely.
        private static readonly string Key = "MySuperSecretKey123";

        public Form1()
        {
            InitializeComponent();

            _dbPath = Path.Combine(AppContext.BaseDirectory, "passwords.db");
            _connectionString = "Data Source=" + _dbPath;

            try
            {
                EnsureDatabase();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup error:\n" + ex, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- DB bootstrap ----------

        private void EnsureDatabase()
        {
            if (!File.Exists(_dbPath))
                using (File.Create(_dbPath)) { }

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                const string sql = @"
CREATE TABLE IF NOT EXISTS Accounts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Website TEXT NOT NULL,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL
);";
                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ---------- UI events ----------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var website = (txtWebsite.Text ?? "").Trim();
                var username = (txtUsername.Text ?? "").Trim();
                var password = (txtPassword.Text ?? "").Trim();

                if (website.Length == 0 || username.Length == 0 || password.Length == 0)
                {
                    MessageBox.Show("Please fill Website, Username, and Password.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var encrypted = Encrypt(password);

                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    const string sql = "INSERT INTO Accounts (Website, Username, Password) VALUES ($w,$u,$p)";
                    using (var cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("$w", website);
                        cmd.Parameters.AddWithValue("$u", username);
                        cmd.Parameters.AddWithValue("$p", encrypted);
                        cmd.ExecuteNonQuery();
                    }
                }

                ClearInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add failed:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccounts.CurrentRow == null)
                {
                    MessageBox.Show("Select a row first.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // NOTE: our Designer column is named "colId" (Id is DataPropertyName)
                object idObj = dgvAccounts.CurrentRow.Cells["colId"].Value;
                if (idObj == null || idObj == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Id.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(idObj);

                if (MessageBox.Show("Delete this account?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    const string sql = "DELETE FROM Accounts WHERE Id=$id";
                    using (var cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("$id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReveal_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccounts.CurrentRow == null)
                {
                    MessageBox.Show("Select a row first.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                object idObj = dgvAccounts.CurrentRow.Cells["colId"].Value;
                if (idObj == null || idObj == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Id.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(idObj);

                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    const string sql = "SELECT Password FROM Accounts WHERE Id=$id";
                    using (var cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("$id", id);
                        var encrypted = cmd.ExecuteScalar() as string;
                        if (!string.IsNullOrEmpty(encrypted))
                        {
                            string decrypted = Decrypt(encrypted);
                            MessageBox.Show("Password: " + decrypted, "Reveal",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No password stored.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reveal failed:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Data binding ----------

        private void LoadData()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                const string sql = "SELECT Id, Website, Username FROM Accounts";
                using (var cmd = new SqliteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    dgvAccounts.AutoGenerateColumns = false; // we use explicit columns from Designer
                    dgvAccounts.DataSource = dt;
                }
            }
        }

        // ---------- Helpers ----------

        private void ClearInputs()
        {
            txtWebsite.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtWebsite.Focus();
        }

        // Simple AES (demo)
        private string Encrypt(string plainText)
        {
            if (plainText == null) plainText = "";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16]; // zero IV for demo simplicity (not recommended for production)

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] input = Encoding.UTF8.GetBytes(plainText);
                    byte[] encrypted = encryptor.TransformFinalBlock(input, 0, input.Length);
                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        private string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] input = Convert.FromBase64String(cipherText);
                    byte[] decrypted = decryptor.TransformFinalBlock(input, 0, input.Length);
                    return Encoding.UTF8.GetString(decrypted);
                }
            }
        }
    }
}
