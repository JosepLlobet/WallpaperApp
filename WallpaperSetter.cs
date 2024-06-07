using System.Diagnostics;

namespace WallpaperApp
{
    public class WallpaperSetter
    {
        public void SetVideoAsWallpaper(string videoPath)
        {
            string wid = GetFirstDesktopWindowId();

            if (!string.IsNullOrEmpty(wid))
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "sh",
                    Arguments = $"-c \"xwinwrap -fs -fdt -ni -b -nf -un -o 1.0 -debug -- mpv -wid WID --loop --no-audio {videoPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = processInfo };
                process.Start();
            }
            else
            {
                Console.WriteLine("No se pudo encontrar el ID de la ventana del escritorio.");
            }
        }

        private string GetFirstDesktopWindowId()
        {
            // Configuración del proceso
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "wmctrl",
                Arguments = "-l | grep 'Desktop' | awk '{print $1}'",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            try
            {
                process.Start();

                // Leer la salida del comando
                string output = process.StandardOutput.ReadToEnd();                
                process.WaitForExit();

                Console.WriteLine("Salida:");
                Console.WriteLine(output);

                string[] results = output.Split(" ");

                return results[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al ejecutar el comando:");
                Console.WriteLine(ex.Message);

                return "";
            }

            return "";
        }        
    }
}
