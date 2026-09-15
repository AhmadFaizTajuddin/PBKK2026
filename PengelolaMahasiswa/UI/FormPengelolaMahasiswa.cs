using System.Text.RegularExpressions;
using PengelolaMahasiswa.Data;
using PengelolaMahasiswa.Model;

namespace PengelolaMahasiswa.UI;

public partial class FormPengelolaMahasiswa : Form
{
    private readonly IRepositoriMahasiswa _repositori;

    private TextBox _kotakNoInduk = default!;
    private TextBox _kotakNama = default!;
    private TextBox _kotakProdi = default!;
    private NumericUpDown _angkaIpk = default!;
    private TextBox _kotakPencarian = default!;
    private DataGridView _tabelData = default!;

    // Regex sederhana: huruf/angka, 5-15 karakter, dipakai untuk validasi NoInduk.
    private static readonly Regex PolaNoInduk = new(@"^[A-Za-z0-9]{5,15}$", RegexOptions.Compiled);

    public FormPengelolaMahasiswa(IRepositoriMahasiswa repositori)
    {
        _repositori = repositori;
        SusunTampilan();
        Load += async (_, _) =>
        {
            await _repositori.SiapkanAsync();
            await MuatUlangTabelAsync();
        };
    }

    private void SusunTampilan()
    {
        Text = "Pengelola Data Mahasiswa";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(920, 580);
        Font = new Font("Segoe UI", 9.5F);
        MinimumSize = new Size(760, 480);

        var panelFormulir = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 5,
            RowCount = 2,
            Height = 120,
            Padding = new Padding(10),
            AutoSize = true
        };
        for (int i = 0; i < 5; i++)
        {
            panelFormulir.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        }

        panelFormulir.Controls.Add(BuatLabel("No. Induk"), 0, 0);
        panelFormulir.Controls.Add(BuatLabel("Nama Lengkap"), 1, 0);
        panelFormulir.Controls.Add(BuatLabel("Program Studi"), 2, 0);
        panelFormulir.Controls.Add(BuatLabel("IPK"), 3, 0);

        _kotakNoInduk = BuatKotakTeks();
        _kotakNama = BuatKotakTeks();
        _kotakProdi = BuatKotakTeks();
        _angkaIpk = new NumericUpDown
        {
            DecimalPlaces = 2,
            Minimum = 0,
            Maximum = 4,
            Increment = 0.01M,
            Dock = DockStyle.Fill,
            Margin = new Padding(4)
        };

        panelFormulir.Controls.Add(_kotakNoInduk, 0, 1);
        panelFormulir.Controls.Add(_kotakNama, 1, 1);
        panelFormulir.Controls.Add(_kotakProdi, 2, 1);
        panelFormulir.Controls.Add(_angkaIpk, 3, 1);

