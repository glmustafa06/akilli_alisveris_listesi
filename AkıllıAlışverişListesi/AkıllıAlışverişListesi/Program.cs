using AkilliAlisveris;

namespace AkıllıAlışverişListesi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Uygulama yapılandırması, DPI ayarları veya varsayılan yazı tipini özelleştirmek için
            // https://aka.ms/applicationconfiguration bakılabilir.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1()); // Form1'i çalıştır
        }
    }
}
