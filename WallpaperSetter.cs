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
                wid = "0xa60000a";
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
            var processInfo = new ProcessStartInfo
            {
                FileName = "sh",
                Arguments = "-c \"xdotool search --name 'Desktop' | head -n 1\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();

            string result = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return result;
        }
    }
}