        var panelTombolAksi = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = true
        };
        var tombolSimpan = BuatTombol("Simpan Baru", Color.FromArgb(56, 142, 60));
        tombolSimpan.Click += async (_, _) => await SimpanBaruAsync();
        var tombolPerbarui = BuatTombol("Perbarui", Color.FromArgb(2, 119, 189));
        tombolPerbarui.Click += async (_, _) => await PerbaruiDataAsync();
        panelTombolAksi.Controls.Add(tombolSimpan);
        panelTombolAksi.Controls.Add(tombolPerbarui);
        panelFormulir.Controls.Add(panelTombolAksi, 4, 0);
        panelFormulir.SetRowSpan(panelTombolAksi, 2);

        var panelPencarian = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 50,
            Padding = new Padding(10, 8, 10, 8)
        };
        panelPencarian.Controls.Add(BuatLabel("Cari No. Induk:"));
        _kotakPencarian = new TextBox { Width = 220, Margin = new Padding(6, 4, 6, 0) };
        panelPencarian.Controls.Add(_kotakPencarian);

        var tombolCari = BuatTombol("Cari", Color.FromArgb(96, 96, 96));
        tombolCari.Click += async (_, _) => await CariAsync();
        panelPencarian.Controls.Add(tombolCari);

        var tombolHapus = BuatTombol("Hapus", Color.FromArgb(211, 47, 47));
        tombolHapus.Click += async (_, _) => await HapusAsync();
        panelPencarian.Controls.Add(tombolHapus);

        var tombolTampilkanSemua = BuatTombol("Tampilkan Semua", Color.FromArgb(96, 96, 96));
        tombolTampilkanSemua.Click += async (_, _) => await MuatUlangTabelAsync();
        panelPencarian.Controls.Add(tombolTampilkanSemua);

        _tabelData = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            BorderStyle = BorderStyle.None,
            BackgroundColor = Color.White
        };
        _tabelData.SelectionChanged += (_, _) => SalinBarisTerpilihKeFormulir();

        Controls.Add(_tabelData);
        Controls.Add(panelPencarian);
        Controls.Add(panelFormulir);
    }

    private static Label BuatLabel(string teks) => new()
    {
        Text = teks,
        AutoSize = true,
        Margin = new Padding(4, 6, 4, 0)
    };

    private static TextBox BuatKotakTeks() => new()
    {
        Dock = DockStyle.Fill,
        Margin = new Padding(4)
    };

    private static Button BuatTombol(string teks, Color warna) => new()
    {
        Text = teks,
        BackColor = warna,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        AutoSize = true,
        Margin = new Padding(4, 2, 0, 2),
        Padding = new Padding(10, 6, 10, 6)
    };

    private bool ValidasiFormulir(out DataMahasiswa data)
    {
        data = new DataMahasiswa();
        string noInduk = _kotakNoInduk.Text.Trim();
        string nama = _kotakNama.Text.Trim();
        string prodi = _kotakProdi.Text.Trim();

        if (!PolaNoInduk.IsMatch(noInduk))
        {
            Peringatkan("No. Induk harus 5-15 karakter alfanumerik.");
            return false;
        }
        if (nama.Length == 0 || prodi.Length == 0)
        {
            Peringatkan("Nama Lengkap dan Program Studi wajib diisi.");
            return false;
        }

        data = new DataMahasiswa
        {
            NoInduk = noInduk,
            NamaLengkap = nama,
            ProgramStudi = prodi,
            Ipk = (double)_angkaIpk.Value
        };
        return true;
    }

    private async Task SimpanBaruAsync()
    {
        if (!ValidasiFormulir(out var data)) return;

        try
        {
            await _repositori.SimpanAsync(data);
            Informasikan("Mahasiswa baru berhasil disimpan.");
            BersihkanFormulir();
            await MuatUlangTabelAsync();
        }
        catch (Exception ex)
        {
            Peringatkan("Gagal menyimpan: " + ex.Message);
        }
    }

    private async Task PerbaruiDataAsync()
    {
        if (!ValidasiFormulir(out var data)) return;

        bool berhasil = await _repositori.PerbaruiAsync(data);
        if (berhasil)
        {
            Informasikan("Data mahasiswa berhasil diperbarui.");
            await MuatUlangTabelAsync();
        }
        else
        {
            Peringatkan("No. Induk tidak ditemukan, tidak ada yang diperbarui.");
        }
    }

    private async Task CariAsync()
    {
        string noInduk = _kotakPencarian.Text.Trim();
        if (noInduk.Length == 0)
        {
            await MuatUlangTabelAsync();
            return;
        }

        var hasil = await _repositori.CariBerdasarkanNoIndukAsync(noInduk);
        _tabelData.DataSource = hasil is null
            ? new List<DataMahasiswa>()
            : new List<DataMahasiswa> { hasil };

        if (hasil is null)
        {
            Informasikan("Data dengan No. Induk tersebut tidak ditemukan.");
        }
    }

    private async Task HapusAsync()
    {
        string noInduk = _kotakPencarian.Text.Trim();
        if (noInduk.Length == 0)
        {
            Peringatkan("Isi kolom pencarian dengan No. Induk yang ingin dihapus.");
            return;
        }

        var konfirmasi = MessageBox.Show(
            $"Hapus data dengan No. Induk {noInduk}?",
            "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (konfirmasi != DialogResult.Yes) return;

        bool berhasil = await _repositori.HapusAsync(noInduk);
        Informasikan(berhasil ? "Data berhasil dihapus." : "Data tidak ditemukan.");
        _kotakPencarian.Clear();
        await MuatUlangTabelAsync();
    }

    private async Task MuatUlangTabelAsync()
    {
        _tabelData.DataSource = null;
        _tabelData.DataSource = await _repositori.SemuaAsync();
    }

    private void SalinBarisTerpilihKeFormulir()
    {
        if (_tabelData.CurrentRow?.DataBoundItem is not DataMahasiswa dipilih) return;

        _kotakNoInduk.Text = dipilih.NoInduk;
        _kotakNama.Text = dipilih.NamaLengkap;
        _kotakProdi.Text = dipilih.ProgramStudi;
        _angkaIpk.Value = (decimal)dipilih.Ipk;
    }

    private void BersihkanFormulir()
    {
        _kotakNoInduk.Clear();
        _kotakNama.Clear();
        _kotakProdi.Clear();
        _angkaIpk.Value = 0;
    }

    private void Peringatkan(string pesan) =>
        MessageBox.Show(pesan, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private void Informasikan(string pesan) =>
        MessageBox.Show(pesan, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
