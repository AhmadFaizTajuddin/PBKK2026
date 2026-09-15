using PengelolaMahasiswa.Model;

namespace PengelolaMahasiswa.Data;

/// <summary>
/// Kontrak akses data mahasiswa, supaya implementasi penyimpanan
/// (SQLite, file, in-memory, dll) bisa ditukar tanpa mengubah UI.
/// </summary>
public interface IRepositoriMahasiswa
{
    Task SiapkanAsync();
    Task<List<DataMahasiswa>> SemuaAsync();
    Task<DataMahasiswa?> CariBerdasarkanNoIndukAsync(string noInduk);
    Task SimpanAsync(DataMahasiswa data);
    Task<bool> PerbaruiAsync(DataMahasiswa data);
    Task<bool> HapusAsync(string noInduk);
}
