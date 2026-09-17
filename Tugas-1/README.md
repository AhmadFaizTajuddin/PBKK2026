# Sistem Mahasiswa (C# / .NET)

Repositori ini berisi dua program C# yang saling berkaitan sebagai latihan
bertahap: **SapaanKonsol** (program konsol sederhana) dan
**PengelolaMahasiswa** (aplikasi desktop CRUD untuk data mahasiswa dengan
penyimpanan SQLite).

> **Catatan tentang gambar di README ini:** kedua proyek memakai .NET 8 SDK
> dan proyek `PengelolaMahasiswa` memakai WinForms yang hanya berjalan di
> Windows. Lingkungan yang dipakai untuk menyusun dokumen ini adalah
> container Linux tanpa .NET SDK dan tanpa tampilan grafis, sehingga kode
> tidak bisa benar-benar dieksekusi di sini. Gambar-gambar di bawah adalah
> **ilustrasi/mockup** yang dibuat menyerupai tampilan asli program
> berdasarkan pembacaan kode sumbernya (teks tombol, label, tata letak, dan
> alur data mengikuti kode persis) — bukan hasil screenshot sungguhan dari
> proses `dotnet run`. Jalankan sendiri di komputer dengan .NET SDK terpasang
> untuk melihat tampilan aslinya.

---

## 1. SapaanKonsol

Program konsol interaktif sederhana — variasi dari contoh klasik
"Hello, World!".

**Struktur file**
```
SapaanKonsol/
├── Program.cs
├── SapaanKonsol.csproj
└── README.md
```

**Cara kerja (`Program.cs`)**
1. Menampilkan prompt `Siapa nama Anda?` dan membaca input dari `Console.ReadLine()`.
2. Jika input kosong/hanya spasi (`string.IsNullOrWhiteSpace`), nama otomatis
   diganti menjadi `"Sahabat"` agar sapaan tetap ramah.
3. Mencetak sapaan personal: `Halo, {nama}! Selamat datang di program latihan C#.`

**Menjalankan**
```bash
cd SapaanKonsol
dotnet run
```

**Ilustrasi hasil eksekusi**

<img width="900" height="320" alt="sapaankonsol_output" src="https://github.com/user-attachments/assets/cfde28a7-f126-4358-8792-e5c710e505a0" />

Contoh di atas menunjukkan dua kali percobaan: sekali dengan nama diisi
(`Rangga`), sekali dengan input dikosongkan sehingga program memakai nilai
default `Sahabat`.

---

## 2. PengelolaMahasiswa

Aplikasi desktop (WinForms, .NET 8) untuk mengelola data mahasiswa: **No.
Induk, Nama Lengkap, Program Studi, dan IPK**. Data disimpan secara lokal di
berkas SQLite `data_mahasiswa.db` yang dibuat otomatis saat aplikasi pertama
kali dijalankan.

**Struktur file**
```
PengelolaMahasiswa/
├── Program.cs                          # Entry point
├── PengelolaMahasiswa.csproj
├── Model/
│   └── DataMahasiswa.cs                # Model data mahasiswa
├── Data/
│   ├── IRepositoriMahasiswa.cs         # Kontrak akses data (interface)
│   └── RepositoriMahasiswaSqlite.cs    # Implementasi repository ber-SQLite
└── UI/
    └── FormPengelolaMahasiswa.cs       # Form utama (tampilan & interaksi)
```

### Arsitektur

Proyek ini mengikuti pola **Repository**, memisahkan logika akses data dari
tampilan:

- **`Model/DataMahasiswa.cs`** — kelas POCO yang merepresentasikan satu
  catatan mahasiswa: `NoInduk`, `NamaLengkap`, `ProgramStudi`, `Ipk`.
- **`Data/IRepositoriMahasiswa.cs`** — antarmuka (interface) yang
  mendefinisikan operasi CRUD (`SiapkanAsync`, `SemuaAsync`,
  `CariBerdasarkanNoIndukAsync`, `SimpanAsync`, `PerbaruiAsync`,
  `HapusAsync`) sehingga implementasi penyimpanan dapat ditukar (SQLite,
  in-memory, dll.) tanpa mengubah kode UI.
