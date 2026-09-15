# Pengelola Data Mahasiswa

Aplikasi desktop (WinForms) untuk mengelola data mahasiswa: No. Induk, Nama
Lengkap, Program Studi, dan IPK. Data disimpan di SQLite lokal
(`data_mahasiswa.db`).

## Arsitektur

- `Model/DataMahasiswa.cs` — kelas data mahasiswa.
- `Data/IRepositoriMahasiswa.cs` — kontrak akses data (repository pattern).
- `Data/RepositoriMahasiswaSqlite.cs` — implementasi repository berbasis SQLite,
  seluruhnya asynchronous.
- `UI/FormPengelolaMahasiswa.cs` — form utama, dibangun dengan `TableLayoutPanel`
  supaya layout tetap rapi saat window di-resize.
- `Program.cs` — entry point, melakukan dependency injection sederhana
  (repository disuntikkan ke form, bukan dipanggil statis dari dalam UI).

## Fitur

- Tambah data mahasiswa baru, dengan validasi format No. Induk memakai Regex.
- **Perbarui (edit)** data mahasiswa yang sudah ada — fitur ini tidak ada di
  versi sebelumnya.
- Cari berdasarkan No. Induk.
- Hapus data dengan konfirmasi.
- Klik baris pada tabel untuk otomatis mengisi formulir (mempermudah edit).

## Menjalankan

Butuh Windows dan .NET 8 SDK.

```bash
dotnet run
```
