using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace Winietowanie
{
    public partial class MainPage : Form
    {
        /**
         * Struktura przechowująca wartości konfiguracyjne
         */
        private struct SetupValues
        {
            public readonly int minimum;
            public readonly int maximum;
            public readonly int division;

            /**
             * Konstruktor
             */
            public SetupValues(int min, int max, int div)
            {
                minimum = min;
                maximum = max;
                division = div;
            }
        }

        // Maksymalna ilość wątków dla programu
        private const int _maxThreads = 64;

        // Wartości dla współczynnika intensywności winiety
        private readonly SetupValues _intensityValues;

        // Wartości dla współczynnika promienia szerokości winiety
        private readonly SetupValues _radiusValues;

        // Nazwa wczytanego pliku
        private String _filename;

        // Obiekt procesora winiety
        private VignetteProcessor _vignetteProcessor;

        // Obiekt generatora raportów
        private VignetteReport _vignetteReport;
        
        /**
         * Konstruktor inicjujący
         */
        public MainPage()
        {

            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            int numberOfThreads = Environment.ProcessorCount;

            ThreadTrackBar.Maximum = _maxThreads;
            ThreadTrackBar.Minimum = 1;
            ThreadTrackBar.Value = numberOfThreads;
            ThreadLabel.Text = "Threads used: " + (numberOfThreads).ToString();

            _intensityValues = new SetupValues(0, 20, 20);

            IntensityTrackBar.Minimum = _intensityValues.minimum;
            IntensityTrackBar.Maximum = _intensityValues.maximum;
            IntensityTrackBar.Value = _intensityValues.minimum;
            IntensityLabel.Text = "Vignette Intensity: " + _intensityValues.minimum.ToString("F2");

            _radiusValues = new SetupValues(1, 80, 20);

            RadiusTrackBar.Minimum = _radiusValues.minimum;
            RadiusTrackBar.Maximum = _radiusValues.maximum;
            RadiusTrackBar.Value = _radiusValues.maximum;
            RadiusLabel.Text = "Vignette Radius: " + (_radiusValues.maximum / _radiusValues.division).ToString("F2");

            RedTrackBar.Minimum = 0;
            RedTrackBar.Maximum = 255;
            RedTrackBar.Value = 0;
            RedLabel.Text = "Vignette Red: 0";

            GreenTrackBar.Minimum = 0;
            GreenTrackBar.Maximum = 255;
            GreenTrackBar.Value = 0;
            GreenLabel.Text = "Vignette Green: 0";

            BlueTrackBar.Minimum = 0;
            BlueTrackBar.Maximum = 255;
            BlueTrackBar.Value = 0;
            BlueLabel.Text = "Vignette Blue: 0";

            _vignetteProcessor = new VignetteProcessor();

            _vignetteProcessor.ThreadNumber = numberOfThreads;

            _vignetteProcessor.Radius = 4.0;

            _vignetteProcessor.Intensity = (double)_intensityValues.maximum / _intensityValues.division;

            _vignetteReport = new VignetteReport(_vignetteProcessor, _maxThreads);
        }

        /**
         * Obsługa przycisku wyszukiwania
         */
        private void BrowseButton_Click_1(object sender, EventArgs e)
        {
            if (!_vignetteReport.IsReady)
            {
                MessageBox.Show("Please wait until report generation is finished.", "Report in progress",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            checkDirectories();

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, @"test_images\input\");

            fileDialog.Filter = "Image Files(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _filename = fileDialog.FileName;
                    _vignetteProcessor.LoadImage(_filename);
                    inputImage.Image = new Bitmap(_filename);
                    CTimeLabel.Text = "C# Generator time: -- ms";
                    ASMTimeLabel.Text = "ASM Generator time: -- ms";
                }
                catch (VignetteException ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.InnerException.Message, "No file is loaded",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        /**
         * Obsługa przycisku konwersji do winiety
         */
        private void ConvertButton_Click_1(object sender, EventArgs e)
        {
            if (!_vignetteReport.IsReady)
            {
                MessageBox.Show("Please wait until report generation is finished.", "Report in progress",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if(CLibrary.Checked)
                {
                   _vignetteProcessor.GenerateVignetteCSDLL();

                    CTimeLabel.Text = "C# Generator time: " + _vignetteProcessor.getExecutionTime();
                } 
                else if (ASMLibrary.Checked)
                {
                    _vignetteProcessor.GenerateVignetteASM();

                    ASMTimeLabel.Text = "ASM Generator time: " + _vignetteProcessor.getExecutionTime();
                }
                else
                {
                    MessageBox.Show("No button is checked.", "Application error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }        
                outputImage.Image = _vignetteProcessor.GetProccesedImage();

                if (SaveToFileCheckBox.Checked) {

                    checkDirectories();

                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.InitialDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, @"test_images\output\");

                    saveFileDialog.Filter = "Image Files(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        _vignetteProcessor.SaveVignetteToFile(filePath);
                    }
                }
                    
                    
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "No file is loaded",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Obsługa suwaka wątków
         */
        private void ThreadTrackBar_Scroll(object sender, EventArgs e)
        {
            int numberOfThreads = ThreadTrackBar.Value;
            try
            {
                _vignetteProcessor.ThreadNumber = numberOfThreads;
                ThreadLabel.Text = "Threads used: " + numberOfThreads;
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Thread value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        /**
         * Obsługa suwaka współczynnika intensywności
         */
        private void IntensityTrackBar_Scroll(object sender, EventArgs e)
        {
            double intensity = IntensityTrackBar.Value / (double)_intensityValues.maximum;
            try
            {
                _vignetteProcessor.Intensity = 1 - intensity;
                IntensityLabel.Text = "Vignette Intensity: " + intensity.ToString("F2");
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Intensity value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        /**
         * Obsługa suwaka współczynnika promienia szerokości winiety
         */
        private void RadiusTrackBar_Scroll(object sender, EventArgs e)
        {
            double radius = RadiusTrackBar.Value / (double)_radiusValues.maximum * 4;
            try
            {
                _vignetteProcessor.Radius = radius;
                RadiusLabel.Text = "Vignette Radius: " + radius.ToString("F2");
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Radius value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Obsługa suwaka korekcji koloru czerwonego
         */
        private void RedTrackBar_Scroll(object sender, EventArgs e)
        {
            int red = RedTrackBar.Value;
            try
            {
                _vignetteProcessor.VignetteRed = red;
                RedLabel.Text = "Vignette Red: "+red;
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Color value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Obsługa suwaka korekcji koloru zielonego
         */
        private void GreenTrackBar_Scroll(object sender, EventArgs e)
        {
            int green = GreenTrackBar.Value;
            try
            {
                _vignetteProcessor.VignetteGreen = green;
                GreenLabel.Text = "Vignette Green: " + green;
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Color value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Obsługa suwaka korekcji koloru niebieskiego
         */
        private void BlueTrackBar_Scroll(object sender, EventArgs e)
        {
            int blue = BlueTrackBar.Value;
            try
            {
                _vignetteProcessor.VignetteBlue = blue;
                BlueLabel.Text = "Vignette Blue: " + blue;
            }
            catch (VignetteException ex)
            {
                MessageBox.Show(ex.Message, "Color value error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Metoda sprawdzająca i tworząca odpowiednie katalogi
         */
        private void checkDirectories()
        {
            string testImages = "test_images";
            string input = Path.Combine(testImages, "input");
            string output = Path.Combine(testImages, "output");
            string reports = Path.Combine(testImages, "reports");

            if (!Directory.Exists(testImages))
            {
                Directory.CreateDirectory(testImages);
            }

            // Sprawdź i utwórz folder 'input'
            if (!Directory.Exists(input))
            {
                Directory.CreateDirectory(input);
            }

            // Sprawdź i utwórz folder 'output'
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            // Sprawdź i utwórz folder 'output'
            if (!Directory.Exists(reports))
            {
                Directory.CreateDirectory(reports);
            }
        }

        /**
         * Metoda generująca report
         */
        private async void RaportButton_Click(object sender, EventArgs e)
        {
            if (!_vignetteReport.IsReady)
            {
                MessageBox.Show("Please wait until report generation is finished.", "Report in progress",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            checkDirectories();

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, @"test_images\input\");

            fileDialog.Filter = "Image Files(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";

            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] fileNames = fileDialog.FileNames;

                    checkDirectories();

                    MessageBox.Show("Report is processing. Please wait.", "Report",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    inputImage.Image = null;

                    _vignetteReport.ThreadNumber = ThreadTrackBar.Value;

                    _vignetteReport.ProbeNumber = (int)ProbesUpDown.Value;

                    await Task.Run(() =>
                    {
                        _vignetteReport.GenerateReport(fileNames);
                       
                    });

                    if(_vignetteReport.IsReady)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();

                        saveFileDialog.InitialDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, @"test_images\reports\");

                        saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|Text files (*.txt)|*.txt";

                        string currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                        saveFileDialog.FileName = $"Report_{currentDateTime}";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            _vignetteReport.SaveReport(filePath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("An unknown error occurred while generating the report.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (VignetteException ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.InnerException.Message, "No file is loaded",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
