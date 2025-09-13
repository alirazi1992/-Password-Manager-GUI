# 🔐 Password Manager GUI (C# WinForms + SQLite)

**Day 21** of my **30-Day C# Project-Based Learning Plan**  
A simple desktop **Password Manager** built with **WinForms** and **SQLite**, focusing on database usage, encryption, and solving platform-specific build issues.

---

## 🚀 Features
- ➕ Add accounts (Website, Username, Password)
- ❌ Delete selected accounts
- 👀 Reveal stored passwords (AES-encrypted in DB)
- 📋 Display Website + Username in a DataGridView
- 💾 SQLite persistence (`passwords.db` auto-created if missing)

---

## 🛠 Tech Stack
- **C# (.NET Framework 4.7.2)**
- **WinForms** (UI)
- **SQLite** via `Microsoft.Data.Sqlite`
- **AES Encryption** with `System.Security.Cryptography`

---

## 🧩 Challenges Faced
- ❌ Encountered error: *“This package does not support Any CPU builds”*
- Learned that **SQLite native DLLs require platform-specific builds** (`x64` or `x86`)
- Fixed by aligning both **Solution** and **Project** to `x64` in Visual Studio Configuration Manager
- Added `SQLitePCLRaw.bundle_e_sqlite3` and initialized with `Batteries_V2.Init();` in `Program.cs`

💡 **Key Learning:** Debugging build/runtime environment issues is as valuable as writing logic.

---

## 📸 Screenshots

| 🔐 | 
|------|
| ![Main](./PasswordManager.png ) |

---

## 📚 Learning Goals

- Practice CRUD with SQLite in WinForms

- Use AES encryption for password security

- Debug platform & build issues (Any CPU vs x64)

- Strengthen problem-solving skills in C#
