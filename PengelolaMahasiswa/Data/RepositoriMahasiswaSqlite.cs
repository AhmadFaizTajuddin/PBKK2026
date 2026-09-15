using Microsoft.Data.Sqlite;
using PengelolaMahasiswa.Model;

namespace PengelolaMahasiswa.Data;

/// <summary>
/// Implementasi IRepositoriMahasiswa berbasis SQLite.
/// Semua operasi ditulis secara asynchronous.
/// </summary>
public class RepositoriMahasiswaSqlite : IRepositoriMahasiswa
{
    private readonly string _connectionString;

    public RepositoriMahasiswaSqlite(string namaBerkas = "data_mahasiswa.db")
    {
        _connectionString = $"Data Source={namaBerkas}";
    }

    private SqliteConnection BukaKoneksi()
    {
        var koneksi = new SqliteConnection(_connectionString);
        koneksi.Open();
        return koneksi;
    }

    public async Task SiapkanAsync()
    {
        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = @"
            CREATE TABLE IF NOT EXISTS mahasiswa (
                no_induk TEXT PRIMARY KEY,
                nama_lengkap TEXT NOT NULL,
                program_studi TEXT NOT NULL,
                ipk REAL NOT NULL
            );";
        await perintah.ExecuteNonQueryAsync();
    }

    public async Task<List<DataMahasiswa>> SemuaAsync()
    {
        var daftar = new List<DataMahasiswa>();

        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = "SELECT no_induk, nama_lengkap, program_studi, ipk FROM mahasiswa ORDER BY nama_lengkap;";

        using var pembaca = await perintah.ExecuteReaderAsync();
        while (await pembaca.ReadAsync())
        {
            daftar.Add(BacaBaris(pembaca));
        }

        return daftar;
    }

    public async Task<DataMahasiswa?> CariBerdasarkanNoIndukAsync(string noInduk)
    {
        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = @"
            SELECT no_induk, nama_lengkap, program_studi, ipk FROM mahasiswa
            WHERE no_induk = $noInduk COLLATE NOCASE
            LIMIT 1;";
        perintah.Parameters.AddWithValue("$noInduk", noInduk);

        using var pembaca = await perintah.ExecuteReaderAsync();
        return await pembaca.ReadAsync() ? BacaBaris(pembaca) : null;
    }

    public async Task SimpanAsync(DataMahasiswa data)
    {
        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = @"
            INSERT INTO mahasiswa (no_induk, nama_lengkap, program_studi, ipk)
            VALUES ($noInduk, $nama, $prodi, $ipk);";
        IsiParameter(perintah, data);
        await perintah.ExecuteNonQueryAsync();
    }

    public async Task<bool> PerbaruiAsync(DataMahasiswa data)
    {
        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = @"
            UPDATE mahasiswa
            SET nama_lengkap = $nama, program_studi = $prodi, ipk = $ipk
            WHERE no_induk = $noInduk COLLATE NOCASE;";
        IsiParameter(perintah, data);
        return await perintah.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> HapusAsync(string noInduk)
    {
        using var koneksi = BukaKoneksi();
        using var perintah = koneksi.CreateCommand();
        perintah.CommandText = "DELETE FROM mahasiswa WHERE no_induk = $noInduk COLLATE NOCASE;";
        perintah.Parameters.AddWithValue("$noInduk", noInduk);
        return await perintah.ExecuteNonQueryAsync() > 0;
    }

    private static void IsiParameter(SqliteCommand perintah, DataMahasiswa data)
    {
        perintah.Parameters.AddWithValue("$noInduk", data.NoInduk);
        perintah.Parameters.AddWithValue("$nama", data.NamaLengkap);
        perintah.Parameters.AddWithValue("$prodi", data.ProgramStudi);
        perintah.Parameters.AddWithValue("$ipk", data.Ipk);
    }

    private static DataMahasiswa BacaBaris(SqliteDataReader pembaca) => new()
    {
        NoInduk = pembaca.GetString(0),
        NamaLengkap = pembaca.GetString(1),
        ProgramStudi = pembaca.GetString(2),
        Ipk = pembaca.GetDouble(3)
    };
}
