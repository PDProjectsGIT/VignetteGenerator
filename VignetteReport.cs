using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winietowanie
{
    /**
     * Klasa generująca raport do pliku CSV.
     * Raport składa się z zestawów czasów wykonania dla wybranych obrazków przy ustalonej liczbie wątków
     */
    internal class VignetteReport
    {
        
        private VignetteProcessor _vignetteProcessor;

        private readonly int _maxThreads;

        private int _testThreads;

        private int _probeNumber;

        private string _content;

        public int ProbeNumber { 
            set { 
                if(value < 1)
                    throw new VignetteException("Probe number must be grater than 1");
                _probeNumber = value;
            }

            get
            {
                return _probeNumber;
            }
        }

        public int ThreadNumber
        {
            set
            {
                if (value < 1 || value > _maxThreads)
                    throw new VignetteException($"Number of threads must be between 1 and {_maxThreads}");
                _testThreads = value;
            }
            get
            {
                return _testThreads;
            }
        }

        public bool IsReady { get; private set; }

        public VignetteReport(VignetteProcessor vignetteProcessor, int maxThreads)
        {
            IsReady = true;
            _vignetteProcessor = vignetteProcessor;
            _maxThreads = maxThreads;
            _testThreads = _maxThreads;
            _content = string.Empty;
            _probeNumber = 5;
        }

        public void GenerateReport(string[] fileNames)
        {
            IsReady = false;

            _content = "Data;Threads;Date;Time\n" +
                fileNames.Length + ";" + _testThreads + ";" + DateTime.Now.ToString("dd.MM.yyyy;HH:mm:ss")+ "\n;\n;\n";

            foreach (string fileName in fileNames)
            {
                _vignetteProcessor.LoadImage(fileName);

                _content += "Name;Width (px);Height (px)\n" +
                    Path.GetFileName(fileName) + ";" + _vignetteProcessor.ImageWidth + ";" + _vignetteProcessor.ImageHeight + "\n;\n";

                for (int i = 1; i <= _testThreads; i++)
                {
                    _content += "Thread: " + i + "\n";
                    _vignetteProcessor.ThreadNumber = i;

                    //stabilizacja czasów
                    _vignetteProcessor.GenerateVignetteCS();
                    _vignetteProcessor.GenerateVignetteASM();

                    string csTimes = "CS (ms)";
                    string asmTimes = "ASM# (ms)";

                    for(int j = 0; j < _probeNumber; j++)
                    {
                        _vignetteProcessor.GenerateVignetteCS();

                        csTimes += ";" + _vignetteProcessor.getExecutionsValue(); 

                        _vignetteProcessor.GenerateVignetteASM();

                        asmTimes += ";" + _vignetteProcessor.getExecutionsValue();
                    }

                    _content += csTimes + "\n";
                    _content += asmTimes + "\n";
                }

                _content += ";\n;\n";
            }
            _vignetteProcessor.eraseData();
            IsReady = true;
        }

        public void SaveReport(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(_content);
            }
        }
    }
}
