namespace KalkulatorSederhana
{
    public partial class Form1 : Form
    {
        // Nilai yang sedang "ditahan" menunggu operator berikutnya dijalankan.
        private double angkaTertahan = 0;

        // Operator yang sedang aktif ("+", "−", "×", "÷", atau "" kalau belum ada).
        private string operatorAktif = "";

        // Menandai bahwa layar harus dikosongkan dulu sebelum menerima digit baru
        // (dipakai setelah user menekan operator atau setelah menekan "=").
        private bool mulaiAngkaBaru = true;

        public Form1()
        {
            InitializeComponent();
        }

        // Semua tombol angka 0-9 memakai handler ini.
        private void AngkaButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button tombol) return;

            if (mulaiAngkaBaru || txtLayar.Text == "0")
            {
                txtLayar.Text = tombol.Text;
                mulaiAngkaBaru = false;
            }
            else
            {
                txtLayar.Text += tombol.Text;
            }
        }

        // Tombol titik desimal, dipisah dari AngkaButton_Click supaya tidak
        // bisa menambahkan lebih dari satu titik dalam satu angka.
        private void btnDecimal_Click(object? sender, EventArgs e)
        {
            if (mulaiAngkaBaru)
            {
                txtLayar.Text = "0.";
                mulaiAngkaBaru = false;
                return;
            }

            if (!txtLayar.Text.Contains('.'))
            {
                txtLayar.Text += ".";
            }
        }

        // Dipakai oleh tombol ÷, ×, −, dan +.
        private void OperatorButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button tombol) return;

            // Kalau user sudah memilih operator sebelumnya lalu langsung memilih
            // operator lain (tanpa menekan "="), hitung dulu hasil sementaranya.
            if (operatorAktif != "" && !mulaiAngkaBaru)
            {
                HitungDanTampilkan();
            }
            else if (double.TryParse(txtLayar.Text, out double nilaiSaatIni))
            {
                angkaTertahan = nilaiSaatIni;
            }

            operatorAktif = tombol.Text;
            mulaiAngkaBaru = true;
        }

        private void btnEquals_Click(object? sender, EventArgs e)
        {
            HitungDanTampilkan();
            operatorAktif = "";
            mulaiAngkaBaru = true;
        }

        // Method inti yang melakukan perhitungan, dipisah dari btnEquals_Click
        // supaya bisa dipanggil ulang dari OperatorButton_Click.
        private void HitungDanTampilkan()
        {
            if (operatorAktif == "" || !double.TryParse(txtLayar.Text, out double angkaKedua))
            {
                return;
            }

            double hasil;

            try
            {
                hasil = operatorAktif switch
                {
                    "+" => angkaTertahan + angkaKedua,
                    "−" => angkaTertahan - angkaKedua,
                    "×" => angkaTertahan * angkaKedua,
                    "÷" => BagiDenganAman(angkaTertahan, angkaKedua),
                    _ => angkaKedua
                };
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Tidak bisa dihitung",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtLayar.Text = hasil.ToString();
            angkaTertahan = hasil;
        }

        private static double BagiDenganAman(double pembilang, double penyebut)
        {
            if (penyebut == 0)
            {
                throw new DivideByZeroException("Angka tidak bisa dibagi dengan nol.");
            }

            return pembilang / penyebut;
        }

        // Fitur tambahan: ubah angka di layar jadi bentuk persen (dibagi 100).
        private void btnPercent_Click(object? sender, EventArgs e)
        {
            if (double.TryParse(txtLayar.Text, out double nilai))
            {
                txtLayar.Text = (nilai / 100).ToString();
                mulaiAngkaBaru = true;
            }
        }

        // Fitur tambahan: hapus satu digit terakhir dari layar.
        private void btnBackspace_Click(object? sender, EventArgs e)
        {
            if (mulaiAngkaBaru) return;

            if (txtLayar.Text.Length > 1)
            {
                txtLayar.Text = txtLayar.Text[..^1];
            }
            else
            {
                txtLayar.Text = "0";
                mulaiAngkaBaru = true;
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            angkaTertahan = 0;
            operatorAktif = "";
            mulaiAngkaBaru = true;
            txtLayar.Text = "0";
        }
    }
}
