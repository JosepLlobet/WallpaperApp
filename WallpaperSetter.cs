using System.Diagnostics;

namespace WallpaperApp
{
    public class WallpaperSetter
    {
        public void SetVideoAsWallpaper(string videoPath)
        {
            string args = $"-c \"xwinwrap -d -st -ni -s -nf -b -un -argb -fs -fdt -- mpv --hwdec=vdpau --vo=vdpau --loop --mute=yes --stop-screensaver=no -wid WID {videoPath}\"";
            
            var processInfo = new ProcessStartInfo
            {
                FileName = "sh",
                //Arguments = $"-c \"xwinwrap -fs -fdt -ni -b -nf -un -o 1.0 -debug -- mpv -wid WID --loop --no-audio {videoPath}\"",
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();
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