- **`Data/RepositoriMahasiswaSqlite.cs`** — implementasi konkret berbasis
  `Microsoft.Data.Sqlite`. Semua query memakai parameter (`$noInduk`,
  `$nama`, dst.) untuk mencegah SQL injection, dan seluruh operasi bersifat
  asynchronous (`async`/`await`).
- **`UI/FormPengelolaMahasiswa.cs`** — form utama yang dibangun secara
  programatik (bukan Designer) memakai `TableLayoutPanel` dan
  `FlowLayoutPanel` agar layout tetap rapi saat window di-*resize*.
- **`Program.cs`** — entry point yang melakukan *dependency injection*
  sederhana: instance `RepositoriMahasiswaSqlite` dibuat lalu disuntikkan ke
  `FormPengelolaMahasiswa` melalui konstruktor, bukan dipanggil statis dari
  dalam UI. Ini memudahkan pengujian/penggantian sumber data di kemudian hari.

### Fitur & alur kerja

| Bagian UI | Fungsi |
|---|---|
| Kolom form (No. Induk, Nama Lengkap, Program Studi, IPK) | Input data mahasiswa |
| Tombol **Simpan Baru** | Memvalidasi lalu menyimpan data baru ke SQLite |
| Tombol **Perbarui** | Memperbarui data mahasiswa yang No. Induk-nya sudah ada |
| Kolom **Cari No. Induk** + tombol **Cari** | Mencari satu data berdasarkan No. Induk |
| Tombol **Hapus** | Menghapus data (dengan dialog konfirmasi Ya/Tidak) |
| Tombol **Tampilkan Semua** | Memuat ulang seluruh data, diurutkan berdasarkan nama |
| Tabel data (`DataGridView`) | Menampilkan seluruh data; klik satu baris otomatis mengisi form untuk diedit |

**Validasi input** (`ValidasiFormulir`):
- No. Induk wajib **5–15 karakter alfanumerik**, dicek dengan Regex
  `^[A-Za-z0-9]{5,15}$`.
- Nama Lengkap dan Program Studi tidak boleh kosong.
- IPK dibatasi antara 0–4 melalui kontrol `NumericUpDown` (2 angka desimal).

Jika validasi gagal, atau operasi ke database gagal/tidak menemukan data,
aplikasi menampilkan `MessageBox` peringatan atau informasi yang sesuai.

**Menjalankan**

Butuh **Windows** dan **.NET 8 SDK** (karena memakai WinForms).

```bash
cd PengelolaMahasiswa
dotnet run
```

### Ilustrasi tampilan aplikasi

<img width="1000" height="660" alt="pengelolamahasiswa_output" src="https://github.com/user-attachments/assets/cf5dd60d-1b0b-43d9-9b8b-9cc053380cf3" />

Ilustrasi di atas menggambarkan kondisi setelah aplikasi dijalankan dan diisi
beberapa data contoh:
- Bagian atas: form input (No. Induk, Nama Lengkap, Program Studi, IPK) beserta
  tombol **Simpan Baru** dan **Perbarui**.
- Bagian tengah: kolom pencarian No. Induk beserta tombol **Cari**, **Hapus**,
  dan **Tampilkan Semua**.
- Bagian bawah: tabel (`DataGridView`) berisi daftar mahasiswa yang tersimpan
  di `data_mahasiswa.db`, diurutkan berdasarkan nama.

---

## Ringkasan perbandingan

| | SapaanKonsol | PengelolaMahasiswa |
|---|---|---|
| Jenis aplikasi | Console App | Desktop App (WinForms) |
| Target framework | `net8.0` | `net8.0-windows` |
| Penyimpanan data | Tidak ada (interaksi sekali jalan) | SQLite (`data_mahasiswa.db`) |
| Kompleksitas | Sangat sederhana, 1 file | Berlapis: Model, Data (Repository), UI |
| Tujuan | Latihan dasar input/output C# | Latihan CRUD, pola Repository, async/await, validasi Regex |
