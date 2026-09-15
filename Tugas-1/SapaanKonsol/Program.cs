// Program konsol sederhana yang menyapa pengguna berdasarkan input nama.
// Berbeda dari contoh "Hello, World!" statis: program ini interaktif.

Console.Write("Siapa nama Anda? ");
string? nama = Console.ReadLine();

if (string.IsNullOrWhiteSpace(nama))
{
    nama = "Sahabat";
}

Console.WriteLine($"Halo, {nama}! Selamat datang di program latihan C#.");
